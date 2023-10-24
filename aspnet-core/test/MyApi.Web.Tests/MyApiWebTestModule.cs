using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyApi.EntityFrameworkCore;
using MyApi.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MyApi.Web.Tests
{
    [DependsOn(
        typeof(MyApiWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class MyApiWebTestModule : AbpModule
    {
        public MyApiWebTestModule(MyApiEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyApiWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(MyApiWebMvcModule).Assembly);
        }
    }
}