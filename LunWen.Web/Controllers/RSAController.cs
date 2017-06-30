using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using LunWen.Infrastructure;
using System.Text;

namespace LunWen.Web.Controllers
{
    public class RSAController : Controller
    {
        public string GetRSAKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //私钥
            TempData["RSAKey"] = rsa.ToXmlString(true);
            RSAParameters parameter = rsa.ExportParameters(true);
            string strPublicKeyExponent = BytesToHexString(parameter.Exponent);
            string strPublicKeyModulus = BytesToHexString(parameter.Modulus);
            string tmpToken = MD5Helper.MD5Value(strPublicKeyModulus);
            TempData["TmpToken"] = tmpToken;
            return strPublicKeyExponent + "|" + strPublicKeyModulus + "|" + tmpToken;
        }

        private string BytesToHexString(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", input[i]));
            }
            return hexString.ToString();

        }
    }
}