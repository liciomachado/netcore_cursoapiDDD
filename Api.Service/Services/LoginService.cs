using System;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        public IConfiguration _configuration { get; set; }

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                baseUser = await _repository.FindByLogin(user.Email);
            else
                return new { authenticated = false, message = "Falha ao autenticar" };

            if (baseUser == null)
                return new { authenticated = false, message = "Falha ao autenticar" };

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(baseUser.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                }
            );
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

            var handler = new JwtSecurityTokenHandler();
            string token = CreateToken(identity, createDate, expirationDate, handler);

            return SuccessObject(createDate, expirationDate, token, baseUser);
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });
            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                name = user.Name,
                message = "Usuário logado com sucesso"
            };
        }
    }
}
