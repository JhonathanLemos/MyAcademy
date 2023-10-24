using Abp.Domain.Entities;
using MyApi.Exercicios;
using MyApi.Treinos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.ExercicioTreinos
{
    public class ExercicioTreino : Entity<long>
    {
        public long ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }

        public long TreinoId { get; set; }
        public Treino Treino { get; set; }
    }
}
