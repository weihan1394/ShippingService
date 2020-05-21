using Autofac;
using ShippingService.Core.RegisterModules;

namespace ShippingService.Api.Infrastructure.Registrations
{
    public static class ContainerRegistration
    {
        public static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<GeneralRegisterModule>();
        }
    }
}
