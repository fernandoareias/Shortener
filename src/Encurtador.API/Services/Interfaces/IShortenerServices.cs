using System;
using Encurtador.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Services.Interfaces
{
    public interface IShortenerServices
    {
        Task<IActionResult> Create(ShortenerDTO request, HttpContext context);
        Task<IActionResult> Burn(string code);
    }
}

