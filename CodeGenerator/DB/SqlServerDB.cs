using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CodeGenerator.DB
{
    public class SqlServerDB : IDB
    {
        public string ConnString { get; set; }

        public SqlServerDB(string connString)
        {
            this.ConnString = connString;
        }


        #region IDB 成员
        public EDbType DbType
        {
            get { return EDbType.SqlServer; }
        }

        public bool TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(this.ConnString))
            {
                conn.Open();
                conn.Close();
                return true;
            }
        }

        public List<DbColumn> GetDbColumns(string database, string table)
        {
            #region SQL

            string sql = string.Format(@"
                                        WITH indexCTE AS
                                        (
	                                        SELECT
                                            ic.column_id,
                                            ic.index_column_id,
                                            ic.object_id
                                            FROM [{0}].sys.indexes idx
                                            INNER JOIN [{0}].sys.index_columns ic ON idx.index_id = ic.index_id AND idx.object_id = ic.object_id
                                            WHERE  idx.object_id =OBJECT_ID('{1}') AND idx.is_primary_key=1
                                        )
                                        select
                                        colm.column_id ColumnID,
                                        CAST(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS BIT) IsPrimaryKey,
                                        colm.name ColumnName,
                                        systype.name ColumnType,
                                        colm.is_identity IsIdentity,
                                        colm.is_nullable IsNullable,
                                        cast(colm.max_length as int) ByteLength,
                                        (
                                            case
                                                when systype.name='nvarchar' and colm.max_length>0 then colm.max_length/2
                                                when systype.name='nchar' and colm.max_length>0 then colm.max_length/2
                                                when systype.name='ntext' and colm.max_length>0 then colm.max_length/2
                                                else colm.max_length
                                            end
                                        ) CharLength,
                                        cast(colm.precision as int) Precision,
                                        cast(colm.scale as int) Scale,
                                        prop.value Remark
                                        from [{0}].sys.columns colm
                                        inner join [{0}].sys.types systype on colm.system_type_id=systype.system_type_id and colm.user_type_id=systype.user_type_id
                                        left join [{0}].sys.extended_properties prop on colm.object_id=prop.major_id and colm.column_id=prop.minor_id
                                        LEFT JOIN indexCTE ON colm.column_id=indexCTE.column_id AND colm.object_id=indexCTE.object_id
                                        where colm.object_id=OBJECT_ID('{1}')
                                        order by colm.column_id", database, table);

            #endregion SQL
            this.ConnString = this.ConnString.Replace("master", database);
            DataTable dt = ExecuteDataTable(sql);
            return dt.Rows.Cast<DataRow>().Select(row => new DbColumn()
            {
                IsPrimaryKey = row.Field<bool>("IsPrimaryKey"),
                ColumnName = row.Field<string>("ColumnName"),
                ColumnType = row.Field<string>("ColumnType"),
                IsIdentity = row.Field<bool>("IsIdentity"),
                IsNullable = row.Field<bool>("IsNullable"),
                ByteLength = row.Field<int>("ByteLength"),
                CharLength = row.Field<int>("CharLength"),
                Scale = row.Field<int>("Scale"),
                Remark = row["Remark"].ToString(),
                CSharpType = SqlServerDbMap.CsharpType(row.Field<string>("ColumnType")),
                JavaType = SqlServerDbMap.JavaType(row.Field<string>("ColumnType")),
            }).ToList();
        }

        public List<DbIndex> GetDbIndexs(string database, string table)
        {
            #region SQL

            string sql = string.Format(@"
                                        select
                                        idx.name IndexName
                                        ,idx.type_desc IndexType
                                        ,idx.is_primary_key IsPrimaryKey
                                        ,idx.is_unique IsUnique
                                        ,idx.is_unique_constraint IsUniqueConstraint
                                        ,STUFF(
                                        (
	                                        SELECT  ','+c.name from [{0}].sys.index_columns ic
	                                        inner join [{0}].sys.columns c on ic.column_id=c.column_id and ic.object_id=c.object_id
	                                        WHERE ic.is_included_column = 0 and ic.index_id=idx.index_id AND ic.object_id=idx.object_id
	                                        ORDER BY ic.key_ordinal
	                                        FOR XML PATH('')
                                        ),1,1,'') IndexColumns
                                        ,STUFF(
                                        (
	                                        SELECT  ','+c.name from [{0}].sys.index_columns ic
	                                        inner join [{0}].sys.columns c on ic.column_id=c.column_id and ic.object_id=c.object_id
	                                        WHERE ic.is_included_column = 1 and ic.index_id=idx.index_id AND ic.object_id=idx.object_id
	                                        ORDER BY ic.key_ordinal
	                                        FOR XML PATH('')
                                        ),1,1,'') IncludeColumns
                                        from [{0}].sys.indexes idx
                                        where object_id =OBJECT_ID('{1}')", database, table);

            #endregion SQL

            DataTable dt = ExecuteDataTable(sql);
            return dt.Rows.Cast<DataRow>().Select(row => new DbIndex()
            {
                IndexName = row.Field<string>("IndexName"),
                IndexType = row.Field<string>("IndexType"),
                IsPrimaryKey = row.Field<bool>("IsPrimaryKey"),
                IsUnique = row.Field<bool>("IsUnique"),
                IsUniqueConstraint = row.Field<bool>("IsUniqueConstraint"),
                IndexColumns = row.Field<string>("IndexColumns"),
                IncludeColumns = row.Field<string>("IncludeColumns")
            }).ToList();
        }

        public List<DbTable> GetDbTables(string database)
        {
            #region SQL

            string sql = string.Format(@"SELECT
                                        obj.name tablename,
                                        schem.name schemname,
                                        idx.rows,
                                        CAST
                                        (
	                                        CASE
		                                        WHEN (SELECT COUNT(1) FROM sys.indexes WHERE object_id= obj.OBJECT_ID AND is_primary_key=1) >=1 THEN 1
		                                        ELSE 0
	                                        END
                                        AS BIT) HasPrimaryKey
                                        from [{0}].sys.objects obj
                                        inner join [{0}].dbo.sysindexes idx on obj.object_id=idx.id and idx.indid<=1
                                        INNER JOIN [{0}].sys.schemas schem ON obj.schema_id=schem.schema_id
                                        where type='U'
                                        order by obj.name", database);

            #endregion SQL

            DataTable dt = ExecuteDataTable(sql);
            return dt.Rows.Cast<DataRow>().Select(row => new DbTable
            {
                TableName = row.Field<string>("tablename"),
                SchemaName = row.Field<string>("schemname"),
                Rows = row.Field<int>("rows"),
                HasPrimaryKey = row.Field<bool>("HasPrimaryKey"),
            }).ToList();
        }

        public List<string> GetDatabases()
        {
            string sql = "select name from sys.databases where name not in ('master','model','msdb','tempdb')";
            DataTable dt = ExecuteDataTable(sql);
            return dt.Rows.Cast<DataRow>().Select(row => row["name"].ToString()).ToList();
        }

        #endregion

        private DataTable ExecuteDataTable(string sql)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, this.ConnString))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public class SqlServerDbMap
        {
            public static string CsharpType(string dbtype)
            {
                if (string.IsNullOrEmpty(dbtype)) return dbtype;
                dbtype = dbtype.ToLower();
                switch (dbtype)
                {
                    case "bit": return "bool";
                    case "tinyint": return "byte";
                    case "smallint": return "short";
                    case "real": return "Single";
                    case "int": return "int";
                    case "bigint": return "long";
                    case "float": return "double";
                    case "decimal": return "decimal";
                    case "smallmoney": return "decimal";
                    case "money": return "decimal";
                    case "numeric": return "decimal";

                    case "char": return "string";
                    case "nchar": return "string";
                    case "varchar": return "string";
                    case "nvarchar": return "string";
                    case "text": return "string";
                    case "ntext": return "string";
                    case "xml": return "string";

                    case "date": return "DateTime";
                    case "datetime": return "DateTime";
                    case "datetime2": return "DateTime";
                    case "datetimeoffset": return "DateTimeOffset";
                    case "smalldatetime": return "DateTime";
                    case "time": return "TimeSpan";

                    case "binary": return "byte[]";
                    case "timestamp": return "byte[]";
                    case "varbinary": return "byte[]";
                    case "image": return "byte[]";

                    case "sql_variant": return "object";
                    case "sysname": return "object";
                    case "uniqueidentifier": return "Guid";
                    default: return dbtype;
                }
            }



            public static string JavaType(string dbtype)
            {
                //Long long
                //Boolean boolean
                //Integer int
                //Double double
                //Float float;
                //Character char
                //Short short
                //BigDecimal BigDecimal
                //Byte byte
                if (string.IsNullOrEmpty(dbtype)) return dbtype;
                dbtype = dbtype.ToLower();
                switch (dbtype)
                {
                    case "bit": return "Boolean";
                    case "tinyint": return "Byte";
                    case "smallint": return "Short";
                    case "real": return "Integer";
                    case "int": return "Integer";
                    case "bigint": return "Long";
                    case "float": return "Float";
                    case "decimal": return "BigDecimal";
                    case "smallmoney": return "BigDecimal";
                    case "money": return "BigDecimal";
                    case "numeric": return "BigDecimal";

                    case "char": return "Sstring";
                    case "nchar": return "String";
                    case "varchar": return "String";
                    case "nvarchar": return "String";
                    case "text": return "String";
                    case "ntext": return "String";
                    case "xml": return "String";

                    case "date": return "Date";
                    case "datetime": return "Timestamp";
                    case "datetime2": return "Timestamp";
                    case "datetimeoffset": return "Timestamp";
                    case "smalldatetime": return "Timestamp";
                    case "time": return "Time";

                    case "binary": return "byte[]";
                    case "timestamp": return "byte[]";
                    case "varbinary": return "byte[]";
                    case "image": return "byte[]";

                    case "sql_variant": return "object";
                    case "sysname": return "object";
                    case "uniqueidentifier": return "Guid";
                    default: return dbtype;
                }
            }
        }
    }
}
