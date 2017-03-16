using Mickey.Core.Domain.Uow;
using System.Threading.Tasks;
using System.Transactions;

namespace Mickey.EntityFramework.Uow
{
    public class EfUnitOfWork : UnitOfWorkBase
    {
        protected IDbContext IDbContext { get; private set; }
        protected TransactionScope CurrentTransaction;

        public EfUnitOfWork(IDbContext dbContext, IUnitOfWorkDefaultOptions defaultOptions) : base(defaultOptions)
        {
            IDbContext = dbContext;
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        public override void SaveChanges()
        {
            IDbContext.SaveChanges();
        }

        public override async Task SaveChangesAsync()
        {
            await IDbContext.SaveChangesAsync();
        }

        protected override void CompleteUow()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }

            DisposeUow();
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }

            DisposeUow();
        }


        protected override void DisposeUow()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
                CurrentTransaction = null;
            }
        }
    }
}
