using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using MyApi.EntityFrameworkCore.Seed.Host;
using MyApi.EntityFrameworkCore.Seed.Tenants;
using MyApi.Authorization.Roles;

namespace MyApi.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            var roleManager = iocResolver.Resolve<RoleManager>();
            WithDbContext<MyApiDbContext>(iocResolver, SeedHostDb);
            WithDbContext<MyApiDbContext>(iocResolver, context => SeedHostDb(roleManager));
        }

        public static void SeedHostDb(MyApiDbContext context)
        {
            context.SuppressAutoSetTenantId = true;
            // Host seed
            new InitialHostDbBuilder(context).Create();

            // Default tenant seed (in host database).
            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();
        }

        public static void SeedHostDb(RoleManager roleManager)
        {
            new MyApiRoleBuilder(roleManager).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
