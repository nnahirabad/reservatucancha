
using canchasfutbol.Application.Behaviours;
using canchasfutbol.Application.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;



namespace canchasfutbol.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfigurationManager configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
             services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
            });

             
             
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<JwtTokenGenerator>();
          
            


            return services;
        }
    }

}
