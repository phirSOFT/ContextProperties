using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.ContextProperties
{
    /// <summary>
    /// Provides a way to define a context property in an interface.
    /// </summary>
    /// <typeparam name="TValue">The type of the property in the interface.</typeparam>
    public interface IMemberContextProperty<TValue>
    {
        TValue this[IContextProvider<IContextProperty<TValue>, TValue> context] { get; }
    }
}
