using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.GetAlls
{
    public class GetAll : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Date { get; set; }
    }
}
