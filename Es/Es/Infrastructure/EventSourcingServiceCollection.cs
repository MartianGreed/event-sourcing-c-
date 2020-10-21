using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Es;
using Es.Bus;
using Es.EventStore;

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
            
            GetAllTypesOf<ICommandHandler>(AppDomain.CurrentDomain.GetAssemblies()).ToList().ForEach((t)=>
            {
                services.AddTransient(typeof(ICommandHandler), t);
            });
            
            GetAllTypesOf<IEventHandler>(AppDomain.CurrentDomain.GetAssemblies()).ToList().ForEach((t)=>
            {
                services.AddTransient(typeof(IEventHandler), t);
            });

            services.AddTransient<IHandlerRegistry<IQueryHandler>>(provider =>
            {
                var service = new QueryHandlerRegistry(provider.GetServices<IQueryHandler>());
                return service;
            });
            services.AddTransient<IHandlerRegistry<ICommandHandler>>(provider =>
            {
                var service = new CommandHandlerRegistry(provider.GetServices<ICommandHandler>());
                return service;
            });
            services.AddTransient<IHandlerRegistry<IEventHandler>>(provider =>
            {
                var service = new EventHandlerRegistry(provider.GetServices<IEventHandler>());
                return service;
            });

            services.AddTransient<IQueryBus>(provider =>
            {
                var service = new QueryBus();
                var registry = provider.GetRequiredService<IHandlerRegistry<IQueryHandler>>();
                service.SetHandlerRegistry(registry as IHandlerRegistry<IQueryHandler>);
                return service;
            });
            
            services.AddTransient<ICommandBus>(provider =>
            {
                var service = new CommandBus();
                var registry = provider.GetRequiredService<IHandlerRegistry<ICommandHandler>>();
                service.SetHandlerRegistry(registry as IHandlerRegistry<ICommandHandler>);
                return service;
            });
            
            services.AddTransient<IEventBus>(provider =>
            {
                var service = new SyncEventBus();
                var registry = provider.GetRequiredService<IHandlerRegistry<IEventHandler>>();
                service.SetHandlerRegistry(registry as IHandlerRegistry<IEventHandler>);
                return service;
            });


            services.AddSingleton<IEventStore, InMemoryEventStore>();

            return services;
        }
        
        private static IEnumerable<TypeInfo> GetAllTypesOf<T>(Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
        }
    }

}