
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using MyApi.Authorization;
using MyApi.Customers.Dto;
using MyApi.EmailCodes;
using MyApi.GetAlls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.EmailCOdes
{
    public class EmailCodeAppService : AsyncCrudAppService<EmailCode, EmailCodeDto, long, GetAll, EmailCodeDto>
    {
        private readonly IObjectMapper _objectMapper;

        public EmailCodeAppService(IRepository<EmailCode, long> repository, IObjectMapper objectMapper) : base(repository)
        {
            _objectMapper = objectMapper;
        }


    }
}
