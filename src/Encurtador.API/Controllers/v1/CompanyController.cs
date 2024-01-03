using System;
using Encurtador.API.Controllers.Common;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.DTOs;
using Encurtador.API.Models;
using Encurtador.API.Services.Interfaces;
using Encurtador.API.Views.Common;
using Encurtador.API.Views.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/company")]
    [Produces("application/json")]
    [Consumes("application/json")] 
    [ApiController]

    public class CompanyController : BaseController
    {

        private readonly ICompanyServices _companyServices;

        public CompanyController(ICompanyServices companyServices, ILogger<BaseController> logger) : base(logger)
        {
            _companyServices = companyServices;
        }


        // POST api/v1/shortener
        /// <summary>
        /// Shortener url
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST api/v1/company
        ///     {
        ///        "name": "Test Company",
        ///        "cnpj": "82713977000110"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Link encurtado</returns>
        /// <response code="201">Return new url</response>
        /// <response code="500">Return error</response> 
        [HttpPost]
            [ProducesResponseType(201, Type = typeof(BaseView<ShortenedCreateView>))]
            [ProducesResponseType(500, Type = typeof(BaseView<ShortenedCreateView>))]
            public async Task<IActionResult> Create([FromBody] CompanyDTO request)
                => await Exec("Create company", $"Company => {request.CNPJ}",
                    async () => await _companyServices.Create(request));
    }
}

