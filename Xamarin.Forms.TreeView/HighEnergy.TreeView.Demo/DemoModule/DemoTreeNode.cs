using System;
using System.Windows.Input;
using Xamarin.Forms;
using HighEnergy.Collections;

namespace HighEnergy.TreeView.Demo
{
    public class DemoTreeNode : TreeNode<DemoTreeNode>
    {
        public ICommand ToggleIsExpandedCommand { protected set; get; }

        // normal view model properties provide the content as well as the visual state

        bool _IsExpanded;
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set { Set("IsExpanded", ref _IsExpanded, value); }
        }

        // we're 100% in control of the indentation level, if any, that we use in rendering our tree nodes
        public double IndentWidth { get { return (double)(Depth * 30); } }

        string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { Set("Title", ref _Title, value); }
        }

        double _Score = .335;
        public double Score
        {
            get { return _Score; }
            set { Set("Score", ref _Score, value); }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Depth")
                base.OnPropertyChanged("IndentWidth");
        }

        public DemoTreeNode()
        {
            ToggleIsExpandedCommand = new Command(
                obj => 
                { 
                    IsExpanded = !IsExpanded; 
                });
        }

        public override string ToString()
        {
            return string.Format("[DemoTreeNode: Title={3}, Score={4}, IsExpanded={1}, IndentWidth={2}]", ToggleIsExpandedCommand, IsExpanded, IndentWidth, Title, Score);
        }
    }
}