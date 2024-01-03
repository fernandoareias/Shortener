using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using Encurtador.API.Models.ValueObjects;
using Encurtador.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Encurtador.API.Testes.Services
{
    public class AuthenticationServicesTests
    {
        [Fact(DisplayName = "Should authenticate.")]
        public async void ShouldAuthenticate()
        {
            string password = "$WHfeUhgBWjh1";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(c => c.GetByEmail(It.IsAny<string>())).ReturnsAsync(new User(new Email("fareias@gmail.com"), new Password(password), new Company("Test company", new CNPJ("93790898000120"))));

            var companyRepository = new Mock<ICompanyRepository>();
            var log = new Mock<ILogger<AuthenticationServices>>();

           var service = new AuthenticationServices(userRepository.Object, companyRepository.Object, log.Object);

            var response = await service.Authenticate("teste@gmail.com", password);

            Assert.True(response is OkObjectResult);

        }


        [Fact(DisplayName = "Not should authenticate when user not exist.")]
        public async void NotShouldAuthenticateWhenUserNotExist()
        {
            string password = "$WHfeUhgBWjh1";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(c => c.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            var companyRepository = new Mock<ICompanyRepository>();
            var log = new Mock<ILogger<AuthenticationServices>>();

            var service = new AuthenticationServices(userRepository.Object, companyRepository.Object, log.Object);

            var response = await service.Authenticate("teste@gmail.com", password);

            Assert.True(response is UnauthorizedResult);

        }


        [Fact(DisplayName = "Not should authenticate when password invalid.")]
        public async void NotShouldAuthentcateWhenPasswordInvalid()
        {
            string password = "$WHfeUhgBWjh1";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(c => c.GetByEmail(It.IsAny<string>())).ReturnsAsync(new User(new Email("fareias@gmail.com"), new Password(password), new Company("Test company", new CNPJ("93790898000120"))));


            var companyRepository = new Mock<ICompanyRepository>();
            var log = new Mock<ILogger<AuthenticationServices>>();

            var service = new AuthenticationServices(userRepository.Object, companyRepository.Object, log.Object);

            var response = await service.Authenticate("teste@gmail.com", "test123");

            Assert.True(response is UnauthorizedResult);

        }


        [Fact(DisplayName = "Should register new user.")]
        public async void ShouldRegisterUser()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(c => c.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            userRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(c => c.GetByCNPJ(It.IsAny<string>())).ReturnsAsync(new Company("Test company", new CNPJ("93790898000120")));

            var log = new Mock<ILogger<AuthenticationServices>>();

            var service = new AuthenticationServices(userRepository.Object, companyRepository.Object, log.Object);

            var response = await service.Register("teste@gmail.com", "test123", "93790898000120");

            Assert.True(response is OkObjectResult);
        }

        [Fact(DisplayName = "Not should register user when already exists.")]
        public async void NotShouldRegisterUserWhenAlreadyExists()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(c => c.GetByEmail(It.IsAny<string>())).ReturnsAsync(new User(new Email("fareias@gmail.com"), new Password("$WHfeUhgBWjh1"), new Company("Test company", new CNPJ("93790898000120"))));
            userRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(c => c.GetByCNPJ(It.IsAny<string>())).ReturnsAsync(new Company("Test company", new CNPJ("93790898000120")));

            var log = new Mock<ILogger<AuthenticationServices>>();

            var service = new AuthenticationServices(userRepository.Object, companyRepository.Object, log.Object);

            var response = await service.Register("teste@gmail.com", "test123", "93790898000120");

            Assert.True(response is BadRequestResult);
        }

        [Fact(DisplayName = "Not should register when company not exists.")]
        public async void NotShouldRegisterWhenCompanyNotExists()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(c => c.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            userRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(c => c.GetByCNPJ(It.IsAny<string>())).ReturnsAsync((Company)null);

            var log = new Mock<ILogger<AuthenticationServices>>();

            var service = new AuthenticationServices(userRepository.Object, companyRepository.Object, log.Object);

            var response = await service.Register("teste@gmail.com", "test123", "93790898000120");

            Assert.True(response is BadRequestResult);
        }
    }
}

