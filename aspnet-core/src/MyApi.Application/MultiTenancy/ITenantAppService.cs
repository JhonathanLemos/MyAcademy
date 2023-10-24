using Abp.Application.Services;
using MyApi.MultiTenancy.Dto;

namespace MyApi.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

