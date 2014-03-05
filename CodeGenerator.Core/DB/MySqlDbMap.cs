using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Core.DB
{
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

                case "DATE": return "java.sql.Date";
                case "TIME": return "java.sql.Time";
                case "DATETIME": return "java.sql.Timestamp";
                case "TIMESTAMP": return "java.sql.Timestamp";
                case "YEAR": return "java.sql.Date";
                default:
                    return dbtype;
            }
        }
    }
}
