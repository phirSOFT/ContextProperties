using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phirSOFT.ContextProperties
{
    public class IndependentContextPool<TValue> : IContextPool<IContextProperty<TValue>, TValue>
    {
        private readonly List<IContextProvider<IContextProperty<TValue>, TValue>> registredProviders = new List<IContextProvider<IContextProperty<TValue>, TValue>>();
        
        IEnumerator<KeyValuePair<IContextProvider<IContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>>> IEnumerable<KeyValuePair<IContextProvider<IContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>>>.GetEnumerator()
        {
            return Enumerable
                .Empty<KeyValuePair<IContextProvider<IContextProperty<TValue>, TValue>,
                    IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>>>().GetEnumerator();
        }

        public IEnumerator<IContextProvider<IContextProperty<TValue>, TValue>> GetEnumerator()
        {
            return registredProviders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) registredProviders).GetEnumerator();
        }

        public void Add(IContextProvider<IContextProperty<TValue>, TValue> item)
        {
            registredProviders.Add(item);
        }

        public void Clear()
        {
            registredProviders.Clear();
        }

        public bool Contains(IContextProvider<IContextProperty<TValue>, TValue> item)
        {
            return registredProviders.Contains(item);
        }

        public void CopyTo(IContextProvider<IContextProperty<TValue>, TValue>[] array, int arrayIndex)
        {
            registredProviders.CopyTo(array, arrayIndex);
        }

        public bool Remove(IContextProvider<IContextProperty<TValue>, TValue> item)
        {
            return registredProviders.Remove(item);
        }

        public int Count
        {
            get { return registredProviders.Count; }
        }

        public bool IsReadOnly => false;

        public bool ContainsKey(IContextProvider<IContextProperty<TValue>, TValue> key)
        {
            return false;
        }

        public bool TryGetValue(IContextProvider<IContextProperty<TValue>, TValue> key, out IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> value)
        {
            value = null;
            return false;
        }

        public IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> this[IContextProvider<IContextProperty<TValue>, TValue> key] => new[] {key};

        public IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> Keys { get; } = null;
        public IEnumerable<IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>> Values { get; } = null;
    }
}
