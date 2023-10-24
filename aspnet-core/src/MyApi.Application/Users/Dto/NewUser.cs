using Abp.AutoMapper;
using MyApi.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class NewUser
    {
        public string UserNameOrEmailAddress { get; set; }
        public string Password { get; set; }
    }
}
