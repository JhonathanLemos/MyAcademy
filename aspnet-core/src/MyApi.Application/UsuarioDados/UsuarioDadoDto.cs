using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.UsuarioDados
{
    [AutoMap(typeof(UsuarioDado))]
    public class UsuarioDadoDto : EntityDto<long>
    {
        public string Cep { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public long UserId { get; set; }
    }
}
