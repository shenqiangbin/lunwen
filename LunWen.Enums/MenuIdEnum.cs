using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Enums
{
    /// <summary>
    /// 菜单id的枚举
    /// 用途：当前菜单高亮显示
    /// </summary>
    public enum MenuIdEnum
    {
        /// <summary>
        /// 首页
        /// </summary>
        ManagerIndex = 1,

        /// <summary>
        /// 论文管理
        /// </summary>
        ThesisMgrIndex = 2,

        /// <summary>
        /// 费用管理
        /// </summary>
        CostMgrIndex = 3,

        /// <summary>
        /// 通知管理
        /// </summary>
        NoticeMgrIndex = 4,

        /// <summary>
        /// 架构管理
        /// </summary>
        OrgMgrIndex = 5,

        /// <summary>
        /// 用户管理
        /// </summary>
        UserIndex = 10,
    }
}
