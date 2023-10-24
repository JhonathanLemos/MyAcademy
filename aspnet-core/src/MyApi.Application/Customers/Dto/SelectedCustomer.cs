using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace MyApi.Customers.Dto
{
    public class SelectedCustomerDto
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
