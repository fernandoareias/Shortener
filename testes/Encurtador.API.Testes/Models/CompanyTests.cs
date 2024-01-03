using System;
using Encurtador.API.Models;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Testes.Models
{
    public class CompanyTests
    {
        [Fact(DisplayName = "Should create company.")]
        public void ShouldCreateCompany()
            => Assert.NotNull(new Company("Test company", new CNPJ("93790898000120")));

        [Fact(DisplayName = "Should throw exception when company name is empty.")]
        public void ShouldThrowExceptionWhenCompanyNameIsEmpty()
            => Assert.Throws<ArgumentException>(() => new Company(string.Empty, new CNPJ("93790898000120")));

        [Fact(DisplayName = "Should throw exception when company name is null.")]
        public void ShouldThrowExceptionWhenCompanyNameIsNull()
            => Assert.Throws<ArgumentException>(() => new Company(null, new CNPJ("93790898000120")));


        [Fact(DisplayName = "Should throw exception when company cnpj is null.")]
        public void ShouldThrowExceptionWhenCompanyCnpjIsNull()
            => Assert.Throws<ArgumentException>(() => new Company("Test company", null));
    }
}

