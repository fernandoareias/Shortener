using System;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Testes.Models.ValueObjects
{
    public class CNPJTests
    { 
        [Fact(DisplayName = "Should create a CNPJ.")]
        public void ShouldCreateCNPJ()
           => Assert.NotNull(new CNPJ("93790898000120"));

        [Fact(DisplayName = "Should create a CNPJ with mask.")]
        public void ShouldCreateCNPJWithMask()
           => Assert.NotNull(new CNPJ("07.343.130/0001-76"));

        [Fact(DisplayName = "Should get a CNPJ with mask.")]
        public void ShouldGetCNPJWithMask()
            => Assert.Equal("07.343.130/0001-76", new CNPJ("07.343.130/0001-76").GetNumberWithMask());

        [Fact(DisplayName = "Not should create a CNPJ if number length is great than 14.")]
        public void NotShouldCreateCNPJIfNumberLengthIsGreatThan14()
            => Assert.Throws<ArgumentException>(() => new CNPJ("937908980001200"));

        [Fact(DisplayName = "Not should create a CNPJ if number length is less than 14.")]
        public void NotShouldCreateCNPJIfNumberLengthIsLessThan14()
            => Assert.Throws<ArgumentException>(() => new CNPJ("9379089800"));

        [Fact(DisplayName = "Should not create CNPJ if number is empty")]
        public void ShouldNotCreateCNPJIfNumberIsEmpty()
            => Assert.Throws<ArgumentException>(() => new CNPJ(string.Empty));

        [Fact(DisplayName = "Should not create CNPJ if number is null")]
        public void ShouldNotCreateCNPJIfNumberIsNull()
            => Assert.Throws<ArgumentException>(() => new CNPJ(null));

        [Fact(DisplayName = "Not should create a CNPJ when invalid")]
        public void NotShouldCreateCNPJWhenInvalid()
            => Assert.Throws<ArgumentException>(() => new CNPJ("11190898000120"));
    }
}

