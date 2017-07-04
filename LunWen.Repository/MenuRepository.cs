﻿using System;
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
    public class MenuRepository : MySqlBaseRepository<Menu>
    {
        public IEnumerable<Menu> GetByRole(int roleId)
        {
            string sql = @"
SELECT 
    *
FROM
    rolemenu
        LEFT JOIN
    menu ON menu.id = rolemenu.menuid AND roleid = @roleid
WHERE
    menu.status = 1 AND rolemenu.status = 1
";

          return  GetConn().Query<Menu>(sql, new { roleid = roleId });

        }
    }
}
