using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using MyApi.Customers;
using MyApi.GetAlls;
using MyApi.Customers.Dto;

namespace MyApi.Tests.Customers
{
    public class CustomerAppService_Tests : MyApiTestBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerAppService_Tests()
        {
            _customerAppService = Resolve<ICustomerAppService>();
        }

        [Fact]
        public async Task GetCustomers_Test()
        {
            await CreateCustomers_Test();
            // Act
            var output = await _customerAppService.GetAllAsync((new GetAll { MaxResultCount = 20, SkipCount = 0 }));

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateCustomers_Test()
        {
            // Act
            await _customerAppService.CreateAsync(
                new CreateCustomerDto
                {
                    Idade = 18,
                    Cnpj = "77.434.578/0001-80",
                    Nome = "Cliente 1",
                    Telefone = "999999999"
                });

            await UsingDbContextAsync(async context =>
            {
                var customer = await context.Customers.FirstOrDefaultAsync(u => u.Nome == "Cliente 1");
                customer.ShouldNotBeNull();
            });
        }
    }
}
