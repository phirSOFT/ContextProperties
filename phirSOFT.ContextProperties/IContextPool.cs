using System.Collections.Generic;

namespace phirSOFT.ContextProperties
{
    public interface IContextPool<TValue> : ICollection<IContextProvider<TValue, TValue>>,
        IReadOnlyDictionary<IContextProvider<TValue, TValue>, IEnumerable<IContextProvider<TValue, TValue>>>
    {
    }
}