
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MyApi.Authorization;
using MyApi.Customers.Dto;
using MyApi.GetAlls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Customers
{
    [AbpAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, long, GetAll, CreateCustomerDto>, ICustomerAppService
    {
        private readonly Abp.ObjectMapping.IObjectMapper _objectMapper;
        public CustomerAppService(IRepository<Customer, long> repository, Abp.ObjectMapping.IObjectMapper objectMapper) : base(repository)
        {
            _objectMapper = objectMapper;
        }

        protected override IQueryable<Customer> CreateFilteredQuery(GetAll input)
        {
            return Repository.GetAll().WhereIf(!string.IsNullOrEmpty(input.Keyword), x => x.Nome.Contains(input.Keyword));
        }

        [AbpAuthorize(AdministrativePermissionActions.Administrative_Customer_Edit)]
        public override Task<CustomerDto> CreateAsync(CreateCustomerDto input)
        {
            return base.CreateAsync(input);
        }

        [AbpAuthorize(AdministrativePermissionActions.Administrative_Customer_Edit)]
        public override Task<CustomerDto> UpdateAsync(CreateCustomerDto input)
        {
            return base.UpdateAsync(input);
        }

        public async Task<List<SelectedCustomerDto>> SelectCustomers()
        {
            List<Customer> customers = await Repository.GetAll().ToListAsync();
            return _objectMapper.Map<List<SelectedCustomerDto>>(customers);
        }
    }
}
