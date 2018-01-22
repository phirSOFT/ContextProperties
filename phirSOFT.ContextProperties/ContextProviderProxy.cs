namespace phirSOFT.ContextProperties
{
    public class ContextProviderProxy<T> : IContextProvider<T, T>
    {
        public IContextProvider<T, T> Provider { get; set; }


        T IContextProvider<T, T>.GetValue(object targetObject, ContextProperty<T> targetProperty)
        {
            return Provider.GetValue(targetObject, targetProperty);
        }

        bool IContextProvider<T, T>.OverridesValue(object targetObject, ContextProperty<T> targetProperty)
        {
            return Provider.OverridesValue(targetObject, targetProperty);
        }
    }
}