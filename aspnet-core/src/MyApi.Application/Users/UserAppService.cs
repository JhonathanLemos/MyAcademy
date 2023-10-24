using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using MyApi.Authorization;
using MyApi.Authorization.Accounts;
using MyApi.Authorization.Roles;
using MyApi.Authorization.Users;
using MyApi.Roles.Dto;
using MyApi.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MyApi.Identidade;
using Abp.ObjectMapping;
using MyApi.EmailCodes;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core.Tokenizer;
using MyApi.GetAlls;
using Abp.Linq.Expressions;
using MyApi.Informacoes;
using MyApi.Enums;
using MyApi.Treinos.Dto;

namespace MyApi.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, GetAll, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly IdentidadeService _identidadeService;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<EmailCode, long> _emailCodeRepository;
        private readonly IRepository<Informacao, long> _informacaoRepository;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IObjectMapper _objectMapper;

        public UserAppService(
            IdentidadeService identidadeService,
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IRepository<User, long> userRepository,
            IRepository<EmailCode, long> emailCodeRepository,
            IRepository<Informacao, long> informacaoRepository,
        IPasswordHasher<User> passwordHasher,
            IObjectMapper objectMapper,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _informacaoRepository = informacaoRepository;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _emailCodeRepository = emailCodeRepository;
            _identidadeService = identidadeService;
            _objectMapper = objectMapper;
            _userRepository = userRepository;
        }



        public async Task<UserDto> CreateOrUpdate(UserDto input)
        {
            var exists = await Repository.GetAll().Where(x => x.Cpf == input.Cpf).FirstOrDefaultAsync();
            if (exists != null)
                input.Id = exists.Id;

            if (input.Id == 0)
            {
                var createUserDto = _objectMapper.Map<CreateUserDto>(input);

                return await CreateAsync(createUserDto);
            }
            else
            {
                var userDto = _objectMapper.Map<UserDto>(input);
                return await UpdateAsync(userDto);
            }

        }
        [AbpAllowAnonymous]
        [AllowAnonymous]
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {


            CheckCreatePermission();
            input.UserName = input.EmailAddress;
            input.UserName = input.EmailAddress;
            input.Surname = input.EmailAddress;
            if (input.Password == null)
                input.Password = "02252713Fc!";
            input.RoleNames = new string[] { "Informacao", "admin" };
            var user = ObjectMapper.Map<User>(input);
            user.IsActive = true;
            user.TenantId = AbpSession.TenantId;
            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();


            return MapToEntityDto(user);

        }

        [AbpAllowAnonymous]
        public async Task GenerateCodeToValidateUser(UserDto user)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            string randomCode = randomNumber.ToString("D6");

            var emailCodeDto = new EmailCodeDto() { Code = randomCode, UserId = user.Id };
            var emailCode = _objectMapper.Map<EmailCode>(emailCodeDto);
            await _emailCodeRepository.InsertAsync(emailCode);
            await _identidadeService.SendEmailAsync(user, randomCode, Properties.Resources.SubjectEmail, Properties.Resources.bodyEmailCode);

        }

        [AllowAnonymous]
        public async Task ValidateEmail(EmailCodeDto emailCodeDto)
        {
            var user = await Repository.GetAsync(emailCodeDto.UserId);
            user.EmailConfirmationCode = emailCodeDto.Code;
            user.IsEmailConfirmed = true;
            await Repository.UpdateAsync(user);
        }
        [AbpAllowAnonymous]
        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        [AbpAllowAnonymous]
        public async Task<bool> ValidateRecoveryToken(EmailCodeValidationDto emailcode)
        {
            var user = await _userManager.FindByIdAsync(emailcode.UserId.ToString());
            var result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", emailcode.Code);
            if (result)
            {
                return true;
            }

            return false;
        }

        [AbpAllowAnonymous]
        public async Task RecoveryPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var userDto = _objectMapper.Map<UserDto>(user);
            var callbackUrl = $"http://localhost:8080/#/public/recovery/{user.Id}/{Uri.EscapeDataString(code)}";
            await _identidadeService.SendEmailRecoveryPasswordAsync(userDto, callbackUrl, Properties.Resources.SubjectEmail, Properties.Resources.bodyEmailResetPassword);
        }

        [AllowAnonymous]
        public async Task<bool> VerifyUserAndCode(EmailCodeValidationDto codeValidationDto)
        {
            var result = await _emailCodeRepository.GetAll().Where(x => x.UserId == Int16.Parse(codeValidationDto.UserId) && x.Code == codeValidationDto.Code).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }

            return false;
        }


        [AbpAuthorize]
        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(GetAll input)
        {
            var predicateSearch = PredicateBuilder.New<User>(true);

            if (input.Date != null)
                predicateSearch.And(x => x.CreationTime.Year == input.Date.Value.Year && x.CreationTime.Month == input.Date.Value.Month && x.CreationTime.Day == input.Date.Value.Day);

            if (input.IsActive != null)
                predicateSearch.And(x => x.IsActive == input.IsActive);

            if (!string.IsNullOrEmpty(input.Keyword))
                predicateSearch.And(x => x.Name.Contains(input.Keyword));

            return Repository.GetAllIncluding(x => x.Roles, x => x.Treinos).Where(predicateSearch);
        }

        [AbpAuthorize(AdministrativePermissionActions.Administrative_User_Get)]
        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }
        [AbpAuthorize]
        [HttpGet]
        public async Task<byte[]> ObterFotoUsuarioLogado()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());

                if (user.Imagem == null)
                    return null;
                else
                    return user.Imagem;
            }
            catch
            {
                return null;
            }
        }

        [AbpAuthorize]
        [HttpGet]
        public async Task<EnumObjetivoAcademia> GetObjetivo(long id)
        {
            var result = await _informacaoRepository.GetAll().Where(x => x.UserId == id).FirstOrDefaultAsync();
            return result.ObjetivoInformacao;
        }

        //[AbpAuthorize]
        //public override async Task<UserDto> GetAsync(EntityDto<long> input)
        //{
        //    var user = _objectMapper.Map<UserDto>(await Repository.GetAll().Include(x => x.Treinos).ThenInclude(x => x.Exercicio).Where(x => x.Id == input.Id).FirstOrDefaultAsync());
        //    return user;
        //}

        [AbpAuthorize]
        public async Task<UserDto> UpdateFotoUsuarioLogado(UserImagemDto dto)
        {
            var user = await GetAsync(new EntityDto<long> { Id = Int64.Parse(dto.UserId) });
            user.Imagem = dto.Imagem;
            await UpdateAsync(user);
            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, GetAll input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());

            var comparePasswords = _passwordHasher.VerifyHashedPassword(user, user.Password, input.NewPassword);

            if (comparePasswords == PasswordVerificationResult.Success)
            {
                throw new UserFriendlyException("Digite uma senha diferente da antiga!");
            }

            if (input.NewPassword == "myApi@01")
            {
                throw new UserFriendlyException("Digite uma senha diferente da padrão do sistema!");
            }

            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> CheckUserEmail(string email)
        {
            if (await Repository.GetAll().Where(x => x.EmailAddress == email).AnyAsync())
                throw new UserFriendlyException($"E-mail {email} já foi usado.");
            else
                return true;
        }

        public async Task SetDefaultPasswordAsync(long userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId.ToString());
            var newPassword = _passwordHasher.HashPassword(currentUser, "myApi@01");
            currentUser.Password = newPassword;

            await Repository.UpdateAsync(currentUser);
        }

        [AbpAllowAnonymous]
        public async Task<long> GetUserByEmail(string email)
        {
            return await Repository.GetAll().Where(x => x.EmailAddress == email).Select(x => x.Id).FirstOrDefaultAsync();
        }

        [AbpAllowAnonymous]
        public async Task<UserDto> GetByEmailOrUserName(string email)
        {
            var user = await _userManager.FindByNameOrEmailAsync(email);
            return _objectMapper.Map<UserDto>(user);
        }

    }
}

