using System.Linq;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        public ContextProperty(IContextProvider<TValue, TValue> defaultContext)
        {
            DefaultContext = defaultContext;
            ContextPool = new ContextProviderTree(defaultContext);
        }

        public ContextProperty(IContextPool<TValue> contextPool)
        {
            ContextPool = contextPool;
        }

        public IContextPool<TValue> ContextPool { get; }

        public IContextProvider<TValue, TValue> DefaultContext { get; set; }

        public TValue this[object target, IContextProvider<TValue, TValue> context] => ContextPool[context]
            .First(c => c.OverridesValue(target, this)).GetValue(target, this);
    }
}