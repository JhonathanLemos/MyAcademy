using Abp.AutoMapper;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using MyApi.Authorization;

namespace MyApi
{
    [DependsOn(
        typeof(MyApiCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MyApiApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyApiAuthorizationProvider>();
            Configuration.Modules.Zero().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(RoleNames.RoleUser, MultiTenancySides.Host | MultiTenancySides.Tenant));
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MyApiApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
