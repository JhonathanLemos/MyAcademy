using System.Collections.Generic;
using Abp.Configuration;
using Abp.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace MyApi.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        private readonly IConfiguration configuration;

        public AppSettingProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {

                 new SettingDefinition(EmailSettingNames.DefaultFromAddress, configuration.GetValue("EmailSettings:Remetente", string.Empty)),
                new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, configuration.GetValue("EmailSettings:DefaultDisplayName", string.Empty)),
                new SettingDefinition(EmailSettingNames.Smtp.Host, configuration.GetValue("EmailSettings:Servidor", string.Empty)),
                new SettingDefinition(EmailSettingNames.Smtp.Port, configuration.GetValue("EmailSettings:Porta", string.Empty)),
                new SettingDefinition(EmailSettingNames.Smtp.UserName, configuration.GetValue("EmailSettings:Usuario", string.Empty)),
                new SettingDefinition(EmailSettingNames.Smtp.Password, configuration.GetValue("EmailSettings:Senha", string.Empty)),
                new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, configuration.GetValue("EmailSettings:SSL", "true")),
                new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, configuration.GetValue("EmailSettings:UsaCredenciaisPadrao", "false")),
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider())
            };
        }
    }
}
