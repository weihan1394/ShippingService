using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShippingService.Core.Models;

namespace ShippingService.Core.Repositories
{
    public interface IShippingExpressRepository
    {
        bool InsertAllAsync(List<express> lsShippingExpress);
    }

    public class ShippingExpressRepository : RepositoryBase<express>, IShippingExpressRepository
    {
        public ShippingExpressRepository(ShippingExpressContext dbContext) : base(dbContext)
        {

        }

        public bool InsertAllAsync(List<express> lsShippingExpress)
        {
            try { 
                foreach (express e in lsShippingExpress)
                {
                    DbContext.Add(e);
                }
                DbContext.SaveChanges();

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
