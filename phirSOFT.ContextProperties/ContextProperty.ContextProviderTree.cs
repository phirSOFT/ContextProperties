using System.Collections;
using System.Collections.Generic;
using System.Linq;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree : ITree<IContextProvider<IContextProperty<TValue>, TValue>>,
            IContextPool<IContextProperty<TValue>, TValue>
        {
            private readonly Dictionary<IContextProvider<IContextProperty<TValue>, TValue>, ContextProviderTreeNode> _nodes =
                new Dictionary<IContextProvider<IContextProperty<TValue>, TValue>, ContextProviderTreeNode>();

            public ContextProviderTree(IContextProvider<IContextProperty<TValue>, TValue> defaultContext)
            {
                Root = new ContextProviderTreeNode(this, defaultContext);
            }

            public void Add(IContextProvider<IContextProperty<TValue>, TValue> item)
            {
                this.Insert(item);
            }

            public void Clear()
            {
                var childNodes = Root.Children.ToList();
                foreach (var childNode in childNodes)
                {
                    RemoveNode((ContextProviderTreeNode)childNode);
                }
            }

            public bool Contains(IContextProvider<IContextProperty<TValue>, TValue> item)
            {
                return _nodes.ContainsKey(item);
            }

            public void CopyTo(IContextProvider<IContextProperty<TValue>, TValue>[] array, int arrayIndex)
            {
                _nodes.Keys.CopyTo(array, arrayIndex);
            }

            public bool Remove(IContextProvider<IContextProperty<TValue>, TValue> item)
            {
                var node = _nodes[item];
                RemoveNode(node);
                return _nodes.Remove(item);
            }

            public int Count => _nodes.Count;
            public bool IsReadOnly => false;

            IEnumerator<KeyValuePair<IContextProvider<IContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>>>
                IEnumerable<KeyValuePair<IContextProvider<IContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>
                >>.GetEnumerator()
            {
                return _nodes.Select(kv =>
                    new KeyValuePair<IContextProvider<IContextProperty<TValue>, TValue>, IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>>(
                        kv.Key, kv.Value)).GetEnumerator();
            }

            public IEnumerator<IContextProvider<IContextProperty<TValue>, TValue>> GetEnumerator()
            {
                return _nodes.Keys.GetEnumerator();
            }

            public bool ContainsKey(IContextProvider<IContextProperty<TValue>, TValue> key)
            {
                return _nodes.ContainsKey(key);
            }

            public bool TryGetValue(IContextProvider<IContextProperty<TValue>, TValue> key,
                out IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> value)
            {
                var result = _nodes.TryGetValue(key, out var node);
                value = node;
                return result;
            }


            public IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> this[IContextProvider<IContextProperty<TValue>, TValue> key]
            {
                get
                {
                    if (_nodes.ContainsKey(key))
                        return _nodes[key];

                    IContextProvider<IContextProperty<TValue>, TValue> lastFound = null;
                    var comparer = new ContextProviderComparer();

                    foreach (var nodesKey in _nodes.Keys)
                    {
                        if (comparer.TryCompare(nodesKey, key, out var result) && result < 0 &&
                            comparer.Compare(nodesKey, lastFound) > 0)
                            lastFound = nodesKey;
                    }

                    return lastFound == null ? new[] { key } : new[] { key }.Concat(_nodes[key]);
                }
            }

            public IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> Keys => _nodes.Keys;
            public IEnumerable<IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>> Values => _nodes.Values;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public ITreeNode<IContextProvider<IContextProperty<TValue>, TValue>> Root { get; }

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