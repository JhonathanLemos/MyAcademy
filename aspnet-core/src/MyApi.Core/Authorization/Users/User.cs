using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using MyApi.Enums;
using MyApi.Informacoes;
using MyApi.Treinos;
using MyApi.UsuarioDados;

namespace MyApi.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public byte[] Imagem { get; set; }
        public EnumTipoSexo Sexo { get; set; }
        public string Cidade { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
        public long InformacaoId { get; set; }
        public Informacao Informacao { get; set; }
        public List<Treino> Treinos { get; set; }
        public UsuarioDado UsuarioDado { get; set; }
        public long UsuarioDadoId { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
