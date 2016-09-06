using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPInstance.Authorization
{
    /// <summary>
    /// 权限树
    /// </summary>
    public class PermissionTree
    {
        /// <summary>
        /// 权限名称(唯一)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 子级权限
        /// </summary>
        public List<PermissionTree> Child { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PermissionTree()
        {
            Child = new List<PermissionTree>();
        }
    }
}
