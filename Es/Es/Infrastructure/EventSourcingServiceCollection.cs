using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Es;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventSourcingServiceCollectionExtensions
    {
        public static IServiceCollection AddEventSourcingServices(this IServiceCollection services)
        {
            GetAllTypesOf<IQueryHandler>(AppDomain.CurrentDomain.GetAssemblies()).ToList().ForEach((t)=>
            {
                services.AddTransient(typeof(IQueryHandler), t);
            });

            services.AddScoped<IQueryHandlerRegistry>(provider =>
            {
                var service = new QueryHandlerRegistry(provider.GetServices<IQueryHandler>());
                return service;
            });

            services.AddScoped<IQueryBus>(provider =>
            {
                var service = new QueryBus();
                var registry = provider.GetRequiredService<IQueryHandlerRegistry>();
                service.SetHandlerRegistry(registry as IQueryHandlerRegistry);
                return service;
            });
            
            return services;
        }
        
        private static IEnumerable<TypeInfo> GetAllTypesOf<T>(Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
        }
    }

}