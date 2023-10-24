using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using MyApi.Authorization.Users;

namespace MyApi.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class CreateUserDto : IShouldNormalize
    {
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }
        public string Cpf { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }
        public string DataNascimento { get; set; }

        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public void Normalize()
        {
            if (RoleNames == null)
            {
                RoleNames = new string[0];
            }
        }
    }
}
