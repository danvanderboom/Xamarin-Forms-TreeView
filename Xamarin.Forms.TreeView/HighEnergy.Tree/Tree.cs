using System;

namespace HighEnergy.Collections
{
    public class Tree<T> : TreeNode<T>
    {
        public Tree() { }

        public Tree(T RootValue)
        {
            Value = RootValue;
        }
    }
}