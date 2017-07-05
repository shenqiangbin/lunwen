using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Cache
{
    public class KeyManager
    {
        public static string GetUserKey(string userCode)
        {
            return "lunwen-user-usercode-" + userCode;
        }

        public static string GetALLMenuKey()
        {
            return "lunwen-allmenus";
        }
    }
}
