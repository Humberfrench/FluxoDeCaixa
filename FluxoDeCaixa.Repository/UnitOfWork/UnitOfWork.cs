using Dietcode.Core.DomainValidator;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Repository.Context;
using FluxoDeCaixa.Repository.Interfaces;

namespace FluxoDeCaixa.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly FluxoDeCaixaContext dbContext;
        private readonly ValidationResult validationResult;

        private bool _disposed;

        public UnitOfWork(IContextManager contextManager)
        {
            dbContext = contextManager.GetContext();
            validationResult = new ValidationResult();
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ValidationResult SaveChanges()
        {
            try
            {
                var dados = dbContext.SaveChanges();
                var validationResult = new ValidationResult
                {
                    Retorno = dbContext.ChangeTracker.Entries().ToList()[0]
                };

            }
            //EntityValidationException
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    var inner = ex.InnerException;
                    if (inner != null)
                    {
                        if (inner.Message == "An error occurred while updating the entries. See the inner exception for details.")
                        {
                            var inner2 = inner.InnerException;
                            if (inner2 != null)
                            {
                                validationResult.Add(inner2.Message);
                            }
                        }
                        else
                        {
                            validationResult.Add(inner.Message);
                        }
                    }
                }
                else
                {
                    validationResult.Add(ex.Message);
                }
            }
            return validationResult;
        }

        public async Task<ValidationResult> SaveChangesAsync()
        {
            try
            {
                var dados = await dbContext.SaveChangesAsync();
                var validationResult = new ValidationResult
                {
                    Retorno = dbContext.ChangeTracker.Entries().ToList()[0]
                };

            }
            //EntityValidationException
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    var inner = ex.InnerException;
                    if (inner != null)
                    {
                        if (inner.Message == "An error occurred while updating the entries. See the inner exception for details.")
                        {
                            var inner2 = inner.InnerException;
                            if (inner2 != null)
                            {
                                validationResult.Add(inner2.Message);
                            }
                        }
                        else
                        {
                            validationResult.Add(inner.Message);
                        }
                    }
                }
                else
                {
                    validationResult.Add(ex.Message);
                }
            }
            return validationResult;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
