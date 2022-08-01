using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Liberty.Extensions.DependencyInjection
{
    internal sealed class DynamicServiceProvider
    {
        private IReadOnlyDictionary<Type, object> _services;

        internal static IServiceScope CreateScope(IServiceProvider serviceProvider, IEnumerable<object> services)
        {
            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            var scope = serviceProvider.CreateScope();
            var dynamicProvider = scope.ServiceProvider.GetRequiredService<DynamicServiceProvider>();
            var parentDynamicProvider = serviceProvider.GetRequiredService<DynamicServiceProvider>();
            var totalServices = services.Concat(parentDynamicProvider.GetServicesOrEmpty());
            dynamicProvider.InitializeService(totalServices);
            return scope;
        }

        internal static object GetService(IServiceProvider serviceProvider, Type serviceType)
        {
            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            
            var dynamicProvider = serviceProvider.GetRequiredService<DynamicServiceProvider>();
            return dynamicProvider.GetService(serviceType);
        }

        private void InitializeService(IEnumerable<object> services)
        {
            _services = services.ToDictionary(x => x.GetType(), x => x);
        }

        private IEnumerable<object> GetServicesOrEmpty()
        {
            return _services?.Values ?? Enumerable.Empty<object>();
        }

        private object GetService(Type serviceType)
        {
            if (_services is null)
            {
                throw new InvalidOperationException(string.Format(SR.OutOfDynamicScopeError, serviceType.FullName));
            }

            if (!_services.TryGetValue(serviceType, out var service))
            {
                throw new InvalidOperationException(string.Format(SR.NotFoundDynamicServiceError, serviceType.FullName));
            }
            
            return service;
        }
    }
}