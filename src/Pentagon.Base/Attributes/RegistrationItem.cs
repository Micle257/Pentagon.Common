namespace Pentagon.Attributes {
    using System;

    /// <summary> Represents a registration item. </summary>
    public class RegistrationItem
    {
        /// <summary> Gets or sets the type of the implementation. </summary>
        /// <value> The <see cref="Type" />. </value>
        public Type ImplementationType { get; set; }

        /// <summary> Gets or sets the type of the binding. </summary>
        /// <value> The <see cref="Type" />. </value>
        public Type BindingType { get; set; }

        /// <summary> Gets or sets the type. </summary>
        /// <value> The <see cref="RegisterType" />. </value>
        public RegisterType Type { get; set; }
    }
}