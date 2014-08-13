using System;
using System.ComponentModel;

namespace HighEnergy.Collections
{
    public class TreeNode<T> : IDisposable, INotifyPropertyChanged, IObservable<T>
    {
        public TreeNode()
        {
            Parent = null;
            Children = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value)
        {
            this.Value = Value;
            Parent = null;
            Children = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value, TreeNode<T> Parent)
        {
            this.Value = Value;
            this.Parent = Parent;
            Children = new TreeNodeList<T>(this);
        }

        private TreeNode<T> _Parent;
        public TreeNode<T> Parent
        {
            get { return _Parent; }
            set
            {
                if (value == _Parent)
                    return;

                if (_Parent != null)
                    _Parent.Children.Remove(this);

                if (value != null && !value.Children.Contains(this))
                    value.Children.Add(this);

                _Parent = value;
            }
        }

        public TreeNode<T> Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;

                TreeNode<T> node = this;
                while (node.Parent != null)
                    node = node.Parent;

                return node;
            }
        }

        private TreeNodeList<T> _Children;
        public TreeNodeList<T> Children
        {
            get { return _Children; }
            private set { _Children = value; }
        }

        private T _Value;
        public T Value
        {
            get { return _Value; }
            set
            {
                _Value = value;

                if (_Value != null && _Value is ITreeNodeAware<T>)
                    (_Value as ITreeNodeAware<T>).Node = this;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            // TODO: implement
            throw new NotImplementedException();
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
                    foreach (TreeNode<T> node in Children)
                        node.Dispose();

                (Value as IDisposable).Dispose();

                if (DisposeTraversal == TreeTraversalType.TopDown)
                    foreach (TreeNode<T> node in Children)
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
                Description = "[" + Value.ToString() + "] ";

            return Description + "Depth=" + Depth.ToString() + ", Children=" + Children.Count.ToString();
        }
    }
}