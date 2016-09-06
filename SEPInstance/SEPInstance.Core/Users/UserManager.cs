using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using SEPInstance.Authorization.Roles;
using SEPInstance.MultiTenancy;
using SEPInstance.Func;

namespace SEPInstance.Users
{
    public class UserManager : AbpUserManager<Tenant, Role, User>
    {
        public UserManager(
            UserStore store,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository
            )
            : base(
                store,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                userLoginAttemptRepository)
        {
        }

        /// <summary>
        /// 创建验证码字符
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {
            string code = ValidateCodeFunc.CreateValidateCode(length);
            return code;
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="code">图片中的验证码字符</param>
        /// <returns></returns>
        public byte[] CreateValidateCodePic(string code)
        {
            var bytes = Func.ValidateCodeFunc.CreateValidateCodePic(code);
            return bytes;
        }

    }
}