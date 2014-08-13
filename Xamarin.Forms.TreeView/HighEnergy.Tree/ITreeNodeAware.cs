using System;

namespace HighEnergy.Collections
{
    public interface ITreeNodeAware<T>
    {
        TreeNode<T> Node { get; set; }
    }
}