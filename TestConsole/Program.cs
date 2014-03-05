using CodeGenerator.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestConsole
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public int AGE { get; set; }
        static void Main(string[] args)
        {
            string str = "ABC";
            Console.WriteLine(str.ToTitleUpper());
            str = null;

            Console.WriteLine(str.ToTitleLower());
            MyBatis entity = new MyBatis
            {
                Package = "nb.cnblogs.lzrabbit.domain",
                TableName = "GProduct",
                DbColumns = CodeGenerator.Core.DB.DBHelper.GetgDbColumns("ghotel", "GProduct")
            };
            string code = entity.TransformText();
            Console.WriteLine(code);

            File.WriteAllText("D:/entity.java", code);

            Console.Read();

        }
    }
}
