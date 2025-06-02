using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PeopleHub.Application.Common.Behaviors;
using System.Reflection;


namespace PeopleHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(options => {
                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            return services;
        }
    }
}
