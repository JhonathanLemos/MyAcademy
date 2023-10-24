using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Users.Dto
{
    public class GetUserByCriacaoAndId
    {
        public DateTime Criacao { get; set; }
        public string Id { get; set; }
    }
}
