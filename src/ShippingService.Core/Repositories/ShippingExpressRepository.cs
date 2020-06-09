using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShippingService.Core.Models;

namespace ShippingService.Core.Repositories
{
    public interface IShippingExpressRepository
    {
        Task<bool> InsertAllAsync(List<express> lsShippingExpress, CancellationToken cancellationToken);
    }

    public class ShippingExpressRepository : RepositoryBase<express>, IShippingExpressRepository
    {
        public ShippingExpressRepository(ShippingExpressContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> InsertAllAsync(List<express> lsShippingExpress, CancellationToken cancellationToken)
        {
            try { 
                foreach (express e in lsShippingExpress)
                {
                    await DbContext.AddAsync(e, cancellationToken);
                }
                await DbContext.SaveChangesAsync(cancellationToken);

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
