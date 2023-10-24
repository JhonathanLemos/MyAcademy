using System.Threading.Tasks;
using MyApi.Configuration.Dto;

namespace MyApi.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
