using Abp.Domain.Entities;
using MyApi.Authorization.Users;
using MyApi.Exercicios;
using MyApi.ExercicioTreinos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Treinos
{
    public class Treino : Entity<long>
    {
        public string Nome { get; set; }
        public int Repeticoes { get; set; }
        public int Series { get; set; }
        public long ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime Criacao { get; set; }
        public bool IsCreated { get; set; }

    }
}
