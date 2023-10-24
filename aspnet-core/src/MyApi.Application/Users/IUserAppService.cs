using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyApi.GetAlls;
using MyApi.Roles.Dto;
using MyApi.Users.Dto;

namespace MyApi.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, GetAll, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
        Task GenerateCodeToValidateUser(UserDto user);
    }
}
