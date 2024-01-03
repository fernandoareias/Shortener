using System;
using Encurtador.API.Views.v1;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.API.Controllers.Common
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected async Task<IActionResult> Exec(string method, string parameters, Func<Task<IActionResult>> process)
        {
            _logger.LogInformation($"[START][{method}] - {parameters}");
            try
            {
                return await process.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[EXCEPTION][{method}] - {parameters} | Exception => {ex}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"[END][{method}] - {parameters}");
            } 
        }
    }
}

