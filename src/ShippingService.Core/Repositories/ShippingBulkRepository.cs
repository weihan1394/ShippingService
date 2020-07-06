using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Models;

namespace ShippingService.Core.Repositories
{
    public interface IShippingBulkRepository
    {
        Task<bool> insertRecords(List<Bulk> lsShippingExpress, CancellationToken cancellationToken);
        Task<bool> deleteAllRecords(CancellationToken cancellationToken);
    }

    public class ShippingBulkRepository : RepositoryBase<Bulk>, IShippingBulkRepository
    {
        public ShippingBulkRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> deleteAllRecords(CancellationToken cancellationToken)
        {
            try
            {
                dBContext.Database.ExecuteSqlCommand("DELETE FROM bulk");
                dBContext.SaveChanges();

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        } 

        public async Task<bool> insertRecords(List<Bulk> lsShippingBulks, CancellationToken cancellationToken)
        {
            try { 
                foreach (Bulk e in lsShippingBulks)
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
