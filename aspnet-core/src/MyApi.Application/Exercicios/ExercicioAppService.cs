using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using MyApi.GetAlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Exercicios
{
    public class ExercicioAppService : AsyncCrudAppService<Exercicio, ExercicioDto, long, GetAll, ExercicioDto>
    {
        public ExercicioAppService(IRepository<Exercicio, long> repository) : base(repository)
        {
        }

        protected override IQueryable<Exercicio> CreateFilteredQuery(GetAll input)
        {
            return Repository.GetAll().WhereIf(!string.IsNullOrEmpty(input.Keyword), x => x.Nome.Contains(input.Keyword));
        }
    }
}
