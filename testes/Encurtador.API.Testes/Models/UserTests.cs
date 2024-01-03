using System;
using Encurtador.API.Models;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Testes.Models
{
    public class UserTests
    {
        [Fact(DisplayName = "Should create user.")]
        public void ShouldCreateUser()
            => Assert.NotNull(new User(new Email("fareias@gmail.com"), new Password("$WHfeUhgBWjh1"), new Company("Test company", new CNPJ("93790898000120"))));

        [Fact(DisplayName = "Should throw exception when e-mail is null.")]
        public void ShouldThrowExceptionWhenEmailIsNull()
            => Assert.Throws<ArgumentException>(() => new User(null, new Password("$WHfeUhgBWjh1"), new Company("Test company", new CNPJ("93790898000120"))));

        [Fact(DisplayName = "Should throw exception when password is null.")]
        public void ShouldThrowExceptionWhenPasswordIsNull()
            => Assert.Throws<ArgumentException>(() => new User(new Email("fareias@gmail.com"), null, new Company("Test company", new CNPJ("93790898000120"))));

        [Fact(DisplayName = "Should throw exception when company is null.")]
        public void ShouldThrowExceptionWhenCompanyIsNull()
           => Assert.Throws<ArgumentException>(() => new User(new Email("fareias@gmail.com"), new Password("$WHfeUhgBWjh1"), null));
    }
}

