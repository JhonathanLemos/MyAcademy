using System.ComponentModel.DataAnnotations;

namespace MyApi.Users.Dto
{
    public class ResetPasswordDto
    {
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
