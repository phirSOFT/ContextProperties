using System.Net.Http.Headers;

namespace phirSOFT.ContextProperties
{
    public interface IContextProvider<in TProperty, out TValue> where TProperty : IContextProperty<TValue>
    {
        TValue GetValue(object targetObject, TProperty targetProperty);
        bool OverridesValue(object targetObject, TProperty targetProperty);
    }
}