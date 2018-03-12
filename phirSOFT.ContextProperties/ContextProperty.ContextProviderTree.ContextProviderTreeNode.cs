using System.Collections;
using System.Collections.Generic;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree
        {
            internal class ContextProviderTreeNode : ITreeNode<IContextProvider<IContextProperty<TValue>, TValue>>,
                IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>>
            {
                private readonly List<ContextProviderTreeNode> _children;
                private readonly ContextProviderTree _tree;

                public ContextProviderTreeNode(ContextProviderTree tree, IContextProvider<IContextProperty<TValue>, TValue> value)
                {
                    _tree = tree;
                    _children = new List<ContextProviderTreeNode>();
                    Value = value;
                }

                public ContextProviderTreeNode Parent { get; private set; }

                public IEnumerator<IContextProvider<IContextProperty<TValue>, TValue>> GetEnumerator()
                {
                    IEnumerable<IContextProvider<IContextProperty<TValue>, TValue>> EnumeratorChildren()
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

                public ITreeNode<IContextProvider<IContextProperty<TValue>, TValue>> AddChild(IContextProvider<IContextProperty<TValue>, TValue> node)
                {
                    var child = new ContextProviderTreeNode(_tree, node)
                    {
                        Parent = this
                    };
                    _children.Add(child);
                    _tree._nodes.Add(node, child);

                    return child;
                }

                public void Detach(ITreeNode<IContextProvider<IContextProperty<TValue>, TValue>> child)
                {
                    var node = (ContextProviderTreeNode) child;
                    node.Parent = null;
                    _children.Remove(node);
                }

                public void Attach(ITreeNode<IContextProvider<IContextProperty<TValue>, TValue>> child)
                {
                    var node = (ContextProviderTreeNode) child;
                    node.Parent = this;
                    _children.Add((ContextProviderTreeNode) child);
                }

                public IContextProvider<IContextProperty<TValue>, TValue> Value { get; set; }
                public IEnumerable<ITreeNode<IContextProvider<IContextProperty<TValue>, TValue>>> Children => _children.AsReadOnly();
            }
        }
    }
}