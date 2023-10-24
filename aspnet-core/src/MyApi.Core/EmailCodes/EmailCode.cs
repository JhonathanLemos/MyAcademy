using Abp.Domain.Entities;
using MyApi.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.EmailCodes
{
    public class EmailCode : Entity<long>
    {
        public string Code { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
