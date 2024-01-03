using System;
using Encurtador.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Services.Interfaces
{
    public interface ICompanyServices
    {
        Task<IActionResult> Create(CompanyDTO request);
    }
}

