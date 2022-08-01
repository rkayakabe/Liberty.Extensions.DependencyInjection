using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Liberty.Extensions.DependencyInjection.Extensions
{
    public static class DynamicServiceCollectionExtensions
    {
        public static void TryAddDynamicScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryAddDynamicScoped(typeof(TService), typeof(TImplementation));
        }
        
        public static void TryAddDynamicScoped(this IServiceCollection serviceCollection, Type serviceType, Type implementationType)
        {
            serviceCollection.TryAddScoped<DynamicServiceProvider>();
            serviceCollection.TryAddScoped(serviceType, x => x.GetDynamicService(implementationType));
        }
        
        public static void TryAddDynamicScoped<TService>(this IServiceCollection serviceCollection)
            where TService : class
        {
            serviceCollection.TryAddDynamicScoped(typeof(TService));
        }
        
        public static void TryAddDynamicScoped(this IServiceCollection serviceCollection, Type serviceType)
        {
            serviceCollection.TryAddScoped<DynamicServiceProvider>();
            serviceCollection.TryAddScoped(serviceType, x => x.GetDynamicService(serviceType));
        }

        public static IServiceCollection AddDynamicScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.AddDynamicScoped(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddDynamicScoped(this IServiceCollection serviceCollection, Type serviceType, Type implementationType)
        {
            serviceCollection.TryAddScoped<DynamicServiceProvider>();
            return serviceCollection.AddScoped(serviceType, x => x.GetDynamicService(implementationType));
        }

        public static IServiceCollection AddDynamicScoped<TService>(this IServiceCollection serviceCollection)
            where TService : class
        {
            serviceCollection.TryAddScoped<DynamicServiceProvider>();
            return serviceCollection.AddDynamicScoped(typeof(TService));
        }
        
        public static IServiceCollection AddDynamicScoped(this IServiceCollection serviceCollection, Type serviceType)
        {
            serviceCollection.TryAddScoped<DynamicServiceProvider>();
            return serviceCollection.AddScoped(serviceType, x => x.GetDynamicService(serviceType));
        }
    }
}