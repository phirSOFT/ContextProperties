namespace phirSOFT.ContextProperties
{
    public class ContextProviderProxy<T> : IContextProvider<IContextProperty<T>, T>
    {
        public IContextProvider<IContextProperty<T>, T> Provider { get; set; }


        T IContextProvider<IContextProperty<T>, T>.GetValue(object targetObject, IContextProperty<T> targetProperty)
        {
            return Provider.GetValue(targetObject, targetProperty);
        }

        bool IContextProvider<IContextProperty<T>, T>.OverridesValue(object targetObject, IContextProperty<T> targetProperty)
        {
            return Provider.OverridesValue(targetObject, targetProperty);
        }
    }
}