using Abp.Authorization;
using MyApi.Authorization.Roles;
using MyApi.Authorization.Users;

namespace MyApi.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
