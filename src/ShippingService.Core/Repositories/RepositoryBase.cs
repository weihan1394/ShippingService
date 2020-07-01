namespace ShippingService.Core.Repositories
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : class
    {
        protected DBContext dBContext { get; }

        public RepositoryBase(DBContext dbContext)
        {
            dBContext = dbContext;
        }
    }
}
