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
            //builder.RegisterType<ShippingExpressRepository>().As<IShippingExpressRepository>().AsSelf();
            builder.RegisterType<ShippingExpressRepository>().As<IShippingExpressRepository>();
            builder.RegisterType<ShippingBulkRepository>().As<IShippingBulkRepository>();
            builder.RegisterType<ShippingPostalRepository>().As<IShippingPostalRepository>();
            builder.RegisterType<CarService>().As<ICarService>();

            builder.Register(ctx => new ShippingRateService(ctx.Resolve<IShippingExpressRepository>(), ctx.Resolve<IShippingBulkRepository>(), ctx.Resolve<IShippingPostalRepository>())).As<IShippingRateService>();
        }
    }
}
