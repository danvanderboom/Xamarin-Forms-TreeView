using System;

namespace HighEnergy.Collections
{
    public class Tree<T> : TreeNode<T>
        where T : new()
    {
        public Tree() { }

        public Tree(T RootValue)
        {
            Value = RootValue;
        }
    }
}