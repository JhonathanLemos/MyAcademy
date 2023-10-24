using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyApi.Authorization.Users;
using MyApi.Exercicios;
using MyApi.ExercicioTreinos;
using MyApi.ExercicioTreinos.Dto;
using System;
using System.Collections.Generic;

namespace MyApi.Treinos.Dto
{
    [AutoMapFrom(typeof(Treino))]
    public class TreinoDto : EntityDto<long>
    {
        public string Nome { get; set; }
        public int Repeticoes { get; set; }
        public int Series { get; set; }
        public long ExercicioId { get; set; }
        public string ExercicioNome { get; set; }
        public string UsuarioNome { get; set; }
        public long UserId { get; set; }
        public DateTime Criacao { get; set; }
        public bool IsCreated { get; set; }

    }
}
