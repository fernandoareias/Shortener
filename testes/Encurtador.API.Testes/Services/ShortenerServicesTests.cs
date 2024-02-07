using System;
using Encurtador.API.Data.Proxy;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Services;
using Encurtador.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;

namespace Encurtador.API.Tests.Services
{
    public class ShortenerServicesTests
    {
        [Fact(DisplayName = "Should burn shortened.")]
        public async void ShouldBurnShortened()
        {
            var authenticationServices = new Mock<IAuthenticationServices>();
            var shortenerRepository = new Mock<IShortenerRepository>();
            shortenerRepository.Setup(c => c.GetByCode(It.IsAny<string>())).ReturnsAsync(new Shortened("https://google.com", new MongoDB.Bson.ObjectId("65837c191499e44a497d4235")));
            shortenerRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);
            var cacheRedis = new Mock<IDistributedCache>();
            var logRedis = new Mock<ILogger<RedisProxy>>();

            var services = new ShortenerServices(
                authenticationServices.Object,
                shortenerRepository.Object,
                cacheRedis.Object,
                logRedis.Object
            );

            var result = await services.Burn("0478C6D89");

            Assert.False(string.IsNullOrWhiteSpace(result));
        }


        [Fact(DisplayName = " Not should burn shortened when shortened not found.")]
        public async void NotShouldBurnWhenShortenedNotExists()
        {
            var authenticationServices = new Mock<IAuthenticationServices>();
            var shortenerRepository = new Mock<IShortenerRepository>();
            shortenerRepository.Setup(c => c.GetByCode(It.IsAny<string>())).ReturnsAsync((Shortened)null);
            var cacheRedis = new Mock<IDistributedCache>();
            var logRedis = new Mock<ILogger<RedisProxy>>();

            var services = new ShortenerServices(
                authenticationServices.Object,
                shortenerRepository.Object,
                cacheRedis.Object,
                logRedis.Object
            );

            var result = await services.Burn("0478C6D89");

            Assert.Null(result);
        }


        [Fact(DisplayName = "Not should burn shortened when already burned.")]
        public async void NotShouldBurnShortenedWhenAlreadyBurned()
        {

            var entity = new Shortened("https://google.com", new MongoDB.Bson.ObjectId("65837c191499e44a497d4235"));
            entity.Burn();

            var authenticationServices = new Mock<IAuthenticationServices>();
            var shortenerRepository = new Mock<IShortenerRepository>();
            shortenerRepository.Setup(c => c.GetByCode(It.IsAny<string>())).ReturnsAsync(entity);
            var cacheRedis = new Mock<IDistributedCache>();
            var logRedis = new Mock<ILogger<RedisProxy>>();

            var services = new ShortenerServices(
                authenticationServices.Object,
                shortenerRepository.Object,
                cacheRedis.Object,
                logRedis.Object
            );

            await Assert.ThrowsAsync<Microsoft.AspNetCore.Http.BadHttpRequestException>(() => services.Burn("0478C6D89"));
        }


        [Fact(DisplayName = "Should create shortened.")]
        public async void ShouldCreateShortened()
        {
            var authenticationServices = new Mock<IAuthenticationServices>();
            authenticationServices.Setup(c => c.GetClaimValue(It.IsAny<HttpContext>(), It.IsAny<string>())).Returns("65837c191499e44a497d4235");

            var shortenerRepository = new Mock<IShortenerRepository>();
            shortenerRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var cacheRedis = new Mock<IDistributedCache>();
            var logRedis = new Mock<ILogger<RedisProxy>>();

            var services = new ShortenerServices(
                authenticationServices.Object,
                shortenerRepository.Object,
                cacheRedis.Object,
                logRedis.Object
            );

            var request = new ShortenerDTO { Url = "https://google.com.br" };
            var result = await services.Create(request, new Mock<HttpContext>().Object);

            Assert.NotNull(result?.Data);
        }

        [Fact(DisplayName = "Not should create shortened when userId not found.")]
        public async void NotShouldCreateShortenedWhenUserIdNotFound()
        {
            var authenticationServices = new Mock<IAuthenticationServices>();
            authenticationServices.Setup(c => c.GetClaimValue(It.IsAny<HttpContext>(), It.IsAny<string>())).Returns(string.Empty);

            var shortenerRepository = new Mock<IShortenerRepository>();
            shortenerRepository.Setup(c => c.unitOfWork.Commit()).ReturnsAsync(true);

            var cacheRedis = new Mock<IDistributedCache>();
            var logRedis = new Mock<ILogger<RedisProxy>>();

            var services = new ShortenerServices(
                authenticationServices.Object,
                shortenerRepository.Object,
                cacheRedis.Object,
                logRedis.Object
            );

            var request = new ShortenerDTO { Url = "https://google.com.br" };
            await Assert.ThrowsAsync<System.UnauthorizedAccessException>(() => services.Create(request, new Mock<HttpContext>().Object));
        }
    }
}

