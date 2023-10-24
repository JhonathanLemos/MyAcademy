using Abp.AutoMapper;
using AutoMapper;
using MyApi.Customers.Dto;
using MyApi.EmailCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.EmailCodes.Mapper
{
    public class EmailCodeMapProfile : Profile
    {
        public EmailCodeMapProfile()
        {
            CreateMap<EmailCodeDto, EmailCode>();
            CreateMap<EmailCode, EmailCodeDto>();
        }
    }
}
