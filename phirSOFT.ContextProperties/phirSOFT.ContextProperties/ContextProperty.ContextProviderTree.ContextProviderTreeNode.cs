using System.Collections.Generic;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree
        {
            internal class ContextProviderTreeNode : ITreeNode<IContextProvider<TValue, TValue>>
            {
                private readonly List<ContextProviderTreeNode> children;
                private readonly ContextProviderTree tree;
                private ContextProviderTreeNode parent;

                public ContextProviderTreeNode(ContextProviderTree tree, IContextProvider<TValue,TValue> value)
                {
                    this.tree = tree;
                    children = new List<ContextProviderTreeNode>();
                    Value = value;
                }

                public ITreeNode<IContextProvider<TValue, TValue>> AddChild(IContextProvider<TValue, TValue> node)
                {
                    var child = new ContextProviderTreeNode(tree, node)
                    {
                        parent = this
                    };
                    children.Add(child);
                    tree.nodes.Add(node, child);

                    return child;
                }

                public void Detach(ITreeNode<IContextProvider<TValue, TValue>> child)
                {
                    var node = (ContextProviderTreeNode) child;
                    node.parent = null;
                    children.Remove((ContextProviderTreeNode) node);
                }

                public void Attach(ITreeNode<IContextProvider<TValue, TValue>> child)
                {
                    var node = (ContextProviderTreeNode)child;
                    node.parent = this;
                    children.Add((ContextProviderTreeNode)child);
                }

                public IContextProvider<TValue, TValue> Value { get; set; }
                public IEnumerable<ITreeNode<IContextProvider<TValue, TValue>>> Children => children.AsReadOnly();
                public ContextProviderTreeNode Parent => parent;
            }
        }
    }
  
}
