using AutoMapper;
using MyApi.Authorization.Users;

namespace MyApi.Users.Dto
{
    public class InformacaoMapProfile : Profile
    {
        public InformacaoMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<UserDto, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
