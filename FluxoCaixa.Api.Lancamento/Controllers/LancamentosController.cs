using Dietcode.Core.Lib;
using FluxoCaixa.Api.Lancamento.Model;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Bus;
using FluxoCaixa.Domain.Lancamentos.Messaging;
using FluxoCaixa.Domain.Lancamentos.ObjectValue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoCaixa.Api.Lancamento.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LancamentosController : ControllerBase
    {
        public IBusService busService;
        private const string ROUTING_KEY = "Lancamentos";

        public LancamentosController(IBusService busService)
        {
            this.busService = busService;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Efetuar o Lançamento a Crédito.")]
        [HttpPost("Credito")]
        public async Task<IActionResult> LancamentoCredito(PostCredito postCredito)
        {
            try
            {
                var lancamento = new Lancamentos
                {
                    Data = DateTime.Now,
                    Descricao = postCredito.Descricao,
                    Observacao = postCredito.Observacao,
                    TipoLancamentoId = 1,
                    Valor = postCredito.Valor,
                    Estornado = false
                };

                var @event = new Message
                {
                    Id = 1,
                    Content = lancamento.ToJson(),
                    CreatedAt = DateTime.Now
                };

                await busService.Publish(ROUTING_KEY, @event);

                return NoContent();
            }
            catch (Exception ex)
            {
                var erro = new ProblemDetails
                {
                    Title = "Erro ao efetuar o lançamento",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://httpstatuses.com/400"
                };
                return BadRequest(erro);
            }

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Efetuar o Lançamento a Debito.")]
        [HttpPost("Debito")]
        public async Task<IActionResult> LancamentoDebito(PostDebito postDebito)
        {
            try
            {
                var lancamento = new Lancamentos
                {
                    Data = DateTime.Now,
                    Descricao = postDebito.Descricao,
                    Observacao = postDebito.Observacao,
                    TipoLancamentoId = 2,
                    Valor = postDebito.Valor,
                    Estornado = false
                };

                var @event = new Message
                {
                    Id = 1,
                    Content = lancamento.ToJson(),
                    CreatedAt = DateTime.Now
                };

                await busService.Publish(ROUTING_KEY, @event);

                return NoContent();
            }
            catch (Exception ex)
            {
                var erro = new ProblemDetails
                {
                    Title = "Erro ao efetuar o lançamento",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://httpstatuses.com/400"
                };
                return BadRequest(erro);
            }

        }
    }
}

