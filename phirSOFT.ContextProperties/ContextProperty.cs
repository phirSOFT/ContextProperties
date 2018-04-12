using System.Linq;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue> : IContextProperty<TValue>
    {
        public ContextProperty(IContextProvider<IContextProperty<TValue>, TValue> defaultContext)
        {
            DefaultContext = defaultContext;
            ContextPool = new ContextProviderTree(defaultContext);
        }

        public ContextProperty(IContextPool<IContextProperty<TValue>, TValue> contextPool)
        {
            ContextPool = contextPool;
        }

        public IContextPool<IContextProperty<TValue>, TValue> ContextPool { get; }

        public IContextProvider<IContextProperty<TValue>, TValue> DefaultContext { get; set; }

        public TValue this[object target, IContextProvider<IContextProperty<TValue>, TValue> context] => ContextPool[context]
            .First(c => c.OverridesValue(target, this)).GetValue(target, this);
    }
}