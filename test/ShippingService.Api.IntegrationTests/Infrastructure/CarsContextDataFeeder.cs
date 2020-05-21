using System;
using ShippingService.Core;
using ShippingService.Core.Models;

namespace ShippingService.Api.IntegrationTests.Infrastructure
{
    public class CarsContextDataFeeder
    {
        public static void Feed(CarsContext dbContext)
        {
            var owner1 = new Owner
            {
                FirstName = "Dom",
                LastName = "Cobb",
            };
            dbContext.Owners.Add(owner1);

            var car1 = new Car
            {
                Plate = "DW 12345",
                Model = "Toyota Avensis",
                Owner = owner1,
            };
            dbContext.Cars.Add(car1);

            dbContext.SaveChanges();
        }
    }
}
