using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Core.DB
{
    public class SqlServerDbMap
    {
        public static string CsharpType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;
            dbtype = dbtype.ToLower();
            string csharpType = "object";
            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "numeric": csharpType = "decimal"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallint": csharpType = "short"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
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
                case "FLOAT": return "Float";
                case "DOUBLE": return "Double";
                case "DECIMAL ": return "Decimal";

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
