﻿namespace phirSOFT.ContextProperties
{
    public interface IContextProvider<out TValue, TPropertyValue> where TValue : TPropertyValue
    {
        TValue GetValue(object targetObject, ContextProperty<TPropertyValue> targetProperty);
        bool OverridesValue(object targetObject, ContextProperty<TPropertyValue> targetProperty);
    }
}