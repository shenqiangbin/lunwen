using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Repository.baseDAO
{
    public class DicHelper
    {
        public static string ToSql(Dictionary<string, string> dic, DicSeprator seprator)
        {
            string sepratorStr = GetStr(seprator);

            List<string> list = new List<string>();
            foreach (var item in dic)
                list.Add($"{item.Key}='{item.Value}'");

            return string.Join(sepratorStr, list.ToArray());
        }

        public static string ToSqlWithPara(Dictionary<string, string> dic)
        {
            List<string> list = new List<string>();
            foreach (var item in dic)
                list.Add($"{item.Key}=@{item.Key}");

            return string.Join(" and ", list.ToArray());
        }

        private static string GetStr(DicSeprator seprator)
        {
            switch (seprator)
            {
                case DicSeprator.And:
                    return " and ";
                case DicSeprator.Comma:
                    return ",";
                default:
                    return ",";
            }
        }
    }

    public enum DicSeprator
    {
        And,
        Comma,
    }
}
