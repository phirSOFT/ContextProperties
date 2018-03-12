using System.Collections.Generic;

namespace phirSOFT.ContextProperties
{
    public interface IContextPool<TProperty, TValue> : ICollection<IContextProvider<TProperty, TValue>>,
        IReadOnlyDictionary<IContextProvider<TProperty, TValue>, IEnumerable<IContextProvider<TProperty, TValue>>>
    where TProperty : IContextProperty<TValue>
    {

    }
   
}