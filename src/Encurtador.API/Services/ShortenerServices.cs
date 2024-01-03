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

        public async Task<IActionResult> Burn(string code)
        {
            var entity = await _encurtadorRepository.GetByCode(code);

            if (entity is null)
                return new NotFoundObjectResult(new BaseView<object>(System.Net.HttpStatusCode.NotFound, "Error", null, "Link not found"));

            if (entity.Burned)
                return new BadRequestObjectResult(new BaseView<object>(System.Net.HttpStatusCode.NotFound, "Error", null, "Code has already be burned"));

            entity!.Burn();

            _encurtadorRepository.Update(entity);
            await _encurtadorRepository.unitOfWork.Commit();

            return new RedirectResult(entity.Url);
        }

        public async Task<IActionResult> Create(ShortenerDTO request, HttpContext context)
        {
            var userId = _authenticationService.GetClaimValue(context, ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return new UnauthorizedResult();

            var entity = new Shortened(request.Url, ObjectId.Parse(userId));

            _encurtadorRepository.Add(entity);
            await _encurtadorRepository.unitOfWork.Commit();
            var view = new ShortenedCreateView(entity);

            return new CreatedResult(view.Url, new BaseView<ShortenedCreateView>(System.Net.HttpStatusCode.Created, "Entity created.", view));
        }
    }
}

