using Abp.Domain.Entities;
using MyApi.ExercicioTreinos;
using MyApi.Treinos;
using System.Collections.Generic;

namespace MyApi.Exercicios;

public class Exercicio : Entity<long>
{
    public string Nome { get; set; }

    public List<Treino> Treinos { get; set; }
}
