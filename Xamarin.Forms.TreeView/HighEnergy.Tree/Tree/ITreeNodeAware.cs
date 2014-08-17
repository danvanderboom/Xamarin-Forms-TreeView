using System;

namespace HighEnergy.Collections
{
    public interface ITreeNodeAware<T>
        where T : new()
    {
        TreeNode<T> Node { get; set; }
    }
}