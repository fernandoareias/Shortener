using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Models.ValueObjects;
using Encurtador.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Encurtador.API.Tests.Services
{
    public class CompanyServicesTests
    {
        [Fact(DisplayName = "Should create company.")]
        public async void ShouldCreateCompany()
        {
            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(c => c.GetByCNPJ(It.IsAny<string>())).ReturnsAsync((Company)null);
            companyRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var services = new CompanyServices(companyRepository.Object);

            var request = new CompanyDTO() { Name = "Teste company", CNPJ = "93790898000120" };
            var response = await services.Create(request);

            Assert.True(response is CreatedResult);
        }

        [Fact(DisplayName = "Not should create company when already exists.")]
        public async void NotShouldCreateCompanyWhenAlreadyExists()
        {
            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(c => c.GetByCNPJ(It.IsAny<string>())).ReturnsAsync((new Company("Test company", new CNPJ("93790898000120"))));
            companyRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var services = new CompanyServices(companyRepository.Object);

            var request = new CompanyDTO() { Name = "Teste company", CNPJ = "93790898000120" };
            var response = await services.Create(request);

            Assert.True(response is BadRequestResult);
        }
    }
}

