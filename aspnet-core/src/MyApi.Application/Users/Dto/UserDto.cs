using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using MyApi.Authorization.Users;
using MyApi.Enums;
using MyApi.Treinos;
using MyApi.Treinos.Dto;

namespace MyApi.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }
        public EnumTipoSexo Sexo { get; set; }
        public string Cidade { get; set; }
        public string PhoneNumber { get; set; }
        public List<TreinoDto> Treinos { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }
        public byte[] Imagem { get; set; }
        public long InformacaoId { get; set; }

        public long UsuarioDadoId { get; set; }

        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        public string DataNascimento { get; set; }

        public string EmailConfirmationCode { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public string Cpf { get; set; }

    }
}
