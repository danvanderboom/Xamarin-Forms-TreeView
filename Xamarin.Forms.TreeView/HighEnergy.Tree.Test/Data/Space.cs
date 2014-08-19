using System;
using HighEnergy.Collections;
using System.Diagnostics;

namespace HighEnergy.Tree.Test
{
    public class Space : TreeNode<Space>
    {
        public string Name { get; set; }
        public double SquareFeet { get; set; }

        public Space() : base() { }

        public Space(Space parent) : base(parent) { }

        //public Space(string name, double squareFeet) : base(new Space(name, squareFeet)) { }

        public override void OnAncestorChanged(NodeChangeType changeType, ITreeNode node)
        {
            base.OnAncestorChanged(changeType, node);
            Debug.WriteLine("Space.OnAncestorChanged: " + changeType + " " + ((Space)node).Name);
        }

        public override void OnDescendantChanged(NodeChangeType changeType, ITreeNode node)
        {
            base.OnDescendantChanged(changeType, node);
            Debug.WriteLine("Space.OnDescendantChanged: " + changeType + " " + ((Space)node).Name);
        }

        protected override void OnParentChanged(ITreeNode<Space> oldValue, ITreeNode<Space> newValue)
        {
            base.OnParentChanged(oldValue, newValue);

            var oldParentName = oldValue != null ? ((Space)oldValue).Name : "null";
            var newParentName = newValue != null ? ((Space)newValue).Name : "null";
            Debug.WriteLine("Space.OnParentChanged: from " + oldParentName + " to " + newParentName);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            Debug.WriteLine("Space.OnPropertyChanged: " + propertyName);
        }
    }
}