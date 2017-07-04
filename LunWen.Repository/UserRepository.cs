using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunWen.Repository.baseDAO;
using LunWen.Model;
using LunWen.Model.Request;
using Dapper;

namespace LunWen.Repository
{
    public class UserRepository : MySqlBaseRepository<User>
    {
        public QueryResult<UserItem> Get(UserQuery query)
        {
            QueryResult<UserItem> result = new QueryResult<UserItem>();

            var sql = GetUserQuerySql(query);
            var para = GetUserQueryPara(query);

            result.List = GetConn().Query<UserItem>(sql.SelectSql, para);
            result.TotalCount = GetConn().ExecuteScalar<int>(sql.CountSql, para);

            return result;
        }

        public QuerySql GetUserQuerySql(UserQuery query)
        {
            var whereList = new List<string>();
            whereList.Add("user.status = 1");

            if (!string.IsNullOrEmpty(query.UserName)) whereList.Add($"user.username like @userName");

            string limitStr = "";
            if (query.PageCondition != null)
                limitStr = $" limit {(query.PageCondition.CurrentPage - 1) * query.PageCondition.ItemsPerPage},{query.PageCondition.ItemsPerPage}";

            string sql = @"
select * from (select *,(@rowNum:=@rowNum+1) as Number from (
SELECT 
	*
 FROM user
where {0} order by {1} {2}
)t,(Select (@rowNum :=0) ) b) tt ";

            string countSqlFormat = @"
SELECT 
	count(id)
 FROM user
where {0} ";

            if (string.IsNullOrEmpty(query.Order))
                query.Order = "user.id asc";

            string selectSql = string.Format(sql, string.Join(" and ", whereList.ToArray()), query.Order, limitStr);
            string countSql = string.Format(countSqlFormat, string.Join(" and ", whereList.ToArray()));

            return new QuerySql { SelectSql = selectSql, CountSql = countSql };
        }

        public DynamicParameters GetUserQueryPara(UserQuery query)
        {
            DynamicParameters para = new DynamicParameters();

            if (!string.IsNullOrEmpty(query.UserName))
                para.Add($"@userName", "%" + query.UserName + "%");
            return para;
        }

        public IEnumerable<UserInfo> GetUserByCode(string userCode)
        {
            string sql = @"
select 
	user.id,
    user.usercode,
    user.UserName,
    user.phone,
    user.email,
    user.sex,
    userrole.roleid
    
 from user
left join userrole
on user.id = userrole.userid
where user.status = 1 and userrole.status = 1 and usercode = @usercode
";
            return GetConn().Query<UserInfo>(sql, new { usercode = userCode });
        }
    }
}
