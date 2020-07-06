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
        Task<bool> insertRecords(List<Express> lsShippingExpress, CancellationToken cancellationToken);
        Task<bool> deleteAllRecords(CancellationToken cancellationToken);

        List<Express> retrieveAll(CancellationToken cancellationToken);
    }

    public class ShippingExpressRepository : RepositoryBase<Express>, IShippingExpressRepository
    {
        public ShippingExpressRepository(DBContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> deleteAllRecords(CancellationToken cancellationToken)
        {
            try
            {
                int row = dBContext.Database.ExecuteSqlCommand("DELETE FROM express");
                dBContext.SaveChanges();

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);

                return false;
            }
        } 

        public async Task<bool> insertRecords(List<Express> lsShippingExpress, CancellationToken cancellationToken)
        {
            try { 
                foreach (Express e in lsShippingExpress)
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

        public List<Express> retrieveAll(CancellationToken cancellationToken)
        {
            List<Express> lsExpress = dBContext.express.ToList();
            return lsExpress;
        }
    }
}
