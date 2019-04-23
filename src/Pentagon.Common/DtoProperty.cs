// -----------------------------------------------------------------------
//  <copyright file="DtoProperty.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    /// <summary> Represents a data transfer object property. Used to annotate/wrap as undefined for more verbose dynamic/static transfer. </summary>
    /// <typeparam name="TProperty"> The type of the property. </typeparam>
    public class DtoProperty<TProperty>
    {
        /// <summary> Initializes a new instance of the <see cref="DtoProperty{TProperty}" /> class as undefined. </summary>
        public DtoProperty()
        {
            IsDefined = false;
            Value = default;
        }

        /// <summary> Initializes a new instance of the <see cref="DtoProperty{TProperty}" /> class with inner value. </summary>
        /// <param name="value"> The value. </param>
        public DtoProperty(TProperty value)
        {
            IsDefined = true;
            Value = value;
        }

        /// <summary> Gets the inner value. Value is invalid/undeterministic if <see cref="IsDefined" /> is <c> false </c>. </summary>
        /// <value> The value. </value>
        public TProperty Value { get; }

        /// <summary> Gets a value indicating whether this instance is defined. </summary>
        /// <value> <c> true </c> if this instance is defined; otherwise, <c> false </c>. </value>
        public bool IsDefined { get; private set; }

        #region Operators

        public static implicit operator TProperty(DtoProperty<TProperty> wrap) => wrap.Value;

        public static implicit operator DtoProperty<TProperty>(TProperty wrap) => new DtoProperty<TProperty>(wrap);

        #endregion

        /// <summary> Marks this property as undefined. </summary>
        public void AsUndefined()
        {
            IsDefined = false;
        }

        /// <inheritdoc />
        public override string ToString() => Value?.ToString() ?? $"DTO property undefined, type: {typeof(TProperty).Name}";
    }
}