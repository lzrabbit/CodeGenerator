using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CodeGenerator.Core.DB
{
    public class DBHelper
    {
        private DBHelper() { }

        static DBHelper()
        {
            ConnString = "Server=192.168.9.51;Database=information_schema;Uid=liuzhao;Pwd=nopass.2;charset=utf8;";
        }

        public static string ConnString { get; set; }
        public static List<DbColumn> GetgDbColumns(string database, string table)
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
                    Precision = SafeConvert.ToInt32(row["NUMERIC_PRECISION"]),
                    Scale = SafeConvert.ToInt32(row["NUMERIC_SCALE"]),
                });
            }
            return list;
        }

        public static DataTable GetDbIndexs()
        {
            return null;
        }

        public static List<DbTable> GetDbTables(string DataBaseName)
        {
            string sql = string.Format("SELECT * from `TABLES` where TABLE_SCHEMA='{0}'", DataBaseName);
            DataTable dt = MySqlHelper.ExecuteDataset(ConnString, sql).Tables[0];
            return dt.Rows.Cast<DataRow>().Select(row => new DbTable
            {
                HasPrimaryKey = true,
                TableName = row["TABLE_NAME"].ToString(),
                SchemaName = row["TABLE_SCHEMA"].ToString(),
                Rows = SafeConvert.ToInt32(row["TABLE_ROWS"]),

            }).ToList();
        }

        public static List<DbDataBase> GetDatabases()
        {

            string sql = "SELECT * from SCHEMATA where SCHEMA_NAME<>'test' and SCHEMA_NAME<>'information_schema'";
            DataTable dt = MySqlHelper.ExecuteDataset(ConnString, sql).Tables[0];

            var result = dt.Rows.Cast<DataRow>().Select(row => new DbDataBase
              {
                  DataBaseName = row["SCHEMA_NAME"].ToString(),
                  SchemaName = null,
              }).ToList();
            return result;
        }

       
    }
}
