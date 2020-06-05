using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ShippingService.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ShippingService.Core.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllSortedByPlateAsync(CancellationToken cancellationToken);
    }

    public class CarService : ICarService
    {
        private readonly CarsContext _dbContext;

        public CarService(CarsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CarDto>> GetAllSortedByPlateAsync(CancellationToken cancellationToken)
        {
            var cars = await _dbContext.cars
                .AsNoTracking()
                .OrderBy(x => x.plate)
                .ToListAsync(cancellationToken);

            return cars.Select(x => new CarDto
            {
                Id = x.id,
                Plate = x.plate,
                Model = x.model,
            });
        }
    }
}
