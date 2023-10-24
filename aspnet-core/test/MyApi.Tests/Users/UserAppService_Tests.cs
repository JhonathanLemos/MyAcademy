using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Abp.Application.Services.Dto;
using MyApi.Users;
using MyApi.Users.Dto;
using MyApi.GetAlls;

namespace MyApi.Tests.Users
{
    public class UserAppService_Tests : MyApiTestBase
    {
        private readonly IUserAppService _userAppService;

        public UserAppService_Tests()
        {
            _userAppService = Resolve<IUserAppService>();
        }

        [Fact]
        public async Task GetUsers_Test()
        {
            // Act
            var output = await _userAppService.GetAllAsync(new GetAll { MaxResultCount = 20, SkipCount = 0 });

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateUser_Test()
        {
            // Act
            await _userAppService.CreateAsync(
                new CreateUserDto
                {
                    EmailAddress = "john@volosoft.com",
                    IsActive = true,
                    Name = "John",
                    Surname = "Nash",
                    Password = "123qwe",
                    UserName = "john"
                });

            await UsingDbContextAsync(async context =>
            {
                var a = await context.Users.LastOrDefaultAsync();
                var johnNashUser = await context.Users.FirstOrDefaultAsync(u => u.Name == "John");
                johnNashUser.ShouldNotBeNull();
            });
        }
    }
}
