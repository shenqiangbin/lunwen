﻿using LunWen.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GetSomePassword();
        }

        private static void GetSomePassword()
        {
            StringBuilder builder = new StringBuilder();

            List<string> pwds = new List<string>()
           {
               "123","123456"
           };

            foreach (var pwd in pwds)
            {
                var salt = Guid.NewGuid().ToString();
                var hashPwd = HashHelper.HashMd5(pwd, salt);
                builder.AppendFormat("{0}  {1}  {2} \r\n", pwd, salt, hashPwd);
            }

            System.IO.File.WriteAllText("d:/pwd.txt",builder.ToString());
            Process.Start("d:/pwd.txt");
        }
    }
}