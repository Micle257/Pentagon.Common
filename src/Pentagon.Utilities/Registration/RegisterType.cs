namespace Pentagon.Registration {
    /// <summary> Specifies the type of the registration. </summary>
    public enum RegisterType
    {
        /// <summary> The unspecified, default value. </summary>
        Unspecified,

        /// <summary> The transient registration, the type is created per request. </summary>
        Transient,

        /// <summary> The singleton registration, the type is created just once. </summary>
        Singleton
    }
}