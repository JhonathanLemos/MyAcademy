using System.Threading.Tasks;
using Abp.Application.Services;
using MyApi.Sessions.Dto;

namespace MyApi.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
