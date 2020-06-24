using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Models;

namespace ShippingService.Core.Repositories
{
    public interface IShippingExpressRepository
    {
        Task<bool> insertRecords(List<express> lsShippingExpress, CancellationToken cancellationToken);
        Task deleteAllRecords(CancellationToken cancellationToken);

        List<express> retrieveAll(CancellationToken cancellationToken);
    }

    public class ShippingExpressRepository : RepositoryBase<express>, IShippingExpressRepository
    {
        public ShippingExpressRepository(DBContext dbContext) : base(dbContext)
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

        public List<express> retrieveAll(CancellationToken cancellationToken)
        {
            List<express> lsExpress = dBContext.express.ToList();
            return lsExpress;
        }
    }
}
