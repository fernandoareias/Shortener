using System.Security.Claims;
using Encurtador.API.Controllers.Common;
using Encurtador.API.Data.Proxy;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Services.Interfaces;
using Encurtador.API.Views.Common;
using Encurtador.API.Views.v1; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;

namespace Encurtador.API.Controllers;

[ApiVersion("1")] 
[Route("api/v{version:apiVersion}")]
[Produces("application/json")]
[Consumes("application/json")] 
[ApiController]

public class ShortenerController : BaseController
{
    private readonly IShortenerServices _shortenerServices;
    public ShortenerController(ILogger<BaseController> logger, IShortenerServices shortenerServices) : base(logger)
    {
        _shortenerServices = shortenerServices;
    }

    // POST api/v1/shortener
    /// <summary>
    /// Shortener url
    /// </summary>
    /// <remarks>
    /// Exemplo:
    ///
    ///     POST api/v1/shortener
    ///     {
    ///        "url": "https://www.google.com"
    ///     }
    ///
    /// </remarks>
    /// <param name="value"></param>
    /// <returns>Link encurtado</returns>
    /// <response code="201">Return new url</response>
    /// <response code="500">Return error</response> 
    [HttpPost("shortener")]
    [ProducesResponseType(201, Type = typeof(BaseView<ShortenedCreateView>))]
    [ProducesResponseType(500, Type = typeof(BaseView<ShortenedCreateView>))]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] ShortenerDTO request)
        => await Exec("SHORTENER", $"URL => {request.Url}", async () => await _shortenerServices.Create(request, HttpContext));



    // GET api/v1/burn/2622d838-bfc0-4c72-aa56-fc33fd34d88c
    /// <summary>
    /// Burn shortener url
    /// </summary>
    /// <remarks>
    /// Exemplo:
    ///
    ///     GET api/v1/7272D4226
    ///
    /// </remarks>
    /// <param name="value"></param>
    /// <returns>Link encurtado</returns>
    /// <response code="302">Redirect to origin url</response>
    /// <response code="404">Return if id not exists</response>
    /// <response code="500">Return error</response> 
    [HttpGet("burn/{code}")] 
    [ProducesResponseType(302)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Burn(string code)
        => await Exec("BURN", $"ID => {code}", async () => await _shortenerServices.Burn(code)); 
}

