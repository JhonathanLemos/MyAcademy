using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using MyApi.Authorization.Users;
using MyApi.GetAlls;
using MyApi.Informacoes.Dto;
using MyApi.Users;
using MyApi.UsuarioDados;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Informacoes
{
    public class InformacaoAppService : AsyncCrudAppService<Informacao, InformacaoDto, long, GetAll, InformacaoDto>
    {
        private readonly IUserAppService _userRepository;
        private readonly IObjectMapper _mapper;
        public InformacaoAppService(IRepository<Informacao, long> repository, IUserAppService userRepository, IObjectMapper mapper) : base(repository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [AbpAuthorize]
        public override async Task<InformacaoDto> CreateAsync(InformacaoDto input)
        {
            var existeInformacao = await Repository.GetAll().Where(x => x.UserId == input.UserId).FirstOrDefaultAsync();
            if (existeInformacao != null)
                await Repository.DeleteAsync(existeInformacao);

            return await base.CreateAsync(input);
        }
        public async Task<bool> VerifyIfInformacaoIsEnded()
        {
            var result = await Repository.GetAll().Where(x => x.UserId == AbpSession.GetUserId()).FirstOrDefaultAsync();
            if (result != null)
            {
                if (result.Finalizado)
                    return true;
            }
            return false;
        }

        public async Task SetAsFinalizado()
        {
            var result = await Repository.GetAll().Where(x => x.UserId == AbpSession.GetUserId()).FirstOrDefaultAsync();
            result.Finalizado = true;
            var user = await _userRepository.GetAsync(new EntityDto<long>() { Id = result.UserId });
            user.RoleNames = new string[] { "Admin" };
            await _userRepository.UpdateAsync(user);
            await Repository.UpdateAsync(result);
        }

        public async Task<InformacaoDto> CreateOrUpdate(InformacaoDto informacaoDto)
        {
            var map = _mapper.Map<Informacao>(informacaoDto);
            var exists = await Repository.GetAll().Where(x => x.UserId == informacaoDto.UserId).FirstOrDefaultAsync();

            if (exists != null)
                await Repository.DeleteAsync(exists.Id);

            var result = await Repository.InsertAsync(map);

            return _mapper.Map<InformacaoDto>(result);
        }

        public async Task<InformacaoDto> GetByUserId(EntityDto<long> input)
        {
            var user = await Repository.GetAll().Where(x => x.UserId == input.Id).FirstOrDefaultAsync();
            return _mapper.Map<InformacaoDto>(user);
        }
    }
}
