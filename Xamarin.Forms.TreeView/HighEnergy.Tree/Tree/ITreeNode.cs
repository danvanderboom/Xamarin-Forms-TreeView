using System.Collections;
using System.ComponentModel;

namespace HighEnergy.Collections
{
    public interface ITreeNode<T> : ITreeNode
    {
        ITreeNode<T> Root { get; }
        new ITreeNode<T> Parent { get; set; }

        T Value { get; set; }
        int Depth { get; }

        ITreeNodeList<T> ChildNodes { get; }
    }

    public interface ITreeNode : INotifyPropertyChanged
    {
        ITreeNode ParentNode { get; }
        IEnumerable Children { get; }
    }
}