using Autofac;
using ShippingService.Core.Repositories;
using ShippingService.Core.Services;

namespace ShippingService.Core.RegisterModules
{
    public class GeneralRegisterModule : Module
    {
        // dependency inject
        // link interface to service
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarService>().As<ICarService>();
            builder.RegisterType<ShippingRateService>().As<IShippingRateService>();
        }
    }
}
