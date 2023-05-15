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

        [HttpGet("Hoje")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos do dia")]
        public async Task<IActionResult> LancamentosDoDia()
        {
            var retorno = await lancamentoApp.LancamentosDoDia();

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("PorData/{date}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos por data. Informar data como dd-mm-aaaa")]
        public async Task<IActionResult> LancamentosPorData(string date)
        {

            var data = StringToDate(date);
            var retorno = await lancamentoApp.LancamentosPorData(data);

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("PorData/{dataInicial}/{dataFinal}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos por faixa de data. Informar data como dd-mm-aaaa")]
        public async Task<IActionResult> LancamentosPorFaixaData(string dataInicial, string dataFinal)
        {
            var inicio = StringToDate(dataInicial);
            var fim = StringToDate(dataFinal);
            var retorno = await lancamentoApp.LancamentosPorFaixaData(inicio, fim);

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("PorMes/{mes}/{ano}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no mês desejado.")]
        public async Task<IActionResult> LancamentosPorMes(int mes, int ano)
        {
            var retorno = await lancamentoApp.LancamentosPorMes(mes, ano);

            return Completed<ValidationResult>(retorno);
        }

        [HttpPost("Lancar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Efetua o Lançamento de uma operação")]
        public async Task<IActionResult> Lancar(LancamentoViewModel lancamento)
        {
            var retorno = await lancamentoApp.Lancar(lancamento);

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("Consolidado")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados. A informação é do dia atual")]
        public async Task<IActionResult> ObterLancamentosConsolidado()
        {
            var retorno = await lancamentoApp.ObterLancamentosConsolidado();

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("Consolidado/{data}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados. A informação é do dia informado como data em string dd-mm-aaaa")]
        public async Task<IActionResult> ObterLancamentosConsolidadoPorData(string data)
        {
            var date = StringToDate(data);
            var retorno = await lancamentoApp.ObterLancamentosConsolidadoPorData(date);

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("Consolidado/sql/{data}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados (usando procedure). A informação é do dia informado como data em string dd-mm-aaaa")]
        public async Task<IActionResult> ObterLancamentosConsolidadoPorDataSql(DateTime data)
        {
            var retorno = await lancamentoApp.ObterLancamentosConsolidadoPorDataSql(data);

            return Completed<ValidationResult>(retorno);
        }

        [HttpGet("Consolidado/sql")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados (usando procedure). A informação é do dia informado como data em string dd-mm-aaaa")]
        public async Task<IActionResult> ObterLancamentosConsolidadoSql()
        {
            var retorno = await lancamentoApp.ObterLancamentosConsolidadoSql();

            return Completed<ValidationResult>(retorno);
        }


        private DateTime StringToDate(string data)
        {
            var dataSplit = data.Split('-');
            var dia = Convert.ToInt32(dataSplit[0]);
            var mes = Convert.ToInt32(dataSplit[1]);
            var ano = Convert.ToInt32(dataSplit[2]);
            return new DateTime(ano, mes, dia);
        }

    }
}
