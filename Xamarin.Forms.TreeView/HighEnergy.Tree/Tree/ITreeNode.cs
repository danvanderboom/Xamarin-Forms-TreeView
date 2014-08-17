using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

namespace HighEnergy.Collections
{
    public interface ITreeNode<T> : ITreeNode
    {
        ITreeNode<T> Root { get; }
        ITreeNode<T> Parent { get; set; }

        T Value { get; set; }

        ITreeNodeList<T> ChildNodes { get; }
    }

    public interface ITreeNode : INotifyPropertyChanged
    {
        // Parent, Parent.Parent, ...
        IEnumerable<ITreeNode> Ancestors { get; }

        // direct Parent
        ITreeNode ParentNode { get; }

        // direct descendants
        IEnumerable<ITreeNode> Children { get; }

        // Children, Children[i].Children, ...
        IEnumerable<ITreeNode> Descendants { get; }

        // distance from Root
        int Depth { get; }

        int Height { get; }
    }
}