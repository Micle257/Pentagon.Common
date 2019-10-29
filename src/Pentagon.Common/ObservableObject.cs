// -----------------------------------------------------------------------
//  <copyright file="ObservableObject.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    /// <summary> Base class that provides properties ability to notify their change in value. </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary> Occurs when a property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Fires the <see cref="PropertyChanged" /> event. </summary>
        /// <param name="propertyName"> The name of the property. </param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}