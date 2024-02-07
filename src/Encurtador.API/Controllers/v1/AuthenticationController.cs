using System;
using Encurtador.API.Controllers.Common;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Services.Interfaces;
using Encurtador.API.Views.Common;
using Encurtador.API.Views.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace Encurtador.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]

    public class AuthenticationController : BaseController
    {

        private readonly IAuthenticationServices _authenticationServices;
        public AuthenticationController(ILogger<BaseController> logger, IAuthenticationServices authenticationServices) : base(logger)
        {
            _authenticationServices = authenticationServices;
        }

        // POST api/v1/authentication/sign-in
        /// <summary>
        /// Sign In
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST api/v1/authentication/sign-in
        ///     {
        ///        "email": "example@teste.com.br",
        ///        "password": "Example@123"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Token</returns>
        /// <response code="201">Return JWT Token</response>
        /// <response code="500">Return error</response> 
        [HttpPost("authentication/sign-in")]
        [ProducesResponseType(201, Type = typeof(BaseView<ShortenedCreateView>))]
        [ProducesResponseType(500, Type = typeof(BaseView<ShortenedCreateView>))]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO request)
            => await Exec("SIGN-IN", $"E-mail => {request.Email}",
                        async () =>
                        {
                            var result = await _authenticationServices.Authenticate(request.Email, request.Password);

                            return result is null ? Unauthorized() : Ok(result);
                        });


        // POST api/v1/authentication/sign-up
        /// <summary>
        /// Sign In
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST api/v1/authentication/sign-up
        ///     {
        ///        "email": "example@teste.com.br",
        ///        "password": "Example@123",
        ///        "cnpj": "82713977000110"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Token</returns>
        /// <response code="200">Return JWT Token</response>
        /// <response code="500">Return error</response> 
        [HttpPost("authentication/sign-up")]
            [ProducesResponseType(201, Type = typeof(BaseView<ShortenedCreateView>))]
            [ProducesResponseType(500, Type = typeof(BaseView<ShortenedCreateView>))]
            public async Task<IActionResult> SignUp([FromBody] AuthenticationDTO request)
                => await Exec("SIGN-UP", $"E-mail => {request.Email}",
                            async () =>
                            {
                                var result  = await _authenticationServices.Register(request.Email, request.Password, request.CNPJ);
                                return result is null ? Unauthorized() : Ok(result);
                            });
        }
}

