using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Encurtador.API.Configurations;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using Encurtador.API.Models.ValueObjects;
using Encurtador.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shortener.API.Views.v1;

namespace Encurtador.API.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {

        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<AuthenticationServices> _logger;
        public AuthenticationServices(IUserRepository userRepository, ICompanyRepository companyRepository, ILogger<AuthenticationServices> logger)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<UserAuthenticateView?> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user == null)
                return null;

            if (!user.Password.Compare(password))
                return null;

            var token = GenerateJwt(user);

            return new UserAuthenticateView(token, user.Email.Endereco);

        }

        public string? GetClaimValue(HttpContext context, string claim)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null)
                return identity?.FindFirst(claim)?.Value;

            return null;

        }

        public async Task<UserRegisterView?> Register(string email, string password, string cnpj)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user != null)
                return null;

            var company = await _companyRepository.GetByCNPJ(cnpj);

            if (company == null)
                return null;

            user = new Models.User(new Email(email), new Password(password), company);

            _userRepository.Add(user);

            await _userRepository.unitOfWork.Commit();

            var token = GenerateJwt(user);

            return new UserRegisterView(token, user.Email.Endereco);
        }


        private string GenerateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user._id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email.Endereco),
                     new Claim("company", user.Company.CNPJ.Numero),
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

