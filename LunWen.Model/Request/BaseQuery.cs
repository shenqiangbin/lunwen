using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Model.Request
{
    public class BaseQuery
    {
        public PageCondition PageCondition { get; set; }
        public string Order { get; set; }
    }

    public class QuerySql
    {
        public string SelectSql { get; set; }
        public string CountSql { get; set; }
    }

    public class PageCondition
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public PageCondition(int CurrentPage, int ItemsPerPage)
        {
            this.CurrentPage = CurrentPage;
            this.ItemsPerPage = ItemsPerPage;
        }
    }

    public class QueryResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
