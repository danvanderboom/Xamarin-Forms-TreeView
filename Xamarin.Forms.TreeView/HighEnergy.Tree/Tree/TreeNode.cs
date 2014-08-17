using System;
using System.ComponentModel;
using System.Collections;

namespace HighEnergy.Collections
{
    public class TreeNode<T> : ObservableObject, ITreeNode<T>, IDisposable
        where T : new()
    {
        public TreeNode()
        {
            // call property setters to trigger setup and event notifications
            Parent = null;
            ChildNodes = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value)
        {
            // call property setters to trigger setup and event notifications
            this.Value = Value;
            Parent = null;
            ChildNodes = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value, TreeNode<T> Parent)
        {
            // call property setters to trigger setup and event notifications
            this.Value = Value;
            this.Parent = Parent;
            ChildNodes = new TreeNodeList<T>(this);
        }

        public ITreeNode ParentNode 
        { 
            get { return _Parent; } 
        }

        private ITreeNode<T> _Parent;
        public ITreeNode<T> Parent
        {
            get { return _Parent; }
            set
            {
                if (value == _Parent)
                    return;

                OnParentChanged(_Parent, value);
            }
        }

        protected virtual void OnParentChanged(ITreeNode<T> oldValue, ITreeNode<T> newValue)
        {
            // remember current depth
            var oldDepth = Depth;

            // remove existing parent
            if (_Parent != null)
                _Parent.ChildNodes.Remove(this);

            // change all the old parent's children to be the new parent's children
            if (newValue != null && !newValue.ChildNodes.Contains(this))
                newValue.ChildNodes.Add(this);

            _Parent = newValue;
            OnPropertyChanged("Parent");

            if (Depth != oldDepth)
                OnPropertyChanged("Depth");
        }

        // TODO: add property and event notifications that are missing from this set: DescendentsChanged, AnscestorsChanged, ChildrenChanged, ParentChanged

        public ITreeNode<T> Root
        {
            get { return (Parent == null) ? this : Parent.Root; }
        }

        private ITreeNodeList<T> _ChildNodes;
        public ITreeNodeList<T> ChildNodes
        {
            get { return _ChildNodes; }
            private set
            {
                if (value == _ChildNodes)
                    return;

                // TODO: call Dispose on _ChildNodes?
                // tell all the children they have no more parent :-(
//                if (value != null)
//                    foreach (ITreeNode<T> node in _ChildNodes)
//                        node.Parent = null;

                _ChildNodes = value;
                OnPropertyChanged("ChildNodes");
            }
        }

        // non-generic iterator for interface-based support
        public IEnumerable Children
        {
            get
            {
                foreach (var child in ChildNodes)
                    yield return child;

                yield break;
            }
        }


        private T _Value;
        public T Value
        {
            get { return _Value; }
            set
            {
                if (value == null && _Value == null)
                    return;

                if (value != null && _Value != null && value.Equals(_Value))
                    return;

                _Value = value;
                OnPropertyChanged("Value");

                // set Node if it's ITreeNodeAware
                if (_Value != null && _Value is ITreeNodeAware<T>)
                    (_Value as ITreeNodeAware<T>).Node = this;
            }
        }

        public int Depth
        {
            get { return (Parent == null ? -1 : Parent.Depth) + 1; }
        }

        private TreeTraversalType _DisposeTraversal = TreeTraversalType.BottomUp;
        public TreeTraversalType DisposeTraversal
        {
            get { return _DisposeTraversal; }
            set { _DisposeTraversal = value; }
        }

        private bool _IsDisposed;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        public void Dispose()
        {
            CheckDisposed();
            OnDisposing();

            // clean up contained objects (in Value property)
            if (Value is IDisposable)
            {
                if (DisposeTraversal == TreeTraversalType.BottomUp)
                    foreach (TreeNode<T> node in ChildNodes)
                        node.Dispose();

                (Value as IDisposable).Dispose();

                if (DisposeTraversal == TreeTraversalType.TopDown)
                    foreach (TreeNode<T> node in ChildNodes)
                        node.Dispose();
            }

            _IsDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (Disposing != null)
                Disposing(this, EventArgs.Empty);
        }

        public void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public override string ToString()
        {
            string Description = string.Empty;
            if (Value != null)
                Description = "[" + Value + "] ";

            return Description + "Depth=" + Depth + ", Children=" + ChildNodes.Count;
        }
    }
}