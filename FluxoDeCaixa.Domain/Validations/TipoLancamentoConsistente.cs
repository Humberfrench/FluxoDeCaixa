using Dietcode.Core.DomainValidator;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Specifications.Lancamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Domain.Validations
{
    public class TipoLancamentoConsistente : Validator<Lancamento>
    {
        public TipoLancamentoConsistente()
        {
            var descricaoValida = new DescricaoPreenchido();
            var tipoLancamentoValido = new TipoLancamentoPreenchido();
            var valorValido = new ValorPreenchido();
            var dataValida = new DataPreenchido();

            base.AdicionarRegra(nameof(descricaoValida), new Rule<Lancamento>(descricaoValida, "Preencher o campo descrição corretamente"));
            base.AdicionarRegra(nameof(tipoLancamentoValido), new Rule<Lancamento>(tipoLancamentoValido, "Preencher o campo tipo de lançamento corretamente"));
            base.AdicionarRegra(nameof(valorValido), new Rule<Lancamento>(valorValido, "Preencher o campo valor corretamente"));
            base.AdicionarRegra(nameof(dataValida), new Rule<Lancamento>(dataValida, "Preencher o campo data corretamente"));
        }
    }
}
