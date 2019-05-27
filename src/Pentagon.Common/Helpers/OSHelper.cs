// -----------------------------------------------------------------------
//  <copyright file="OSHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary> Provides methods for determining current OS platform. </summary>
    public static class OSHelper
    {
        /// <summary> Gets a value indicating whether current OS is Mono. </summary>
        /// <value> <c> true </c> if this OS is mono; otherwise, <c> false </c>. </value>
        public static bool IsMono =>
                Type.GetType(typeName: "Mono.Runtime") != null;

        /// <summary> Gets a value indicating whether current OS is Windows. </summary>
        /// <value> <c> true </c> if this OS is Windows; otherwise, <c> false </c>. </value>
        public static bool IsWindows =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary> Gets a value indicating whether current OS is Linux. </summary>
        /// <value> <c> true </c> if this OS is Linux; otherwise, <c> false </c>. </value>
        public static bool IsLinux =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary> Gets a value indicating whether current OS is Mac. </summary>
        /// <value> <c> true </c> if this OS is Mac; otherwise, <c> false </c>. </value>
        public static bool IsMac =>
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}