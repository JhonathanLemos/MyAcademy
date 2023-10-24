using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using MyApi.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Migrator
{
    public class CreationRoles
    {
        private readonly RoleManager _roleManager;

        public CreationRoles(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CriarRoles()
        {
            if(!await _roleManager.RoleExistsAsync(StaticRoleNames.Host.User))
            {
                var userRoles = new Role(1, StaticRoleNames.Host.User);
                await _roleManager.CreateAsync(userRoles);
            }
        }
    }
}
