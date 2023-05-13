using Dietcode.Core.DomainValidator;
using FluxoDeCaixa.Domain.Interfaces.Repository;

namespace FluxoDeCaixa.App
{
    public class BaseApp<T> where T : new()
    {
        private readonly IUnitOfWork uow;
        protected ValidationResult<T> baseValidationResult;

        public BaseApp(IUnitOfWork uow)
        {
            this.uow = uow;
            baseValidationResult = new ValidationResult<T>();
        }

        public void BeginTransaction()
        {
            uow.BeginTransaction();
        }

        public async Task Commit()
        {
            var retorno = await Task.Run(() => uow.SaveChanges());

            if (!retorno.Valid)
            {
                retorno.Erros.ToList().ForEach(e => baseValidationResult.Add(e.Message));
            }
        }
    }
}