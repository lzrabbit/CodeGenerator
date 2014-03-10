using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CodeGenerator.Core.DB
{
    public class MySqlDB : IDB
    {
        public string ConnString { get; set; }

        public MySqlDB(string connString)
        {
            this.ConnString = connString;
        }


        #region IDB 成员

        public bool TestConnection()
        {
            using (MySqlConnection conn = new MySqlConnection(this.ConnString))
            {
                conn.Open();
                conn.Close();
                return true;
            }
        }

        public List<DbColumn> GetDbColumns(string database, string table)
        {
            List<DbColumn> list = new List<DbColumn>();
            DataTable dt = MySqlHelper.ExecuteDataset(ConnString, string.Format("SELECT * FROM `COLUMNS` WHERE TABLE_SCHEMA='{0}' AND TABLE_NAME='{1}';", database, table)).Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DbColumn
                {
                    ByteLength = SafeConvert.ToInt32(row["CHARACTER_OCTET_LENGTH"]),
                    CharLength = SafeConvert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]),
                    ColumnName = row["COLUMN_NAME"].ToString(),
                    ColumnType = row["DATA_TYPE"].ToString(),
                    IsIdentity = row["EXTRA"].ToString() == "auto_increment",
                    IsNullable = row["IS_NULLABLE"].ToString() == "YES",
                    IsPrimaryKey = row["COLUMN_KEY"].ToString() == "PRI",
                    Remark = row["COLUMN_COMMENT"].ToString(),
                    //Precision = SafeConvert.ToInt32(row["NUMERIC_PRECISION"]),
                    Scale = SafeConvert.ToInt32(row["NUMERIC_SCALE"]),
                    CSharpType = MySqlDbMap.CsharpType(row["DATA_TYPE"].ToString()),
                    JavaType = MySqlDbMap.JavaType(row["DATA_TYPE"].ToString()),
                });
            }
            return list;
        }

        public List<DbIndex> GetDbIndexs(string database, string table)
        {
            throw new NotImplementedException();
        }

        public List<DbTable> GetDbTables(string database)
        {
            string sql = string.Format("SELECT * from `TABLES` where TABLE_SCHEMA='{0}'", database);
            DataTable dt = MySqlHelper.ExecuteDataset(ConnString, sql).Tables[0];
            return dt.Rows.Cast<DataRow>().Select(row => new DbTable
            {
                HasPrimaryKey = true,
                TableName = row["TABLE_NAME"].ToString(),
                SchemaName = row["TABLE_SCHEMA"].ToString(),
                Rows = SafeConvert.ToInt32(row["TABLE_ROWS"]),

            }).ToList();
        }

        public List<string> GetDatabases()
        {
            string sql = "SELECT * from SCHEMATA where SCHEMA_NAME<>'test' and SCHEMA_NAME<>'information_schema'";
            DataTable dt = MySqlHelper.ExecuteDataset(ConnString, sql).Tables[0];

            var result = dt.Rows.Cast<DataRow>().Select(row => row["SCHEMA_NAME"].ToString()).ToList();
            return result;
        }

        #endregion


        public class MySqlDbMap
        {
            public static string CsharpType(string dbtype)
            {
                dbtype = dbtype.ToUpper();
                switch (dbtype)
                {
                    case "TINYINT": return "byte";
                    case "SMALLINT": return "short";
                    case "MEDIUMINT": return "int";
                    case "INT": return "int";
                    case "BIGINT": return "long";
                    case "FLOAT": return "float";
                    case "DOUBLE": return "double";
                    case "DECIMAL ": return "decimal";

                    case "CHAR": return "string";
                    case "VARCHAR": return "string";
                    case "TINYTEXT": return "string";
                    case "TEXT": return "string";
                    case "MEDIUMTEXT": return "string";
                    case "LONGTEXT": return "string";

                    case "TINYBLOB": return "byte[]";
                    case "BLOB": return "byte[]";
                    case "MEDIUMBLOB": return "byte[]";
                    case "LONGBLOB": return "byte[]";

                    case "DATE": return "DateTime";
                    case "TIME": return "DateTime";
                    case "DATETIME": return "DateTime";
                    case "TIMESTAMP": return "DateTime";
                    case "YEAR": return "DateTime";
                    default:
                        return dbtype;
                }
            }

            public static string JavaType(string dbtype)
            {
                dbtype = dbtype.ToUpper();
                switch (dbtype)
                {
                    case "TINYINT": return "int";
                    case "SMALLINT": return "int";
                    case "MEDIUMINT": return "int";
                    case "INT": return "int";
                    case "BIGINT": return "long";
                    case "FLOAT": return "float";
                    case "DOUBLE": return "double";
                    case "DECIMAL ": return "decimal";

                    case "CHAR": return "String";
                    case "VARCHAR": return "String";
                    case "TINYTEXT": return "String";
                    case "TEXT": return "String";
                    case "MEDIUMTEXT": return "String";
                    case "LONGTEXT": return "String";

                    case "TINYBLOB": return "byte[]";
                    case "BLOB": return "byte[]";
                    case "MEDIUMBLOB": return "byte[]";
                    case "LONGBLOB": return "byte[]";

                    case "DATE": return "Date";
                    case "TIME": return "Time";
                    case "DATETIME": return "Timestamp";
                    case "TIMESTAMP": return "Timestamp";
                    case "YEAR": return "Date";
                    default:
                        return dbtype;
                }
            }
        }

    }
}
