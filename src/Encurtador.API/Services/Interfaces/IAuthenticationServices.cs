using System;
using Microsoft.AspNetCore.Mvc;
using Shortener.API.Views.v1;

namespace Encurtador.API.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<UserAuthenticateView?> Authenticate(string email, string password);
        Task<UserRegisterView?> Register(string email, string password, string cnpj);
        string? GetClaimValue(HttpContext context, string claim);
    }
}

