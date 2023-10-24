using AutoMapper;
using MyApi.ExercicioTreinos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.ExercicioTreinos
{
    public class ExercicioTreinosMapProfile : Profile
    {
        public ExercicioTreinosMapProfile()
        {
            CreateMap<ExercicioTreino, ExercicioTreinoDto>();
            CreateMap<ExercicioTreinoDto, ExercicioTreino>();
        }
    }
}
