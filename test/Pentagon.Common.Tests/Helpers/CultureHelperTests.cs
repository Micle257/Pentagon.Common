// -----------------------------------------------------------------------
//  <copyright file="CultureHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Common.Tests.Helpers {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Pentagon.Helpers;
    using Xunit;
    using Xunit.Abstractions;

    public class CultureHelperTests
    {
        readonly ITestOutputHelper _outputHelper;

        public CultureHelperTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        
        [Fact]
        public void Exists()
        {
            var cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                          .Where(x => !string.IsNullOrEmpty(x?.Name));

            var allNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            allNames.UnionWith(cultureInfos.Select(x => x?.Name));
            allNames.UnionWith(new string[] { null }); // for invariant

            var asserts = true;

            foreach (var name in allNames)
            {
               var result = CultureHelper.Exists(name);

               if (!result)
                    _outputHelper.WriteLine($"Culture name do not exists: {name}.");

               asserts &= result;
            }

            Assert.True(asserts);
        }

        [Fact]
        public void TryParse_NullCulture_ReturnsInvariantCulture()
        {
            var result = CultureHelper.TryParse(null, out var culture);

            Assert.True(result);
            Assert.Equal(CultureInfo.InvariantCulture, culture);
        }
    }
}