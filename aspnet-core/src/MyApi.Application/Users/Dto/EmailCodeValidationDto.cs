using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Users.Dto
{
    public class EmailCodeValidationDto
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
