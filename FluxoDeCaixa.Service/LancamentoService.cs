using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using Dietcode.Core.DomainValidator;
using FluxoDeCaixa.Domain.ObjectValues;
using FluxoDeCaixa.Domain.Enum;
using Dietcode.Core.Lib;

namespace FluxoDeCaixa.Service
{

    public class LancamentoService : BaseService<Lancamento>, ILancamentoService
    {
        private readonly ILancamentoRepository lancamentoRepository;

        public LancamentoService(IBaseRepository<Lancamento> repository,
                                 ILancamentoRepository lancamentoRepository) : base(repository)
        {
            this.lancamentoRepository = lancamentoRepository;

        }
        public async Task<ValidationResult> Lancar(Lancamento lancamento)
        {
            var retorno = new ValidationResult();

            if (!lancamento.IsValid())
            {
                retorno.GetErros(lancamento.ValidationResult.Erros);
                return retorno;
            }

            //comment abaixo para usar o meio validador acima
            //if (lancamento.TipoLancamentoId == 0)
            //{
            //    retorno.Add("Tipo de lançamento não informado");
            //    return retorno;
            //}

            //if (lancamento.Valor <= 0)
            //{
            //    retorno.Add("Valor do lançamento não informado");
            //    return retorno;
            //}

            //if (lancamento.Data == DateTime.MinValue)
            //{
            //    retorno.Add("Data do lançamento não informado");
            //    return retorno;
            //}

            //if (lancamento.Descricao == string.Empty)
            //{
            //    retorno.Add("Descrição do lançamento não informado");
            //    return retorno;
            //}


            await lancamentoRepository.Adicionar(lancamento);

            retorno.Retorno = "Lancamento realizado com sucesso";
            return retorno;
        }

        public async Task<ValidationResult> Estornar(int id)
        {
            var retorno = new ValidationResult();
            var lancamento = await lancamentoRepository.ObterPorId(id);
            if (lancamento == null)
            {
                retorno.Add("Lançamento não encontrado");
                return retorno;
            }
            if (lancamento.Estornado)
            {
                retorno.Add("Lançamento já estornado");
                return retorno;
            }

            lancamento.Estornado = true;
            lancamento.Observacao += "**ESTORNADO";

            await lancamentoRepository.Atualizar(lancamento);
            retorno.Retorno = "Lancamento estornado com sucesso";
            return retorno;
        }

        public async Task<ValidationResult<List<Lancamento>>> LancamentosDoDia()
        {
            var retorno = new ValidationResult<List<Lancamento>>();
            var dados = await lancamentoRepository.Pesquisar(p => p.Data == DateTime.Now.Date && !p.Estornado);

            retorno.Retorno = dados.ToList();

            return retorno;
        }

        public async Task<ValidationResult<List<Lancamento>>> EstornosDoDia()
        {
            var retorno = new ValidationResult<List<Lancamento>>();
            var dados = await lancamentoRepository.Pesquisar(p => p.Data == DateTime.Now.Date && p.Estornado);

            retorno.Retorno = dados.ToList();

            return retorno;
        }


        public async Task<ValidationResult<List<Lancamento>>> LancamentosPorMes(int mes, int ano)
        {

            var retorno = new ValidationResult<List<Lancamento>>();
            if (mes == 0)
            {
                retorno.Add("Mês não informado");
                return retorno;
            }
            if (ano == 0)
            {
                retorno.Add("Mês não informado");
                return retorno;
            }

            var dados = (await lancamentoRepository.ObterTodos()).Where(p => p.Data.Month == mes &&
                                                                  p.Data.Year == ano && !p.Estornado);

            retorno.Retorno = dados.ToList();

            return retorno;

        }

        public async Task<ValidationResult<List<Lancamento>>> EstornosPorMes(int mes, int ano)
        {

            var retorno = new ValidationResult<List<Lancamento>>();
            if (mes == 0)
            {
                retorno.Add("Mês não informado");
                return retorno;
            }
            if (ano == 0)
            {
                retorno.Add("Mês não informado");
                return retorno;
            }

            var dados = (await lancamentoRepository.ObterTodos()).Where(p => p.Data.Month == mes &&
                                                                  p.Data.Year == ano && p.Estornado);

            retorno.Retorno = dados.ToList();

            return retorno;

        }

        public async Task<ValidationResult<List<Lancamento>>> LancamentosPorFaixaData(DateTime dataInicial, DateTime dataFinal)
        {

            var retorno = new ValidationResult<List<Lancamento>>();
            if (dataInicial == DateTime.MinValue)
            {
                retorno.Add("Data não informada");
                return retorno;
            }

            if (dataFinal == DateTime.MinValue)
            {
                retorno.Add("Data não informada");
                return retorno;
            }

            var dados = await lancamentoRepository.Pesquisar(p => p.Data >= dataInicial
                                                               && p.Data <= dataFinal
                                                               && !p.Estornado);

            retorno.Retorno = dados.ToList();

            return retorno;

        }
        public async Task<ValidationResult<List<Lancamento>>> LancamentosPorData(DateTime data)
        {

            var retorno = new ValidationResult<List<Lancamento>>();
            if (data == DateTime.MinValue)
            {
                retorno.Add("Data não informada");
                return retorno;
            }

            var dados = await lancamentoRepository.Pesquisar(p => p.Data == data && !p.Estornado);

            retorno.Retorno = dados.ToList();

            return retorno;

        }

        public async Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidado()
        {
            var retorno = new ValidationResult<ConsolidadoLancamento>();


            //fui 2X ao banco.
            //Aqui estou usando o Entity. CASO a tabela seja muito grande, pode ser que o Entity não seja a melhor opção.
            //Entao pode se optar por um query via dapper. Ou usar proc. POREM evito o uso de proc, por que
            // o DDD qiuer que a regra de negocio esteja no dominio. E proc é uma regra de banco.
            //E com o select basico no repositorio, não vou ferir a regra de negocio. e Se desejar trocar o SQL server
            //Por outro banco, não vou ter problemas. Apenas uma pequena adaptação no EF.
            //Então não precisarei migrar procs, ngocio, adaptar elas a linguagem do novo banco, etc.
            var debitos = (await lancamentoRepository.Pesquisar(p => p.TipoLancamentoId == EnumTipoLancamento.Debito.Int()
                                                                 && p.Data.Date == DateTime.Now.Date
                                                                 && !p.Estornado))
                                                                .Sum(l => l.Valor);
            var creditos = (await lancamentoRepository.Pesquisar(p => p.TipoLancamentoId == EnumTipoLancamento.Credito.Int()
                                                                 && p.Data.Date == DateTime.Now.Date
                                                                 && !p.Estornado))
                                                                .Sum(l => l.Valor);

            var saldo = creditos - debitos;

            retorno.Retorno = new ConsolidadoLancamento()
            {
                Data = DateTime.Now,
                ConsolidadoCredito = creditos,
                ConsolidadoDebito = debitos,
                SaldoDoDia = saldo
            };

            return retorno;
        }

        public async Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidadoPorData(DateTime data)
        {
            var retorno = new ValidationResult<ConsolidadoLancamento>();

            if (data == DateTime.MinValue)
            {
                retorno.Add("Data não informada");
                return retorno;
            }

            //fui 2X ao banco.
            //Aqui estou usando o Entity. CASO a tabela seja muito grande, pode ser que o Entity não seja a melhor opção.
            //Entao pode se optar por um query via dapper. Ou usar proc. POREM evito o uso de proc, por que
            // o DDD qiuer que a regra de negocio esteja no dominio. E proc é uma regra de banco.
            //E com o select basico no repositorio, não vou ferir a regra de negocio. e Se desejar trocar o SQL server
            //Por outro banco, não vou ter problemas. Apenas uma pequena adaptação no EF.
            //Então não precisarei migrar procs, ngocio, adaptar elas a linguagem do novo banco, etc.
            var debitos = (await lancamentoRepository.ObterTodos()).Where(p => p.TipoLancamentoId == EnumTipoLancamento.Debito.Int()
                                                                       && p.Data.Date == data
                                                                       && !p.Estornado)
                                                                      .Sum(l => l.Valor);
            var creditos = (await lancamentoRepository.ObterTodos()).Where(p => p.TipoLancamentoId == EnumTipoLancamento.Credito.Int()
                                                                        && p.Data.Date == data
                                                                        && !p.Estornado)
                                                                       .Sum(l => l.Valor);

            var saldo = creditos - debitos;

            retorno.Retorno = new ConsolidadoLancamento()
            {
                Data = data,
                ConsolidadoCredito = creditos,
                ConsolidadoDebito = debitos,
                SaldoDoDia = saldo
            };

            return retorno;
        }

        public async Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidadoSql()
        {
            /*
             Normalmente eu peferiria o ObterLancamentosConsolidado. Justamente pelo DDD e o negócio
            estar no domínio. Mas como o objetivo é mostrar o uso do e de proc Dapper, vou usar o SQL.
            para conferencia estará exposto o endpoint
            */
            var retorno = new ValidationResult<ConsolidadoLancamento>();
            retorno.Retorno = await lancamentoRepository.ObterConsolidadoDia();
            return retorno;
        }

        public async Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidadoPorDataSql(DateTime data)
        {
            var retorno = new ValidationResult<ConsolidadoLancamento>();
            if (data == DateTime.MinValue)
            {
                retorno.Add("Data não informada");
                return retorno;
            }

            /*
             Normalmente eu peferiria o ObterLancamentosConsolidado. Justamente pelo DDD e o negócio
            estar no domínio. Mas como o objetivo é mostrar o uso do e de proc Dapper, vou usar o SQL.
            para conferencia estará exposto o endpoint
            */
            retorno.Retorno = await lancamentoRepository.ObterConsolidadoPorData(data);
            return retorno;
        }
    }
}