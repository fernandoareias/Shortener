using System;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<IActionResult> Authenticate(string email, string password);
        Task<IActionResult> Register(string email, string password, string cnpj);
        string? GetClaimValue(HttpContext context, string claim);
    }
}

