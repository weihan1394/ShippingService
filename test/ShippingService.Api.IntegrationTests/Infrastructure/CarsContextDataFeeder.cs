using System;
using ShippingService.Core;
using ShippingService.Core.Models;

namespace ShippingService.Api.IntegrationTests.Infrastructure
{
    public class CarsContextDataFeeder
    {
        public static void Feed(CarsContext dbContext)
        {
            var car1 = new car
            {
                plate = "DW 12345",
                model = "Toyota Avensis",
            };
            dbContext.cars.Add(car1);

            dbContext.SaveChanges();
        }
    }
}
