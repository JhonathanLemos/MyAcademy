using Abp.AutoMapper;
using AutoMapper;
using MyApi.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Customers.Mapper
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<Customer, SelectedCustomerDto>()
                .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Nome))
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Id));
        }
    }
}
