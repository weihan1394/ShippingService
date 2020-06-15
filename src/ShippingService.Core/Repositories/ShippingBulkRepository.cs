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
                int row = dBContext.Database.ExecuteSqlCommand("DELETE FROM express");
                dBContext.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } 

        public async Task<bool> insertRecords(List<express> lsShippingExpress, CancellationToken cancellationToken)
        {
            try { 
                foreach (express e in lsShippingExpress)
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
