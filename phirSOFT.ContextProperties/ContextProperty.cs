using System.Linq;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue> : IContextProperty<TValue>
    {
        public ContextProperty(IContextProvider<ContextProperty<TValue>, TValue> defaultContext)
        {
            DefaultContext = defaultContext;
            ContextPool = new ContextProviderTree(defaultContext);
        }

        public ContextProperty(IContextPool<ContextProperty<TValue>, TValue> contextPool)
        {
            ContextPool = contextPool;
        }

        public IContextPool<ContextProperty<TValue>, TValue> ContextPool { get; }

        public IContextProvider<ContextProperty<TValue>, TValue> DefaultContext { get; set; }

        public TValue this[object target, IContextProvider<ContextProperty<TValue>, TValue> context] => ContextPool[context]
            .First(c => c.OverridesValue(target, this)).GetValue(target, this);
    }
}