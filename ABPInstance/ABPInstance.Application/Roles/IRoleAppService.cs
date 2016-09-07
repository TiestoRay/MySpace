using System.Threading.Tasks;
using Abp.Application.Services;
using ABPInstance.Roles.Dto;

namespace ABPInstance.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
