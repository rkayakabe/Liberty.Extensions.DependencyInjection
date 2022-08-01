using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Liberty.Extensions.DependencyInjection.Extensions
{
    public static class DynamicServiceProviderExtensions
    {
        public static IServiceScope CreateDynamicScope(this IServiceProvider serviceProvider, IEnumerable<object> services)
        {
            return DynamicServiceProvider.CreateScope(serviceProvider, services);
        }
        
        public static IServiceScope CreateDynamicScope(this IServiceProvider serviceProvider, params object[] services)
        {
            return DynamicServiceProvider.CreateScope(serviceProvider, services);
        }

        internal static object GetDynamicService(this IServiceProvider serviceProvider, Type serviceType)
        {
            return DynamicServiceProvider.GetService(serviceProvider, serviceType);
        }
    }
}