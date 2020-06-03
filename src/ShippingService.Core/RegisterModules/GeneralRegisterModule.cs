using Autofac;
using ShippingService.Core.Repositories;
using ShippingService.Core.Services;

namespace ShippingService.Core.RegisterModules
{
    public class GeneralRegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarService>().As<ICarService>();
        }
    }
}
