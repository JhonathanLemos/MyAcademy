using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace MyApi.Customers
{
    public class Customer : FullAuditedEntity<long>
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Cnpj { get; set; }
    }
}
