using System.Threading.Tasks;
using Abp.Application.Services;
using MyApi.Authorization.Accounts.Dto;

namespace MyApi.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
