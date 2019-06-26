// -----------------------------------------------------------------------
//  <copyright file="DispatcherServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using Microsoft.Extensions.DependencyInjection;

    public static class EventBusServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, bool defaultRunInBackground = false)
        {
            services.AddSingleton<IEventBus>(c => new InProcessBus(defaultRunInBackground));
            
            return services;
        }
    }
}