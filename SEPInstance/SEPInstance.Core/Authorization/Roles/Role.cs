using Abp.Authorization.Roles;
using SEPInstance.Users;
using System.ComponentModel.DataAnnotations;

namespace SEPInstance.Authorization.Roles
{
    public class Role : AbpRole<User>
    {
        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(50)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 角色描述最大长度
        /// </summary>
        public const int MaxDescriptionLength = 50;

        public Role()
        {

        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}