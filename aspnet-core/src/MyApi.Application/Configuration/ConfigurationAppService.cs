using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MyApi.Configuration.Dto;

namespace MyApi.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MyApiAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
