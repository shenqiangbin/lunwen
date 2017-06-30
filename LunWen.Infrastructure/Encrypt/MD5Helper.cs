using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LunWen.Infrastructure
{
    public class MD5Helper
    {
        /// <summary>
        /// MD5.大写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5Value(string value)
        {
            var md5 = MD5.Create();
            byte[] targetStr = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(value));
            return BitConverter.ToString(targetStr).Replace("-", "");
        }        
    }
}