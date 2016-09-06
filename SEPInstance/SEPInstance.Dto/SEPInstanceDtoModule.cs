using System.Reflection;
using Abp.Modules;
using Abp.AutoMapper;
using SEPInstance.Users;
using SEPInstance.Dto.InputDto.Users;
using SEPInstance.Authorization.Roles;
using SEPInstance.Dto.OutputDto.Roles;
using SEPInstance.Dto.InputDto.Roles;

namespace SEPInstance
{
    [DependsOn(typeof(SEPInstanceCoreModule))]
    public class SEPInstanceDtoModule : AbpModule
    {
        public override void PreInitialize()
        {
            //用户
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {

                mapper.CreateMap<User, EditUserInput>().ForMember(opt => opt.Email, opt => opt.MapFrom(s => s.EmailAddress))
                    .ForMember(opt => opt.UserId, opt => opt.MapFrom(s => s.Id));
            });

            //角色
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                mapper.CreateMap<Role, RoleListDto>()
                    .ForMember(m => m.CreateUserName, m => m.MapFrom(n => n.CreatorUser.Name))
                    .ForMember(m => m.LastModifierUserName, m => m.MapFrom(n => n.LastModifierUser.Name));
            });
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                mapper.CreateMap<CreateRoleInput, Role>()
                    .ForMember(m => m.DisplayName, m => m.MapFrom(n => n.Name));
            });
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
