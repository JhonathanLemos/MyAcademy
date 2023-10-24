using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Expressions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using MyApi.Authorization.Users;
using MyApi.Exercicios;
using MyApi.ExercicioTreinos;
using MyApi.GetAlls;
using MyApi.Treinos.Dto;
using MyApi.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Treinos
{
    public class TreinoAppService : AsyncCrudAppService<Treino, TreinoDto, long, GetAll, TreinoDto>
    {
        private readonly IRepository<Exercicio, long> _exerciceRepository;
        private readonly IRepository<ExercicioTreino, long> _exerciceTreinoRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IObjectMapper _objectMapper;
        public TreinoAppService(IRepository<Treino, long> repository, IRepository<Exercicio, long> exerciceRepository, IRepository<ExercicioTreino, long> exerciceTreinoRepository, IRepository<User, long> userRepository, IObjectMapper objectMapper) : base(repository)
        {
            _exerciceRepository = exerciceRepository;
            _exerciceTreinoRepository = exerciceTreinoRepository;
            _userRepository = userRepository;
            _objectMapper = objectMapper;
        }

        protected override IQueryable<Treino> CreateFilteredQuery(GetAll input)
        {

            var predicateSearch = PredicateBuilder.New<Treino>(true);

            if (input.Keyword != null)
                predicateSearch.And(x => x.Nome.Contains(input.Keyword) || x.User.Name.Contains(input.Keyword));

            return Repository.GetAllIncluding(x => x.User, x => x.Exercicio).Where(predicateSearch);
        }
        public async Task<List<ExercicioDto>> GetExercicesList()
        {
            var list = await _exerciceRepository.GetAll().ToListAsync();
            return _objectMapper.Map<List<ExercicioDto>>(list);
        }

        public async Task<List<UserDto>> GetUsersList()
        {
            var list = await _userRepository.GetAll().ToListAsync();
            return _objectMapper.Map<List<UserDto>>(list);
        }

        public override async Task<TreinoDto> CreateAsync(TreinoDto input)
        {
            //foreach (var item in treinoExercicio)
            //{
            //    await _exerciceTreinoRepository.InsertAsync(item);
            //}
            return await base.CreateAsync(input);
        }


        public async Task<List<TreinoDto>> GetTreinosByDataAndId(GetUserByCriacaoAndId input)
        {
            var list = await Repository.GetAll().Include(x => x.Exercicio).Where(x => x.UserId == Int64.Parse(input.Id) && x.Criacao.Year == input.Criacao.Year && x.Criacao.Month == input.Criacao.Month && x.Criacao.Day == input.Criacao.Day).ToListAsync();
            return _objectMapper.Map<List<TreinoDto>>(list);
        }
    }
}
