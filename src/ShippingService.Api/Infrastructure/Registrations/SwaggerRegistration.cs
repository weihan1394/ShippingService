using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ShippingService.Api.Infrastructure.Registrations
{
    public static class SwaggerRegistration
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Markono ShippingService Api",
                    Version = "v1",
                    Description = "Markono Shipping Service ",
                    Contact = new OpenApiContact
                    {
                        Name = "chiawh@markono.com",
                        Url = new Uri("mailto:chiawh@markono.com?subject=Shipping Service Feedback"),
                    }
                });

                swaggerOptions.OrderActionsBy(x => x.RelativePath);
                swaggerOptions.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShippingService.Api.xml"));

  
            });
        }
    }
}
