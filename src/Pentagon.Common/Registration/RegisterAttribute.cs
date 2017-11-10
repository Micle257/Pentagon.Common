// -----------------------------------------------------------------------
//  <copyright file="RegisterAttribute.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Registration
{
    using System;

    /// <summary> Represents an attribute for registration module for dependency injection. </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public sealed class RegisterAttribute : Attribute
    {
        /// <summary> Initializes a new instance of the <see cref="RegisterAttribute" /> class. </summary>
        /// <param name="type"> The type. </param>
        /// <param name="bindReference"> The bind reference. </param>
        public RegisterAttribute(RegisterType type = RegisterType.Transient, Type bindReference = null)
        {
            Type = type;
            BindReference = bindReference;
        }

        /// <summary> Gets the type. </summary>
        /// <value> The <see cref="RegisterType" />. </value>
        public RegisterType Type { get; }

        /// <summary> Gets the type reference to module this registered module is bind to. </summary>
        /// <value> The <see cref="Type" />. </value>
        public Type BindReference { get; }
    }
}