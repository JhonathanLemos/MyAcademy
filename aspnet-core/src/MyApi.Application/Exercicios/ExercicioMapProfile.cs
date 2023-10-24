using AutoMapper;
using MyApi.Authorization.Users;
using MyApi.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Exercicios
{
    public class ExercicioMapProfile : Profile
    {
        public ExercicioMapProfile()
        {
            CreateMap<ExercicioDto, Exercicio>();
            CreateMap<Exercicio, ExercicioDto>();
        }
    }
}
