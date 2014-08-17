using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;

namespace HighEnergy.Collections
{
    public class TreeNodeList<T> : List<ITreeNode<T>>, ITreeNodeList<T>, INotifyPropertyChanged
    {
        public ITreeNode<T> Parent { get; set; }

        public TreeNodeList(ITreeNode<T> Parent)
        {
            // call property setters to trigger setup and event notifications
            this.Parent = Parent;
        }

        // once you start working with collection manipulation functions like this which return the object they add... you'll hate having it any other way
        public new ITreeNode<T> Add(ITreeNode<T> Node)
        {
            base.Add(Node);

            // call property setters to trigger setup and event notifications
            Node.Parent = Parent;

            OnPropertyChanged("Count");

            return Node;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public override string ToString()
        {
            return "Count=" + Count;
        }
    }
}