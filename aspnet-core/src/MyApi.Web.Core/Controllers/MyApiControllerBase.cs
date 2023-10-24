using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyApi.Controllers
{
    public abstract class MyApiControllerBase: AbpController
    {
        protected MyApiControllerBase()
        {
            LocalizationSourceName = MyApiConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
