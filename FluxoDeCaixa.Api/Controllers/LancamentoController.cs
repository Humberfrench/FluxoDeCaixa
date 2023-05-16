using Dietcode.Api.Core;
using Dietcode.Core.DomainValidator;
using FluxodeCaixa.ViewModel;
using FluxodeCaixa.ViewModel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoDeCaixa.Api.Controllers
{

    [ApiController]
    [Route("api/Lancamento")]
    [SwaggerTag("Lançamentos de Operações")]
    public class LancamentoController : ApiControllerBase
    {
        private readonly ILancamentoApp lancamentoApp;
        public LancamentoController(ILancamentoApp lancamentoApp)
        {
            this.lancamentoApp = lancamentoApp;
        }

        [HttpPost("Estornar/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Efetua o estorno de uma operação")]
        public async Task<IActionResult> Estornar(int id)
        {
            var retorno = await lancamentoApp.Estornar(id);

            return Completed<ValidationResult>(retorno);
        }

        [HttpPost("Lancar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Efetua o Lançamento de uma operação")]
        public async Task<IActionResult> Lancar(LancamentoViewModel lancamento)
        {
            var retorno = await lancamentoApp.Lancar(lancamento);

            return Completed<ValidationResult>(retorno);
        }


    }
}
