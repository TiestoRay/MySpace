using Abp.Authorization;
using SEPInstance.Authorization.Roles;
using SEPInstance.MultiTenancy;
using SEPInstance.Users;

namespace SEPInstance.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
