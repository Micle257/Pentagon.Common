// -----------------------------------------------------------------------
//  <copyright file="DispatcherServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public static class DispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services, bool autoAddCommandHandlers = false)
        {
            services.AddSingleton<IDispatcher>(c => new Dispatcher(c));

            if (!autoAddCommandHandlers)
                return services;

            var commands = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(a => a.GetTypes())
                                    .Where(a => a.IsClass && !a.IsAbstract)
                                    .Distinct();

            foreach (var command in commands)
            {
                var interf = command.GetInterfaces()
                                    .Where(b => b.GenericTypeArguments.Length == 2)
                                    .FirstOrDefault(a => a.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));

                if (interf == null)
                    continue;

                services.AddTransient(interf, command);
            }

            return services;
        }
    }
}