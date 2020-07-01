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
        Task<bool> insertRecords(List<bulk> lsShippingExpress, CancellationToken cancellationToken);
        Task deleteAllRecords(CancellationToken cancellationToken);
    }

    public class ShippingBulkRepository : RepositoryBase<bulk>, IShippingBulkRepository
    {
        public ShippingBulkRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public async Task deleteAllRecords(CancellationToken cancellationToken)
        {
            try
            {
                dBContext.Database.ExecuteSqlCommand("DELETE FROM bulk");
                dBContext.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } 

        public async Task<bool> insertRecords(List<bulk> lsShippingBulks, CancellationToken cancellationToken)
        {
            try { 
                foreach (bulk e in lsShippingBulks)
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