// -----------------------------------------------------------------------
//  <copyright file="CultureHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Provides helper logic for culture tasks. </summary>
    public static class CultureHelper
    {
        /// <summary> The culture names. </summary>
        [NotNull]
        static readonly HashSet<string> CultureNames = CreateCultureNames();

        /// <summary> Determines if given culture name exists. </summary>
        /// <param name="name"> The name. </param>
        /// <returns> Flag indicating whether culture exists. </returns>
        public static bool Exists([CanBeNull] string name) => CultureNames.Contains(item: name);

        /// <summary> Tries to parse the culture name (if culture is <c> null </c> invariant is returned) to the <see cref="CultureInfo" />. </summary>
        /// <param name="culture"> The culture. </param>
        /// <param name="cultureInfo"> The culture information. </param>
        /// <returns> Flag indicating if parsing was successful. </returns>
        public static bool TryParse([CanBeNull] string culture, [CanBeNull] out CultureInfo cultureInfo)
        {
            if (Exists(name: culture))
            {
                cultureInfo = culture == null ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(name: culture);
                return true;
            }

            cultureInfo = null;
            return false;
        }

        /// <summary> Creates the culture names. </summary>
        /// <returns> A string hash set. </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        static HashSet<string> CreateCultureNames()
        {
            var cultureInfos = CultureInfo.GetCultures(types: CultureTypes.AllCultures)
                                          .Where(x => !string.IsNullOrEmpty(value: x?.Name))
                                          .ToArray();

            var allNames = new HashSet<string>(comparer: StringComparer.OrdinalIgnoreCase);

            allNames.UnionWith(cultureInfos.Select(x => x?.TwoLetterISOLanguageName));
            allNames.UnionWith(cultureInfos.Select(x => x?.Name));
            allNames.UnionWith(new string[] {null}); // for invariant

            return allNames;
        }
    }
}