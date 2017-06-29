using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Repository.baseDAO
{
    public class Column
    {
        public string ColumnName { get; set; }
        public string ColumnComment { get; set; }
        public string DataType { get; set; }

        public override string ToString()
        {
            return $"{ColumnName.PadRight(20)}  {DataType.PadRight(20)}  {ColumnComment}  ";
        }

        public string GeneratePropertyString()
        {
            return string.Format("public {0} {1} {{ get; set; }}", GetDataTypeString(), ColumnName);
        }

        private string GetDataTypeString()
        {
            switch (DataType)
            {
                case "int":
                    return "int";
                case "datetime":
                    return "DateTime";
                case "varchar":
                    return "string";
                default:
                    return "string";
            }
        }

        public string GenerateSavePropertyString()
        {
            return string.Format("public {0} {1} {{ get; set; }}", GetSaveDataTypeString(), ColumnName);
        }

        private string GetSaveDataTypeString()
        {
            switch (DataType)
            {
                case "int":
                    return "int?";
                case "datetime":
                    return "DateTime?";
                case "varchar":
                    return "string";
                default:
                    return "string";
            }
        }
    }

    public class Table
    {
        public string TableName { get; set; }
        public string TableComment { get; set; }
    }
}
