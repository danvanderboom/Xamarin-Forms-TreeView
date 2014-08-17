using System;
using System.Linq;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

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

                if (_ChildNodes != null)
                    _ChildNodes.PropertyChanged -= HandleChildNodeCountChange;

                _ChildNodes = value;

                if (_ChildNodes != null)
                    _ChildNodes.PropertyChanged += HandleChildNodeCountChange;

                OnPropertyChanged("ChildNodes");
            }
        }

        void HandleChildNodeCountChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Count")
                OnPropertyChanged("Count");
        }

        // non-generic iterator for interface-based support
        public IEnumerable<ITreeNode> Children
        {
            get
            {
                foreach (ITreeNode node in ChildNodes)
                    yield return node;

                yield break;
            }
        }

        public IEnumerable<ITreeNode> Descendants
        {
            get
            {
                foreach (ITreeNode node in Children)
                {
                    yield return node;

                    foreach (ITreeNode descendant in node.Descendants)
                        yield return descendant;
                }

                yield break;
            }
        }

        public IEnumerable<ITreeNode> Subtree
        {
            get
            {
                yield return this;

                foreach (ITreeNode node in Descendants)
                    yield return node;

                yield break;
            }
        }

        public IEnumerable<ITreeNode> Ancestors
        {
            get
            {
                if (Parent == null)
                    yield break;

                yield return Parent;

                foreach (ITreeNode node in Parent.Ancestors)
                    yield return node;

                yield break;
            }
        }

        public int Height
        {
            get
            {
                //return ChildNodes.Count == 0 ? 1 : ChildNodes.Max(n => n.Height);

                if (ChildNodes.Count == 0)
                    return 1;

                var maxHeight = 0;

                foreach (ITreeNode node in ChildNodes)
                    maxHeight = Math.Max(maxHeight, node.Height);

                return maxHeight + 1;
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
            get { return (Parent == null ? 0 : Parent.Depth) + 1; }
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