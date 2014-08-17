using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HighEnergy.Collections
{
    public interface ITreeNodeList<T> : IList<ITreeNode<T>>, INotifyPropertyChanged
    {
        // usage: var myNewNode = node.Children.Add(new myNodeType("..."));
        new ITreeNode<T> Add(ITreeNode<T> Node);
    }
}