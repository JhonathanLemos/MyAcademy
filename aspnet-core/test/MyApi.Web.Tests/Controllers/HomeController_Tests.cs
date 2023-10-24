using System.Threading.Tasks;
using MyApi.Models.TokenAuth;
using MyApi.Web.Controllers;
using Shouldly;
using Xunit;

namespace MyApi.Web.Tests.Controllers
{
    public class HomeController_Tests: MyApiWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}