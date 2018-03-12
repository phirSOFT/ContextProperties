using System.Collections;
using System.Collections.Generic;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree
        {
            internal class ContextProviderTreeNode : ITreeNode<IContextProvider<ContextProperty<TValue>, TValue>>,
                IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>>
            {
                private readonly List<ContextProviderTreeNode> _children;
                private readonly ContextProviderTree _tree;

                public ContextProviderTreeNode(ContextProviderTree tree, IContextProvider<ContextProperty<TValue>, TValue> value)
                {
                    _tree = tree;
                    _children = new List<ContextProviderTreeNode>();
                    Value = value;
                }

                public ContextProviderTreeNode Parent { get; private set; }

                public IEnumerator<IContextProvider<ContextProperty<TValue>, TValue>> GetEnumerator()
                {
                    IEnumerable<IContextProvider<ContextProperty<TValue>, TValue>> EnumeratorChildren()
                    {
                        var currentNode = this;
                        while (currentNode != null)
                        {
                            yield return currentNode.Value;
                            currentNode = currentNode.Parent;
                        }
                    }

                    return EnumeratorChildren().GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                public ITreeNode<IContextProvider<ContextProperty<TValue>, TValue>> AddChild(IContextProvider<ContextProperty<TValue>, TValue> node)
                {
                    var child = new ContextProviderTreeNode(_tree, node)
                    {
                        Parent = this
                    };
                    _children.Add(child);
                    _tree._nodes.Add(node, child);

                    return child;
                }

                public void Detach(ITreeNode<IContextProvider<ContextProperty<TValue>, TValue>> child)
                {
                    var node = (ContextProviderTreeNode) child;
                    node.Parent = null;
                    _children.Remove(node);
                }

                public void Attach(ITreeNode<IContextProvider<ContextProperty<TValue>, TValue>> child)
                {
                    var node = (ContextProviderTreeNode) child;
                    node.Parent = this;
                    _children.Add((ContextProviderTreeNode) child);
                }

                public IContextProvider<ContextProperty<TValue>, TValue> Value { get; set; }
                public IEnumerable<ITreeNode<IContextProvider<ContextProperty<TValue>, TValue>>> Children => _children.AsReadOnly();
            }
        }
    }
}