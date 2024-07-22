using Dietcode.Api.Core;
using Dietcode.Core.DomainValidator;
using FluxodeCaixa.ViewModel;
using FluxodeCaixa.ViewModel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoDeCaixa.Api.Controllers
{
    [ApiController]
    [Route("api/Relatorios")]
    [SwaggerTag("Relatorios de Lançamentos de Operações")]
    public class RelatoriosController : ApiControllerBase
    {

        private readonly ILancamentoApp lancamentoApp;
        public RelatoriosController(ILancamentoApp lancamentoApp)
        {
            this.lancamentoApp = lancamentoApp;
        }

        [HttpGet("Hoje")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos do dia")]
        public async Task<IActionResult> LancamentosDoDia()
        {
            var retorno = await lancamentoApp.LancamentosDoDia();

            return Completed<ValidationResult<List<LancamentoViewModel>>>(retorno);
        }
        [HttpGet("Estornos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os Estornos do dia")]
        public async Task<IActionResult> EstornosDoDia()
        {
            var retorno = await lancamentoApp.EstornosDoDia();

            return Completed<ValidationResult<List<LancamentoViewModel>>>(retorno);
        }

        [HttpGet("PorData/{date}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos por data. Informar data como dd-mm-aaaa")]
        public async Task<IActionResult> LancamentosPorData(string date)
        {

            var data = StringToDate(date);
            var retorno = await lancamentoApp.LancamentosPorData(data);

            return Completed<ValidationResult<List<LancamentoViewModel>>>(retorno);
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

            return Completed<ValidationResult<List<LancamentoViewModel>>>(retorno);
        }

        [HttpGet("PorMes/{mes}/{ano}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no mês desejado.")]
        public async Task<IActionResult> LancamentosPorMes(int mes, int ano)
        {
            var retorno = await lancamentoApp.LancamentosPorMes(mes, ano);

            return Completed<ValidationResult<List<LancamentoViewModel>>>(retorno);
        }

        [HttpGet("EstornosMes/{mes}/{ano}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<LancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no mês desejado.")]
        public async Task<IActionResult> EstornosPorMes(int mes, int ano)
        {
            var retorno = await lancamentoApp.EstornosPorMes(mes, ano);

            return Completed<ValidationResult<List<LancamentoViewModel>>>(retorno);
        }

        [HttpGet("Consolidado")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados. A informação é do dia atual")]
        public async Task<IActionResult> ObterLancamentosConsolidado()
        {
            var retorno = await lancamentoApp.ObterLancamentosConsolidado();

            return Completed<ValidationResult<ConsolidadoLancamentoViewModel>>(retorno);
        }

        [HttpGet("Consolidado/{data}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados. A informação é do dia informado como data em string dd-mm-aaaa")]
        public async Task<IActionResult> ObterLancamentosConsolidadoPorData(string data)
        {
            var date = StringToDate(data);
            var retorno = await lancamentoApp.ObterLancamentosConsolidadoPorData(date);

            return Completed<ValidationResult<ConsolidadoLancamentoViewModel>>(retorno);
        }

        [HttpGet("Consolidado/sql/{data}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados (usando procedure). A informação é do dia informado como data em string dd-mm-aaaa")]
        public async Task<IActionResult> ObterLancamentosConsolidadoPorDataSql(string data)
        {
            var date = StringToDate(data);
            var retorno = await lancamentoApp.ObterLancamentosConsolidadoPorDataSql(date);

            return Completed<ValidationResult<ConsolidadoLancamentoViewModel>>(retorno);
        }

        [HttpGet("Consolidado/sql")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult<List<ConsolidadoLancamentoViewModel>>), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos no Consolidados (usando procedure). A informação é do dia informado como data em string dd-mm-aaaa")]
        public async Task<IActionResult> ObterLancamentosConsolidadoSql()
        {
            var retorno = await lancamentoApp.ObterLancamentosConsolidadoSql();

            return Completed<ValidationResult<ConsolidadoLancamentoViewModel>>(retorno);
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
