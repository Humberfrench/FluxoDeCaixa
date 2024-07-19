using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoCaixa.Api.Consolidado.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QueryController : ControllerBase
    {
        public QueryController()
        {
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado do dia.")]
        [HttpGet("Dia")]
        public async Task<IActionResult> GetDia()
        {
            return null;
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado dos ultimos 7 dias.")]
        [HttpGet("SevenDays")]
        public async Task<IActionResult> GetLastSevenDays()
        {
            return null;
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado do mês atual.")]
        [HttpGet("Month")]
        public async Task<IActionResult> GetMonth()
        {
            return null;
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado do mês especifico.")]
        [HttpGet("Month/{month}")]
        public async Task<IActionResult> GetMonth(int month)
        {
            return null;
        }


        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado de um período. Inserir as datas no formato yyyy-mm-dd")]
        [HttpGet("Range/{dataInicial}/{dataFinal}")]
        public async Task<IActionResult> GetPeriodo(string dataInicial, string dataFinal)
        {
            return null;
        }

    }
}
