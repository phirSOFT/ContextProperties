namespace phirSOFT.ContextProperties
{
    public interface IContextProperty<TValue>
    {
        TValue this[object instance, IContextProvider<IContextProperty<TValue>, TValue> context] { get; }

        IContextProvider<IContextProperty<TValue>, TValue> DefaultContext { get; }
    }
}