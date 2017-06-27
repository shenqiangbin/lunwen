using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Infrastructure
{
    public class SexHelper
    {
        public static string GetDesc(int number)
        {
            if (number == 0)
                return "男";
            else if (number == 1)
                return "女";
            else
                return "未知";
        }

        public static int GetNumber(string desc)
        {
            if (desc == "男")
                return 0;
            else if (desc == "女")
                return 1;
            else
                return -1;
        }
    }
}
