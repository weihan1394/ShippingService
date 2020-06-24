using ShippingService.Core.Dtos;
using ShippingService.Core.Models;

namespace ShippingService.Core.Mapper
{
    class ExpressDtoMapper
    {
        public static ExpressDto map(express e)
        {
            return new ExpressDto
            {
                id = e.id,
                type = e.type,
                trackable = e.trackable,
                serviceLevel = e.service_level,
                country = e.country,
                countryCode = e.country_code,
                rateFlag = e.rate_flag,
                weight = e.weight,
                zone = e.zone
            };
        }

        public static ExpressDto update(ExpressDto expressDto, string vendor, double price)
        {
            expressDto.price = price;
            expressDto.vendor = vendor;

            return expressDto;
        }
    }
}
