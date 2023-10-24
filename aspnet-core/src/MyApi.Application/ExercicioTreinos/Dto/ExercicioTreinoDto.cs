using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyApi.Exercicios;
using MyApi.Treinos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.ExercicioTreinos.Dto
{
    [AutoMapFrom(typeof(ExercicioTreino))]
    public class ExercicioTreinoDto : EntityDto<long>
    {
        public long ExercicioId { get; set; }
        public long TreinoId { get; set; }
    }
}
