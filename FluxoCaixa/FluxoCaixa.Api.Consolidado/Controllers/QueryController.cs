using FluxoCaixa.Domain.Consolidado.Interfaces.Services;
using FluxoCaixa.Domain.Consolidado.ObjectValue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoCaixa.Api.Consolidado.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly ILancamentoService lancamentoService;
        private static string format = "yyyy-MM-dd";

        public QueryController(IMemoryCache cache, ILancamentoService lancamentoService)
        {
            this.cache = cache;
            this.lancamentoService = lancamentoService;
        }

        [ProducesResponseType(typeof(LancamentoConsolidado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado do dia.")]
        [HttpGet("Dia")]
        public async Task<IActionResult> GetDia()
        {

            var cacheKey = $"cache-obterdia-{DateTime.Now.ToString(format)}";

            if (cache.TryGetValue(cacheKey, out LancamentoConsolidado dados))
            {
                return Ok(dados);
            }
            else
            {
                dados = (await lancamentoService.ObterLancamentosDia());
                cache.Set(cacheKey, dados, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
                return Ok(dados);
            }

        }

        [ProducesResponseType(typeof(LancamentoConsolidado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado dos ultimos 7 dias.")]
        [HttpGet("SevenDays")]
        public async Task<IActionResult> GetLastSevenDays()
        {
            var cacheKey = $"cache-obter7dias-{DateTime.Now.AddDays(-7).ToString(format)}-{DateTime.Now.ToString(format)}";

            if (cache.TryGetValue(cacheKey, out LancamentoConsolidado dados))
            {
                return Ok(dados);
            }
            else
            {
                dados = (await lancamentoService.ObterLancamentosFaixaDeDataAteHoje(DateTime.Now.AddDays(-7)));
                cache.Set(cacheKey, dados, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return Ok(dados);
            }

        }

        [ProducesResponseType(typeof(LancamentoConsolidado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado do mês atual.")]
        [HttpGet("Month")]
        public async Task<IActionResult> GetMonth()
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var cacheKey = $"cache-obterdomes-{DateTime.Now.ToString(format)}-mes-{month}-ano-{year}";

            if (cache.TryGetValue(cacheKey, out LancamentoConsolidado dados))
            {
                return Ok(dados);
            }
            else
            {
                dados = (await lancamentoService.ObterLancamentosMesEspecifico(month, year));
                cache.Set(cacheKey, dados, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return Ok(dados);
            }

        }

        [ProducesResponseType(typeof(LancamentoConsolidado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado do mês especifico.")]
        [HttpGet("Month/{month}/{year}")]
        public async Task<IActionResult> GetMonth(int month, int year)
        {
            var cacheKey = $"cache-obterdomesespec-{DateTime.Now.ToString(format)}-mes-{month}-ano-{year}";

            if (cache.TryGetValue(cacheKey, out LancamentoConsolidado dados))
            {
                return Ok(dados);
            }
            else
            {
                dados = (await lancamentoService.ObterLancamentosMesEspecifico(month, year));
                cache.Set(cacheKey, dados, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return Ok(dados);
            }
        }


        [ProducesResponseType(typeof(LancamentoConsolidado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obter o consolidado de um período. Inserir as datas no formato yyyy-mm-dd")]
        [HttpGet("Range/{dataInicial}/{dataFinal}")]
        public async Task<IActionResult> GetPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var cacheKey = $"cache-obterfaixadedatas-{dataInicial.ToString(format)}-{dataFinal.ToString(format)}";

            if (cache.TryGetValue(cacheKey, out LancamentoConsolidado dados))
            {
                return Ok(dados);
            }
            else
            {
                dados = (await lancamentoService.ObterLancamentosFaixaDeDatas(dataInicial, dataFinal));
                cache.Set(cacheKey, dados, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return Ok(dados);
            }
        }


    }
}
