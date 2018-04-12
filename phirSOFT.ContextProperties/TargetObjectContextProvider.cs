using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.ContextProperties
{
    /// <summary>
    /// Implements a <see cref="IContextProvider{TProperty,TValue}"/> that redirects the calls to the target object if possible.
    /// </summary>
    /// <typeparam name="TProperty">THe type of the property.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class TargetObjectContextProvider<TProperty, TValue> : IContextProvider<TProperty, TValue> where TProperty : IContextProperty<TValue>
    {
        /// <inheritdoc cref="IContextProvider{TProperty,TValue}.GetValue"/>
        public TValue GetValue(object targetObject, TProperty targetProperty)
        {
            return ((IContextProvider<TProperty, TValue>) targetObject).GetValue(targetObject, targetProperty);
        }

        /// <inheritdoc cref="IContextProvider{TProperty,TValue}.OverridesValue"/>
        public bool OverridesValue(object targetObject, TProperty targetProperty)
        {
            return targetObject is IContextProvider<TProperty, TValue> provider &&
                   provider.OverridesValue(targetObject, targetProperty);
        }
    }
}
