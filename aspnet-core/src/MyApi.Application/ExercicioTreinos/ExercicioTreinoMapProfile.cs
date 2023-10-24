using AutoMapper;
using MyApi.Authorization.Users;
using MyApi.ExercicioTreinos.Dto;
using MyApi.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.ExercicioTreinos
{
    public class ExercicioTreinoMapProfile : Profile
    {
        public ExercicioTreinoMapProfile()
        {
            CreateMap<ExercicioTreinoDto, ExercicioTreino>();
            CreateMap<ExercicioTreino, ExercicioTreinoDto>();
        }
    }
}
