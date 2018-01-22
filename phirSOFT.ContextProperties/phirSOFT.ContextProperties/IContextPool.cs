using System.Collections.Generic;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public interface IContextPool<TValue>
    {
        ITreeNode<IContextProvider<TValue, TValue>> this[IContextProvider<TValue, TValue> key] { get; }

        int Count { get; }
        bool IsReadOnly { get; }
        IEnumerable<IContextProvider<TValue, TValue>> Keys { get; }
        ITreeNode<IContextProvider<TValue, TValue>> Root { get; }
        IEnumerable<ITreeNode<IContextProvider<TValue, TValue>>> Values { get; }

        void Add(IContextProvider<TValue, TValue> item);
        void Clear();
        bool Contains(IContextProvider<TValue, TValue> item);
        bool ContainsKey(IContextProvider<TValue, TValue> key);
        void CopyTo(IContextProvider<TValue, TValue>[] array, int arrayIndex);
        IEnumerator<IContextProvider<TValue, TValue>> GetEnumerator();
        bool Remove(IContextProvider<TValue, TValue> item);
        bool TryGetValue(IContextProvider<TValue, TValue> key, out ITreeNode<IContextProvider<TValue, TValue>> value);
    }
}