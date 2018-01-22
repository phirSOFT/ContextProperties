using System.Collections;
using System.Collections.Generic;
using System.Linq;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree : ITree<IContextProvider<TValue, TValue>>, IContextPool<TValue>
        {
            private readonly Dictionary<IContextProvider<TValue, TValue>, ContextProviderTreeNode> _nodes =
                new Dictionary<IContextProvider<TValue, TValue>, ContextProviderTreeNode>();

            public ContextProviderTree(IContextProvider<TValue, TValue> defaultContext)
            {
                Root = new ContextProviderTreeNode(this, defaultContext);
            }

            public void Add(IContextProvider<TValue, TValue> item)
            {
                this.Insert(item);
            }

            public void Clear()
            {
                var childNodes = Root.Children.ToList();
                foreach (var childNode in childNodes) RemoveNode((ContextProviderTreeNode) childNode);
            }

            public bool Contains(IContextProvider<TValue, TValue> item)
            {
                return _nodes.ContainsKey(item);
            }

            public void CopyTo(IContextProvider<TValue, TValue>[] array, int arrayIndex)
            {
                _nodes.Keys.CopyTo(array, arrayIndex);
            }

            public bool Remove(IContextProvider<TValue, TValue> item)
            {
                var node = _nodes[item];
                RemoveNode(node);
                return _nodes.Remove(item);
            }

            public int Count => _nodes.Count;
            public bool IsReadOnly => false;

            IEnumerator<KeyValuePair<IContextProvider<TValue, TValue>, IEnumerable<IContextProvider<TValue, TValue>>>>
                IEnumerable<KeyValuePair<IContextProvider<TValue, TValue>, IEnumerable<IContextProvider<TValue, TValue>>
                >>.GetEnumerator()
            {
                return _nodes.Select(kv =>
                    new KeyValuePair<IContextProvider<TValue, TValue>, IEnumerable<IContextProvider<TValue, TValue>>>(
                        kv.Key, kv.Value)).GetEnumerator();
            }

            public IEnumerator<IContextProvider<TValue, TValue>> GetEnumerator()
            {
                return _nodes.Keys.GetEnumerator();
            }

            public bool ContainsKey(IContextProvider<TValue, TValue> key)
            {
                return _nodes.ContainsKey(key);
            }

            public bool TryGetValue(IContextProvider<TValue, TValue> key,
                out IEnumerable<IContextProvider<TValue, TValue>> value)
            {
                var result = _nodes.TryGetValue(key, out var node);
                value = node;
                return result;
            }


            public IEnumerable<IContextProvider<TValue, TValue>> this[IContextProvider<TValue, TValue> key] =>
                _nodes[key];

            public IEnumerable<IContextProvider<TValue, TValue>> Keys => _nodes.Keys;
            public IEnumerable<IEnumerable<IContextProvider<TValue, TValue>>> Values => _nodes.Values;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public ITreeNode<IContextProvider<TValue, TValue>> Root { get; }

            private void RemoveNode(ContextProviderTreeNode node)
            {
                var children = node.Children.ToList();

                foreach (var child in children)
                {
                    node.Detach(child);
                    node.Parent?.Attach(child);
                }
            }
        }
    }
}