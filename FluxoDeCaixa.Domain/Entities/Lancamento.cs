﻿
using Dietcode.Core.DomainValidator;
using FluxoDeCaixa.Domain.Specifications.Lancamentos;
using FluxoDeCaixa.Domain.Validations;

namespace FluxoDeCaixa.Domain.Entity;

public partial class Lancamento
{
    private readonly ValidationResult validationResult;
    private bool? isValid;

    public Lancamento()
    {
        validationResult = new ValidationResult();
        isValid = null;

        Descricao = string.Empty;
        Observacao = string.Empty;
        TipoLancamento = new TipoLancamento();
    }

    public int LancamentoId { get; set; }

    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public int TipoLancamentoId { get; set; }

    public string Descricao { get; set; }

    public string Observacao { get; set; }

    public bool Estornado { get; set; }

    public virtual TipoLancamento TipoLancamento { get; set; }


    public virtual ValidationResult ValidationResult => validationResult;

    public virtual bool IsValid()
    {
        if (!isValid.HasValue)
        {
            var validationDados = Validar(this);
            if (validationDados.Invalid)
            {
                validationDados.Erros.ToList().ForEach(e => validationResult.Add(e));
            }
            return validationResult.Valid;
        }
        return isValid.Value;

    }

    public virtual ValidationResult Validar(Lancamento entity)
    {
        var entidadeDescricaoValidate = new TipoLancamentoConsistente();
        var validationResultEntidade = entidadeDescricaoValidate.Validar(entity);
        if (validationResultEntidade.Invalid)
        {
            validationResult.GetErros(validationResultEntidade.Erros);
            validationResultEntidade.Erros.ToList().ForEach(e => validationResult.Add(e));
        }
        return validationResult;
    }


}