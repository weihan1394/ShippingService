namespace ShippingService.Core.Repositories
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : class
    {
        protected ShippingExpressContext DbContext { get; }

        public RepositoryBase(ShippingExpressContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
