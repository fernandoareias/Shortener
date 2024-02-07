using System;
using System.Security.Claims;
using Encurtador.API.Data.Proxy;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Services.Interfaces;
using Encurtador.API.Views.Common;
using Encurtador.API.Views.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;

namespace Encurtador.API.Services
{
    public class ShortenerServices : IShortenerServices
    {
        private readonly EncurtadorRepositoryProxy _encurtadorRepository;
        private readonly IAuthenticationServices _authenticationService;

        public ShortenerServices(IAuthenticationServices authenticationService, IShortenerRepository repository, IDistributedCache distributedCache, ILogger<RedisProxy> logger)
        {
            _encurtadorRepository = new EncurtadorRepositoryProxy(repository, distributedCache, logger);
            _authenticationService = authenticationService;
        }

        public async Task<string> Burn(string code)
        {
            var entity = await _encurtadorRepository.GetByCode(code);

            if (entity is null)
                return null;

            if (entity.Burned)
                throw new BadHttpRequestException("Code has already be burned");

            entity!.Burn();

            _encurtadorRepository.Update(entity);
            await _encurtadorRepository.unitOfWork.Commit();

            return entity.Url;
        }

        public async Task<BaseView<ShortenedCreateView>> Create(ShortenerDTO request, HttpContext context)
        {
            var userId = _authenticationService.GetClaimValue(context, ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException();

            var entity = new Shortened(request.Url, ObjectId.Parse(userId));

            _encurtadorRepository.Add(entity);
            await _encurtadorRepository.unitOfWork.Commit();
            var view = new ShortenedCreateView(entity);

            return new BaseView<ShortenedCreateView>(System.Net.HttpStatusCode.Created, "Entity created.", view);
        }
    }
}

