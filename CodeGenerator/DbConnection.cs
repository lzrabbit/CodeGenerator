using CodeGenerator.Core.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class DbConnection
    {
        static readonly string FILE = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lzrabbit", "SimpleCoder", "conf.ini");
        public EDbType DbType { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\r\n", this.DbType, this.Server, this.Port, this.User, this.Password);
            //this.txtConnString.Text = "Server=192.168.9.51;Database=information_schema;Uid=liuzhao;Pwd=nopass.2;charset=utf8;";
            //this.txtConnString.Text = "data source=192.168.99.109;database=Elong_Tuan;user id=sa;password=nopass.2;";

        }
        public string ToConnString()
        {
            string connString;
            if (DbType == EDbType.SqlServer) connString = string.Format("data source={0};database=master;user id={1};password={2};", this.Server, this.User, this.Password);
            else connString = string.Format("Server={0};Database=information_schema;Uid={1};Pwd={2};charset=utf8;", this.Server, this.User, this.Password);
            return connString;
        }

        public IDB GetDB()
        {
            if (this.DbType == EDbType.MySql) return new MySqlDB(this.ToConnString());
            else return new SqlServerDB(this.ToConnString());
        }


        public static List<DbConnection> Load()
        {
            List<DbConnection> list = new List<DbConnection>();
            if (File.Exists(FILE))
            {
                var conns = File.ReadAllLines(FILE);
                foreach (var conn in conns)
                {
                    if (!string.IsNullOrEmpty(conn))
                    {
                        list.Add(new DbConnection
                        {
                            DbType = conn.Split('\t')[0] == EDbType.SqlServer.ToString() ? EDbType.SqlServer : EDbType.MySql,
                            Server = conn.Split('\t')[1],
                            Port = int.Parse(conn.Split('\t')[2]),
                            User = conn.Split('\t')[3],
                            Password = conn.Split('\t')[4],
                        });
                    }
                }
            }
            return list;
        }

        public void Save()
        {
            var dir=Path.GetDirectoryName(FILE);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.AppendAllText(FILE, this.ToString());
        }

        public static void Save(List<DbConnection> conns)
        {
            File.WriteAllLines(FILE, conns.Select(conn => conn.ToString()).ToArray());
        }
    }

    public enum EDbType
    {
        SqlServer,
        MySql
    }
}
