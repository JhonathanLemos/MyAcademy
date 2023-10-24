
using Abp.Application.Services;
using MyApi.Customers.Dto;
using MyApi.GetAlls;

namespace MyApi.Customers
{
    public interface ICustomerAppService : IAsyncCrudAppService<CustomerDto, long, GetAll, CreateCustomerDto>
    {

    }
}
