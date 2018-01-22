using System.Collections.Generic;
using System.Linq;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        public IContextPool<TValue> ContextPool { get; }

        public IContextProvider<TValue, TValue> DefaultContext { get; set; }

        TValue this[object target, IContextProvider<TValue, TValue> context] => ContextPool[context].First(c => c.OverridesValue(target,this)).GetValue(target, this);
    }
}