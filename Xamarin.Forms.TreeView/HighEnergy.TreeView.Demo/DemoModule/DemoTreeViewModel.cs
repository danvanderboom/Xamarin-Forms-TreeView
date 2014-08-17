using System;
using HighEnergy.Collections;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;

namespace HighEnergy.TreeView.Demo
{
    public class DemoTreeViewModel : ObservableObject
    {


        DemoTreeNode _MyTree;
        public DemoTreeNode MyTree
        {
            get { return _MyTree; }
            set { Set("MyTree", ref _MyTree, value); }
        }

        public ICommand AddNodeCommand { protected set; get; }

        public DemoTreeViewModel()
        {
            MyTree = new DemoTreeNode { Title = "Root", Score = 0.5, IsExpanded = true };

            var a = MyTree.ChildNodes.Add(new DemoTreeNode { Title = "Branch A", Score = 0.75, IsExpanded = false });
            var a1 = a.ChildNodes.Add(new DemoTreeNode { Title = "Leaf A1", Score = 0.85, IsExpanded = true });
            var a2 = a.ChildNodes.Add(new DemoTreeNode { Title = "Leaf A2", Score = 0.65, IsExpanded = true });

            var b = MyTree.ChildNodes.Add(new DemoTreeNode { Title = "Branch B", Score = 0.25, IsExpanded = true });
            var b1 = b.ChildNodes.Add(new DemoTreeNode { Title = "Leaf B1", Score = 0.35, IsExpanded = true });
            var b2 = b.ChildNodes.Add(new DemoTreeNode { Title = "Leaf B2", Score = 0.15, IsExpanded = true });

//            var c = MyTree.ChildNodes.Add(new DemoTreeNode { Title = "Branch C", Score = 0.25, IsExpanded = true });
//            var c1 = c.ChildNodes.Add(new DemoTreeNode { Title = "Leaf C1", Score = 0.35, IsExpanded = true });
//            var c2 = c.ChildNodes.Add(new DemoTreeNode { Title = "Branch C2", Score = 0.15, IsExpanded = true });
//            var c2a = c2.ChildNodes.Add(new DemoTreeNode { Title = "Leaf C2a", Score = 0.15, IsExpanded = true });
//
//            var d = MyTree.ChildNodes.Add(new DemoTreeNode { Title = "Branch D", Score = 0.25, IsExpanded = true });
//            var d1 = d.ChildNodes.Add(new DemoTreeNode { Title = "Leaf D1", Score = 0.35, IsExpanded = true });
//            var d2 = d.ChildNodes.Add(new DemoTreeNode { Title = "Leaf D2", Score = 0.15, IsExpanded = true });

            AddNodeCommand = new Command(
                obj => 
                { 
                    b.ChildNodes.Add(new DemoTreeNode { Title = "Another Leaf!", Score = 0.66, IsExpanded = true });
                });

            Timer();
        }

        async void Timer()
        {
            while (true)
            {
                await Task.Delay(3000);

                if (MyTree == null)
                    return;

                var BranchA = MyTree.ChildNodes[0] as DemoTreeNode;
                if (BranchA == null)
                    return;

                BranchA.ChildNodes.Add(new DemoTreeNode { Title = "New Stuff", Score = 0.25, IsExpanded = true });
            }
        }
    }
}