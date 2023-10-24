using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Users.Dto
{
    public class UserImagemDto
    {
        public string UserId { get; set; }
        public byte[] Imagem { get; set; }
    }
}
