using System.ComponentModel.DataAnnotations;

namespace MyApi.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}