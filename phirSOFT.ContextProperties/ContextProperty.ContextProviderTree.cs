using System.Collections;
using System.Collections.Generic;
using System.Linq;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree : ITree<IContextProvider<ContextProperty<TValue>, TValue>>,
            IContextPool<ContextProperty<TValue>, TValue>
        {
            private readonly Dictionary<IContextProvider<ContextProperty<TValue>, TValue>, ContextProviderTreeNode> _nodes = 
                new Dictionary<IContextProvider<ContextProperty<TValue>, TValue>, ContextProviderTreeNode>();

            public ContextProviderTree(IContextProvider<ContextProperty<TValue>, TValue> defaultContext)
            {
                Root = new ContextProviderTreeNode(this, defaultContext);
            }

            public void Add(IContextProvider<ContextProperty<TValue>, TValue> item)
            {
                this.Insert(item);
            }

            public void Clear()
            {
                var childNodes = Root.Children.ToList();
                foreach (var childNode in childNodes)
                {
                    RemoveNode((ContextProviderTreeNode) childNode);
                }
            }

            public bool Contains(IContextProvider<ContextProperty<TValue>, TValue> item)
            {
                return _nodes.ContainsKey(item);
            }

            public void CopyTo(IContextProvider<ContextProperty<TValue>, TValue>[] array, int arrayIndex)
            {
                _nodes.Keys.CopyTo(array, arrayIndex);
            }

            public bool Remove(IContextProvider<ContextProperty<TValue>, TValue> item)
            {
                var node = _nodes[item];
                RemoveNode(node);
                return _nodes.Remove(item);
            }

            public int Count => _nodes.Count;
            public bool IsReadOnly => false;

            IEnumerator<KeyValuePair<IContextProvider<ContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>>>>
                IEnumerable<KeyValuePair<IContextProvider<ContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>>
                >>.GetEnumerator()
            {
                return _nodes.Select(kv =>
                    new KeyValuePair<IContextProvider<ContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>>>(
                        kv.Key, kv.Value)).GetEnumerator();
            }

            public IEnumerator<IContextProvider<ContextProperty<TValue>, TValue>> GetEnumerator()
            {
                return _nodes.Keys.GetEnumerator();
            }

            public bool ContainsKey(IContextProvider<ContextProperty<TValue>, TValue> key)
            {
                return _nodes.ContainsKey(key);
            }

            public bool TryGetValue(IContextProvider<ContextProperty<TValue>, TValue> key,
                out IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>> value)
            {
                var result = _nodes.TryGetValue(key, out var node);
                value = node;
                return result;
            }


            public IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>> this[IContextProvider<ContextProperty<TValue>, TValue> key] =>
                _nodes[key];

            public IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>> Keys => _nodes.Keys;
            public IEnumerable<IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>>> Values => _nodes.Values;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public ITreeNode<IContextProvider<ContextProperty<TValue>, TValue>> Root { get; }

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