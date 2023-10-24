using Abp.Domain.Entities;
using MyApi.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.UsuarioDados
{
    public class UsuarioDado : Entity<long>
    {
        public string Cep { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
    }
}
