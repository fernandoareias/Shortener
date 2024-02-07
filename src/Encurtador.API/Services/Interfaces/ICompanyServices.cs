using System;
using Encurtador.API.DTOs;
using Encurtador.API.Views.Common;
using Encurtador.API.Views.v1;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Services.Interfaces
{
    public interface ICompanyServices
    {
        Task<BaseView<CompanyCreatedView>?> Create(CompanyDTO request);
    }
}

