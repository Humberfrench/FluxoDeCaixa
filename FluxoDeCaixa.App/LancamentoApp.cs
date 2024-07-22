using Dietcode.Api.Core.Results;
using Dietcode.Core.DomainValidator;
using Dietcode.Core.Lib;
using FluxodeCaixa.ViewModel;
using FluxodeCaixa.ViewModel.Interfaces;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.App
{

    public class LancamentoApp : BaseApp<LancamentoViewModel>, ILancamentoApp
    {
        private readonly ILancamentoService service;

        public LancamentoApp(IUnitOfWork uow, ILancamentoService service) : base(uow)
        {
            this.service = service;
        }


        public async Task<MethodResult> Lancar(LancamentoViewModel lancamento)
        {
            BeginTransaction();

            //Esse ConvertObjects � uma extens�o cujo eu criei para converter objetos de um tipo para outro
            //Antigamente usava o Automapper. Porem ele exige mais codigos, declarar de/para, etc.
            // neste caso como trabalhei com adquirentes que usam campos como codigo_cliente e eu uso CodigoCliente
            // o ConvertObjects faz a convers�o automatica. Basta ter os atributos de Json indicando. 
            //Na ausencia de JsonProperty as props s�o convertidas para outra de outra classe APENAS com nomes iguais.
            // Ou seja CodigoCliente da Classe A ser� convertido para CodigoCliente da Classe B
            var dadoIncluir = lancamento.ConvertObjects<Lancamento>();
            var retorno = await service.Lancar(dadoIncluir);

            /*Esta classe � um proxy de multiplas utilidades.
            Em primeiro passo ela � usada para converter qualquer objeto vindo do dominio para a ViewModel, assim
            n�o preciso ficar expondo os objetos de dominio para o mundo externo.
            Como uso o pattern de balidation result, isso � util para commitar ou n�o as grava��es.
            Supnhanos item/pedido, cliei a classe de pedido e as classes de items. vincluei e mandei gravar no Entity
            e depois disso houve algum prooblema em um dos itens, e n�o posso gravar o dado.
            ent�o n�o chamo o commit, que efetua o SAVECHANGES. E n�o gravo o pedido. Justamente para n�o gravar errado
            e Dados incompletos. Aqui � onde gerencio a transa��o via unit of work. (outro design pattern).
            Abaixo. Se o retorno do service for valido/ok (n�o tem erros) eu chamo o commit.
            E em caso de erro do commit eu trato e expondo adiante o erro. Geralmente em commit
            */
            if (retorno.Valid)
            {
                //commit transaction
                await CommitAync();
                //commit error
                if (baseValidationResult.Invalid)
                {
                    return BadRequest(ConvertValidationErrors(baseValidationResult.Erros.ToList()));
                }
            }

            return Ok(retorno);

        }

        public async Task<MethodResult> Estornar(int id)
        {
            BeginTransaction();

            var retorno = await service.Estornar(id);

            if (retorno.Valid)
            {
                //commit transaction
                await CommitAync();
                //commit error
                if (baseValidationResult.Invalid)
                {
                    return BadRequest(ConvertValidationErrors(baseValidationResult.Erros.ToList()));
                }
            }

            return Ok(retorno);
        }

        public async Task<MethodResult> LancamentosDoDia()
        {
            var retorno = new ValidationResult<List<LancamentoViewModel>>();
            var consulta = await service.LancamentosDoDia();
            retorno.Retorno = consulta.Retorno.ConvertObjects<List<LancamentoViewModel>>();

            if (retorno.Retorno.Count == 0)
            {
                return NotFound("No results returned");
            }

            return Ok(retorno);
        }

        public async Task<MethodResult> EstornosDoDia()
        {
            var retorno = new ValidationResult<List<LancamentoViewModel>>();
            var consulta = await service.EstornosDoDia();
            retorno.Retorno = consulta.Retorno.ConvertObjects<List<LancamentoViewModel>>();

            if (retorno.Retorno.Count == 0)
            {
                return NotFound("No results returned");
            }

            return Ok(retorno);
        }

        public async Task<MethodResult> LancamentosPorData(DateTime data)
        {
            var retorno = new ValidationResult<List<LancamentoViewModel>>();
            var consulta = await service.LancamentosDoDia();

            if (consulta.Invalid)
            {
                return BadRequest(ConvertValidationErrors(consulta.Erros.ToList()));
            }

            retorno.Retorno = consulta.Retorno.ConvertObjects<List<LancamentoViewModel>>();

            if (retorno.Retorno.Count == 0)
            {
                return NotFound("No results returned");
            }

            return Ok(retorno);
        }

        public async Task<MethodResult> LancamentosPorFaixaData(DateTime dataInicial, DateTime dataFinal)
        {
            var retorno = new ValidationResult<List<LancamentoViewModel>>();
            var consulta = await service.LancamentosDoDia();

            if (consulta.Invalid)
            {
                return BadRequest(ConvertValidationErrors(consulta.Erros.ToList()));
            }

            retorno.Retorno = consulta.Retorno.ConvertObjects<List<LancamentoViewModel>>();

            if (retorno.Retorno.Count == 0)
            {
                return NotFound("No results returned");
            }

            return Ok(retorno);
        }

        public async Task<MethodResult> LancamentosPorMes(int mes, int ano)
        {
            var retorno = new ValidationResult<List<LancamentoViewModel>>();
            var consulta = await service.LancamentosPorMes(mes, ano);

            if (consulta.Invalid)
            {
                return BadRequest(ConvertValidationErrors(consulta.Erros.ToList()));
            }

            retorno.Retorno = consulta.Retorno.ConvertObjects<List<LancamentoViewModel>>(8);

            if (retorno.Retorno.Count == 0)
            {
                return NotFound("No results returned");
            }

            return Ok(retorno);
        }
        public async Task<MethodResult> EstornosPorMes(int mes, int ano)
        {
            var retorno = new ValidationResult<List<LancamentoViewModel>>();
            var consulta = await service.EstornosPorMes(mes, ano);

            if (consulta.Invalid)
            {
                return BadRequest(ConvertValidationErrors(consulta.Erros.ToList()));
            }

            retorno.Retorno = consulta.Retorno.ConvertObjects<List<LancamentoViewModel>>(8);

            if (retorno.Retorno.Count == 0)
            {
                return NotFound("No results returned");
            }

            return Ok(retorno);
        }

        public async Task<MethodResult> ObterLancamentosConsolidado()
        {
            var retorno = new ValidationResult<ConsolidadoLancamentoViewModel>();
            var consulta = await service.ObterLancamentosConsolidado();
            retorno.Retorno = consulta.Retorno.ConvertObjects<ConsolidadoLancamentoViewModel>();

            return Ok(retorno);
        }

        public async Task<MethodResult> ObterLancamentosConsolidadoPorData(DateTime data)
        {
            var retorno = new ValidationResult<ConsolidadoLancamentoViewModel>();
            var consulta = await service.ObterLancamentosConsolidadoPorData(data);

            if (consulta.Invalid)
            {
                return BadRequest(ConvertValidationErrors(consulta.Erros.ToList()));
            }

            retorno.Retorno = consulta.Retorno.ConvertObjects<ConsolidadoLancamentoViewModel>();

            return Ok(retorno);
        }

        public async Task<MethodResult> ObterLancamentosConsolidadoPorDataSql(DateTime data)
        {
            var retorno = new ValidationResult<ConsolidadoLancamentoViewModel>();
            var consulta = await service.ObterLancamentosConsolidadoPorDataSql(data);

            if (consulta.Invalid)
            {
                return BadRequest(ConvertValidationErrors(consulta.Erros.ToList()));
            }

            retorno.Retorno = consulta.Retorno.ConvertObjects<ConsolidadoLancamentoViewModel>();

            return Ok(retorno);
        }

        public async Task<MethodResult> ObterLancamentosConsolidadoSql()
        {
            var retorno = new ValidationResult<ConsolidadoLancamentoViewModel>();
            var consulta = await service.ObterLancamentosConsolidadoSql();
            retorno.Retorno = consulta.Retorno.ConvertObjects<ConsolidadoLancamentoViewModel>();

            return Ok(retorno);
        }
    }
}
