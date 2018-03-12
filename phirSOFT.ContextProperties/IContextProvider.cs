namespace phirSOFT.ContextProperties
{
    public interface IContextProvider<out TValue, TPropertyValue> where TValue : TPropertyValue
    {
        TValue GetValue(object targetObject, ContextProperty<TPropertyValue> targetProperty);
        bool OverridesValue(object targetObject, ContextProperty<TPropertyValue> targetProperty);
    }

    public interface IIndexerContextProvider<out TValue, TKey, TPropertyValue> where TValue : TPropertyValue
    {
        IIndexer<TKey, TValue> GetValue(object targetObject, IndexerContextProperty<TKey, TPropertyValue> targetProperty);
        bool OverridesValue(object targetObject, IndexerContextProperty<TKey, TPropertyValue> targetProperty);
    }
}