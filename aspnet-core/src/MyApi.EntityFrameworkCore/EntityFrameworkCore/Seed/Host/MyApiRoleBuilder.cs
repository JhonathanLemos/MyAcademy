using System.Linq;
using MyApi.Authorization;
using MyApi.Authorization.Roles;
using System.Collections.ObjectModel;
using Abp.Authorization.Roles;
using System.Collections;
using System.Collections.Generic;
using Abp.Authorization;
using System.Collections.Immutable;

namespace MyApi.EntityFrameworkCore.Seed.Host
{
    public class MyApiRoleBuilder
    {
        private readonly RoleManager _roleManager;

        public MyApiRoleBuilder(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public void Create()
        {
            CreateHostRoleAndUsers("Customer", new List<Permission>() {
                new Permission(PermissionNames.Pages_Customers),
                new Permission(AdministrativePermissionActions.Administrative_Customer_See),
                new Permission(AdministrativePermissionActions.Administrative_Customer_Edit),});
            CreateHostRoleAndUsers("Informacao", new List<Permission>() {
                new Permission(PermissionNames.Pages_Informacoes),
                new Permission(AdministrativePermissionActions.Administrative_Informacao_Edit),
                new Permission(AdministrativePermissionActions.Administrative_Informacao_See) });
        }

        private void CreateHostRoleAndUsers(string entidade, List<Permission> list)
        {
            var roleExists = _roleManager.FindByNameAsync(entidade).Result;

            if (roleExists == null)
            {
                var newRole = new Role() { Name = entidade, DisplayName = entidade };
                var result = _roleManager.CreateAsync(newRole).Result;
                if (result.Succeeded)
                {
                    var createdRole = _roleManager.GetRoleByIdAsync(newRole.Id).Result;
                    _roleManager.SetGrantedPermissionsAsync(createdRole.Id, list);
                }
            }
        }


    }
}
