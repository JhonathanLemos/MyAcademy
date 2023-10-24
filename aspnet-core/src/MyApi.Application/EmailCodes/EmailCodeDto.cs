using Abp.Application.Services.Dto;

namespace MyApi.EmailCodes
{
    public class EmailCodeDto : EntityDto<long>
    {
        public string Code { get; set; }
        public long UserId { get; set; }
    }
}
