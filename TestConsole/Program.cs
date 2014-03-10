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

            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            Console.Read();

        }
    }
}
