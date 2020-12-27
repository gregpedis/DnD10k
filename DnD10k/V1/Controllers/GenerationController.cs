using DnD10k.Base;
using DnD10k.Extensions;
using DnD10k.Models;
using DnD10k.V1.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.V1.Controllers
{
    [ApiController]
    [ApiVersion(Definitions.FIRST_VERSION)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenerationController : ControllerBase
    {
        private readonly ILogger<GenerationController> _logger;
        private readonly IGenerationService _provider;

        public GenerationController(
            ILogger<GenerationController> logger, 
            IGenerationService provider) 
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(Effect), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetRandomEffect()
        {
            try
            {
                var effect = _provider.GetRandomEffect();
                return this.Ok(effect);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return BadRequest(e.Message);
            }
        }
    }
}

