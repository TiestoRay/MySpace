using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using SEPInstance.MultiTenancy;
using SEPInstance.Users;
using SEPInstance.Authorization.Roles;
using SEPInstance.Extensions;
using SEPInstance.Dto.InputDto.Sessions;

namespace SEPInstance
{
    /// <summary>
    /// 共通接口的实现
    /// 继承自ApplicationService
    /// </summary>
    public abstract class SEPInstanceAppServiceBase : ApplicationService
    {
        /// <summary>
        /// 属性注入租户管理者
        /// </summary>
        public TenantManager TenantManager { get; set; }

        /// <summary>
        /// 属性注入用户管理者
        /// </summary>
        public UserManager UserManager { get; set; }

        /// <summary>
        /// 属性注入角色管理者
        /// </summary>
        public RoleManager RoleManager { get; set; }

        /// <summary>
        /// 用户信息扩展
        /// </summary>
        public IUserExtension _UserExtension { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected SEPInstanceAppServiceBase()
        {
            LocalizationSourceName = SEPInstanceConsts.LocalizationSourceName;
        }

        /// <summary>
        /// 自动检查结果并将错误信息抛出
        /// </summary>
        /// <param name="identityResult">IdentityResult类</param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public UserLoginInfoDto GetCurrentUser() {
            return _UserExtension.GetCurrentUser();
        }

        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public long GetCurrentUserId()
        {
            return _UserExtension.GetCurrentUserId();
        }

        /// <summary>
        /// 判断当前用户是否已登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            return _UserExtension.IsLogin();
        }
    }
}