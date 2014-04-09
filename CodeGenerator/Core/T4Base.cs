using CodeGenerator.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerator.Core
{
    public abstract class T4Base : T4TemplateBase
    {

        public string Namespace { get; set; }

        public string TableName { get; set; }

        public string FullClassName { get { return this.Namespace + "." + TableName; } }

        public List<DbColumn> DbColumns { get; set; }

        public abstract string TransformText();

        public List<string> DbColumnsWithoutPrimaryKey
        {
            get { return this.DbColumns.Where(col => col.IsPrimaryKey == false && col.IsIdentity == false).Select(col => col.ColumnName).ToList(); }
        }

        public List<string> DbColumnsWithoutIdentityKey
        {
            get { return this.DbColumns.Where(col => col.IsIdentity == false).Select(col => col.ColumnName).ToList(); }
        }

        public List<string> PrimaryKeys
        {
            get { return this.DbColumns.Where(col => col.IsPrimaryKey == true).Select(col => col.ColumnName).ToList(); }
        }

        public T4Base()
        {
            this.Namespace = this.GetType().Namespace;
        }
    }

    public static class StringExt
    {
        public static string ToTitleUpper(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length == 1) return str.ToUpper();
            else return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        public static string ToTitleLower(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length == 1) return str.ToLower();
            else return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        public static string RegexReplace(this string str, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return Regex.Replace(str, pattern, replacement);

        }
    }
}
