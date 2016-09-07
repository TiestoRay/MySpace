using Abp.Authorization;
using ABPInstance.Authorization.Roles;
using ABPInstance.MultiTenancy;
using ABPInstance.Users;

namespace ABPInstance.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
