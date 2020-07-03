using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Models;

namespace ShippingService.Core.Repositories
{
    public interface IShippingPostalRepository
    {
        Task<bool> insertRecords(List<postal> lsShippingExpress, CancellationToken cancellationToken);
        Task<bool> deleteAllRecords(CancellationToken cancellationToken);
    }

    public class ShippingPostalRepository : RepositoryBase<postal>, IShippingPostalRepository
    {
        public ShippingPostalRepository(DBContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> deleteAllRecords(CancellationToken cancellationToken)
        {
            try
            {
                int row = dBContext.Database.ExecuteSqlCommand("DELETE FROM postal");
                dBContext.SaveChanges();

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);

                return false;
            }
        } 

        public async Task<bool> insertRecords(List<postal> lsShippingPostal, CancellationToken cancellationToken)
        {
            try { 
                foreach (postal e in lsShippingPostal)
                {
                    await dBContext.AddAsync(e, cancellationToken);
                }
                await dBContext.SaveChangesAsync(cancellationToken);

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
