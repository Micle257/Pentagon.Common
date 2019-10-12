// -----------------------------------------------------------------------
//  <copyright file="AppDomainHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides helper logic for <see cref="AppDomain"/>.
    /// </summary>
    public static class AppDomainHelper
    {
        /// <summary>
        /// Gets the loaded types from app domain.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <returns>Iteration of <see cref="Type"/>.</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<Type> GetLoadedTypes([NotNull] this AppDomain appDomain) => appDomain.GetAssemblies().SelectMany(GetLoadableTypes);

        /// <summary>
        /// Gets the loadable types from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>Iteration of <see cref="Type"/>.</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<Type> GetLoadableTypes([NotNull] Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}