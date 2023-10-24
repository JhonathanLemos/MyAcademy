using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using MyApi.GetAlls;
using MyApi.Informacoes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.UsuarioDados
{
    public class UsuarioDadoAppService : AsyncCrudAppService<UsuarioDado, UsuarioDadoDto, long, GetAll, UsuarioDadoDto>
    {
        private readonly IObjectMapper _mapper;
        public UsuarioDadoAppService(IRepository<UsuarioDado, long> repository, IObjectMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<UsuarioDadoDto> CreateOrUpdate(UsuarioDadoDto usuarioDadoDto)
        {
            var map = _mapper.Map<UsuarioDado>(usuarioDadoDto);

            var exists = await Repository.GetAll().Where(x => x.UserId == usuarioDadoDto.UserId).FirstOrDefaultAsync();
            if (exists != null)
                await DeleteAsync(new EntityDto<long>(exists.Id));

            var result = await Repository.InsertAsync(map);

            return _mapper.Map<UsuarioDadoDto>(result);
        }

        public async Task<UsuarioDadoDto> GetByUserId(EntityDto<long> input)
        {
            var user = await Repository.GetAll().Where(x => x.UserId == input.Id).FirstOrDefaultAsync();
            return _mapper.Map<UsuarioDadoDto>(user);
        }


    }
}
