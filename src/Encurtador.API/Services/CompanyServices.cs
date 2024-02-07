using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Services.Interfaces;
using Encurtador.API.Views.Common;
using Encurtador.API.Views.v1;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyServices(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BaseView<CompanyCreatedView>?> Create(CompanyDTO request)
        {
            var company = await _companyRepository.GetByCNPJ(request.CNPJ);

            if (company is not null)
                return null;

            var entity = new Company(request.Name, new Models.ValueObjects.CNPJ(request.CNPJ));

            _companyRepository.Add(entity);
            await _companyRepository.unitOfWork.Commit();
            var view = new CompanyCreatedView(entity);

            return new BaseView<CompanyCreatedView>(System.Net.HttpStatusCode.Created, "Entity created.", view);
        }
    }
}

