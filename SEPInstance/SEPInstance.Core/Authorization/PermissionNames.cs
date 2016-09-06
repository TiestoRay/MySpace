using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace SEPInstance.Authorization
{
    public static class PermissionNames
    {
        public const string Pages = "Pages";

        public const string Pages_Tenants = "Pages.Tenants";

        #region 首页

        /// <summary>
        /// 首页
        /// </summary>
        public const string Pages_Home = "Pages.Home";

        /// <summary>
        /// 首页
        /// </summary>
        public const string Pages_Home_Index = "Pages.Home.Index";

        /// <summary>
        /// 关于
        /// </summary>
        public const string Pages_Home_About = "Pages.Home.About";

        #endregion

        #region 用户
        /// <summary>
        /// 用户
        /// </summary>
        public const string Pages_Users = "Pages.Users";

        /// <summary>
        /// 用户列表
        /// </summary>
        public const string Pages_Users_List = "Pages.Users.List";

        /// <summary>
        /// 用户列表分页
        /// </summary>
        public const string Pages_Users_List_Pager = "Pages.Users.List.Pager";

        /// <summary>
        /// 用户列表的面包屑
        /// </summary>
        public const string Pages_Users_List_BreadCrumb = "Pages.Users.List.BreadCrumb";

        /// <summary>
        /// 用户新增
        /// </summary>
        public const string Pages_Users_Add = "Pages.Users.Add";

        /// <summary>
        /// 用户修改
        /// </summary>
        public const string Pages_Users_Edit = "Pages.Users.Edit";

        /// <summary>
        /// 用户删除
        /// </summary>
        public const string Pages_Users_Del = "Pages.Users.Del";

        /// <summary>
        /// 用户设置角色
        /// </summary>
        public const string Pages_Users_Role = "Pages.Users.Role";

        #endregion

        #region 角色及权限
        /// <summary>
        /// 角色
        /// </summary>
        public const string Pages_Roles = "Pages.Roles";

        /// <summary>
        /// 角色列表
        /// </summary>
        public const string Pages_Roles_List = "Pages.Roles.List";

        /// <summary>
        /// 角色新增
        /// </summary>
        public const string Pages_Roles_Add = "Pages.Roles.Add";

        /// <summary>
        /// 角色修改
        /// </summary>
        public const string Pages_Roles_Edit = "Pages.Roles.Edit";

        /// <summary>
        /// 角色删除
        /// </summary>
        public const string Pages_Roles_Del = "Pages.Roles.Del";

        /// <summary>
        /// 角色权限分配
        /// </summary>

        public const string Pages_Roles_Authorization = "Pages.Roles.Authorization";
        #endregion

        #region 日志
        /// <summary>
        /// 日志
        /// </summary>
        public const string Pages_Log = "Pages.Log";

        /// <summary>
        /// 日志列表
        /// </summary>
        public const string Pages_Log_List = "Pages.Log.List";

        #endregion

        /// <summary>
        /// 获取所有权限（初始化）
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAuthorization()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var type = typeof(PermissionNames);
            var p = type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            foreach (FieldInfo field in p)
            {
                if (dic.ContainsKey(field.Name) || dic.ContainsValue(field.GetValue(null).ToString()))
                {
                    throw new Exception("设置权限时出现重复值" + field.Name);
                }
                dic.Add(field.Name, field.GetValue(null).ToString());
            }
            return dic;
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <returns>权限树列表</returns>
        public static List<PermissionTree> GetAuthorizationTree()
        {
            List<PermissionTree> tree = new List<PermissionTree>();

            #region 首页
            tree.Add(
                new PermissionTree
                {
                    Name = Pages_Home,
                    DisplayName = "首页",
                    Child = new List<PermissionTree>{
                        new PermissionTree{
                            Name=Pages_Home_Index,
                            DisplayName="首页"
                        },
                        new PermissionTree{
                            Name=Pages_Home_About,
                            DisplayName="关于"
                        }
                    }
                }
           );
            #endregion

            #region 用户
            tree.Add(
                new PermissionTree
                {
                    Name = Pages_Users,
                    DisplayName = "用户",
                    Child = new List<PermissionTree>{
                        new PermissionTree{
                            Name=Pages_Users_List,
                            DisplayName="用户列表",
                            Child=new List<PermissionTree>{
                                new PermissionTree{
                                    Name=Pages_Users_List_Pager,
                                    DisplayName="用户列表分页"
                                },
                                new PermissionTree{
                                    Name=Pages_Users_List_BreadCrumb,
                                    DisplayName="用户列表面包屑"
                                }
                            }
                        },
                        new PermissionTree{
                            Name=Pages_Users_Add,
                            DisplayName="新增用户"
                        },
                        new PermissionTree{
                            Name=Pages_Users_Edit,
                            DisplayName="编辑用户"
                        },
                        new PermissionTree{
                            Name=Pages_Users_Del,
                            DisplayName="删除用户"
                        },
                        new PermissionTree{
                            Name=Pages_Users_Role,
                            DisplayName="角色分配"
                        }
                    }
                }
            );
            #endregion

            #region 角色
            tree.Add(
               new PermissionTree
               {
                   Name = Pages_Roles,
                   DisplayName = "角色",
                   Child = new List<PermissionTree>{
                        new PermissionTree{
                            Name=Pages_Roles_List,
                            DisplayName="角色列表"
                        },
                        new PermissionTree{
                            Name=Pages_Roles_Add,
                            DisplayName="新增角色"
                        },
                        new PermissionTree{
                            Name=Pages_Roles_Edit,
                            DisplayName="编辑角色"
                        },
                        new PermissionTree{
                            Name=Pages_Roles_Del,
                            DisplayName="删除角色"
                        },
                        new PermissionTree{
                            Name=Pages_Roles_Authorization,
                            DisplayName="角色权限分配"
                        }
                    }
               }
           );
            #endregion

            #region 日志
            tree.Add(
                new PermissionTree
                {
                    Name = Pages_Log,
                    DisplayName = "日志",
                    Child = new List<PermissionTree>{
                        new PermissionTree{
                            Name=Pages_Log_List,
                            DisplayName="日志"
                        }
                    }
                }
           );
            #endregion

            return tree;
        }

        /// <summary>
        /// 根据权限(Name)获取权限显示名称(DisplayName)
        /// </summary>
        /// <param name="name">权限名</param>
        /// <returns></returns>
        public static string GetAuthorizationDisplayName(string name)
        {
            var trees = GetAuthorizationTree();
            foreach (var tree in trees)
            {
                if (tree.Name == name)
                    return tree.DisplayName;
                foreach (var item in tree.Child)
                {
                    if (item.Name == name)
                        return item.DisplayName;
                    if (item.Child != null)
                    {
                        foreach (var child in item.Child)
                        {
                            if (child.Name == name)
                                return child.DisplayName;
                        }
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// 根据权限(Name)获取权限上级名称(Name或null)
        /// </summary>
        /// <param name="name">权限名</param>
        /// <returns></returns>
        public static string[] GetAuthorizationParentName(string name)
        {
            var trees = GetAuthorizationTree();
            foreach (var tree in trees)
            {
                if (tree.Name == name)
                    return null;
                foreach (var item in tree.Child)
                {
                    if (item.Name == name)
                        return new[] { tree.Name };
                    foreach (var child in item.Child)
                    {
                        if (child.Name == name)
                            return new[] { tree.Name, item.Name };
                    }
                }
            }
            return null;
        }
    }
}