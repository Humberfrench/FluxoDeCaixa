using Dietcode.Api.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoDeCaixa.Api.Controllers
{
    [ApiController]
    [Route("api/Ping")]
    [SwaggerTag("Validação do Status do Serviço")]
    public class PingController : ApiControllerBase
    {

        [HttpGet(""),HttpPost(""),HttpPut("")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        [SwaggerOperation(Summary = "Validação do Status do Serviço")]
        public IActionResult Ping()
        {
            var ok = "OK!Serviço On!";

            return Ok(ok);
        }

    }
}
