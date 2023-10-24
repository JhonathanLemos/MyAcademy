using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyApi.ExercicioTreinos.Dto;
using MyApi.Treinos.Dto;
using System.Collections.Generic;

namespace MyApi.Exercicios
{
    [AutoMapFrom(typeof(Exercicio))]
    public class ExercicioDto : EntityDto<long>
    {
        public string Nome { get; set; }

        public List<TreinoDto> Treinos { get; set; }
    }
}
