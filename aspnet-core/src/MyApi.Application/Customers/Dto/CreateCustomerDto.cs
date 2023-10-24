using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace MyApi.Customers.Dto
{
    [AutoMapFrom(typeof(Customer))]
    public class CreateCustomerDto : FullAuditedEntityDto<long>
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Cnpj { get; set; }
    }
}
