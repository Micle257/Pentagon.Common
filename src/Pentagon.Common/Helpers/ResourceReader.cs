// -----------------------------------------------------------------------
//  <copyright file="ResourceReader.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class ResourceReader
    {
        public string Read(string name)
        {
            var ass = Assembly.GetExecutingAssembly();
            var stream = ass.GetManifestResourceStream(typeof(ResourceReader), name);
            return new StreamReader(stream, Encoding.Unicode).ReadToEnd();
        }
    }
}