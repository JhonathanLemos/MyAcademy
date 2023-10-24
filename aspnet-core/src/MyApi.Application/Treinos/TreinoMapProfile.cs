using AutoMapper;
using MyApi.Authorization.Users;
using MyApi.Treinos.Dto;
using MyApi.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Treinos
{
    public class TreinoMapProfile : Profile
    {
        public TreinoMapProfile()
        {
            CreateMap<TreinoDto, Treino>();
            CreateMap<Treino, TreinoDto>()
                .ForMember(opt => opt.UsuarioNome, opt => opt.MapFrom(x => x.User.Name))
                .ForMember(opt => opt.ExercicioNome, opt => opt.MapFrom(x => x.Exercicio.Nome));
        }
    }
}
