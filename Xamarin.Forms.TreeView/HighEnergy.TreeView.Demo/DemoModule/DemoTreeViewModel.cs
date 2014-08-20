using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using HighEnergy.Collections;

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

        static Random random = new Random(DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Day);

        public DemoTreeViewModel()
        {
            MyTree = new DemoTreeNode { Title = "Root", Score = 0.5, IsExpanded = true };

            var a = MyTree.Children.Add(new DemoTreeNode { Title = "Branch A", Score = 0.75, IsExpanded = true });
            a.Children.Add(new DemoTreeNode { Title = "Leaf A1", Score = 0.85, IsExpanded = true });
            a.Children.Add(new DemoTreeNode { Title = "Leaf A2", Score = 0.65, IsExpanded = true });

            var b = MyTree.Children.Add(new DemoTreeNode { Title = "Branch B", Score = 0.25, IsExpanded = true });
            b.Children.Add(new DemoTreeNode { Title = "Leaf B1", Score = 0.35, IsExpanded = true });
            b.Children.Add(new DemoTreeNode { Title = "Leaf B2", Score = 0.15, IsExpanded = true });

            // TODO: start optimizing for performance in supporting larger trees

//            var c = MyTree.ChildNodes.Add(new DemoTreeNode { Title = "Branch C", Score = 0.25, IsExpanded = true });
//            var c1 = c.ChildNodes.Add(new DemoTreeNode { Title = "Leaf C1", Score = 0.35, IsExpanded = true });
//            var c2 = c.ChildNodes.Add(new DemoTreeNode { Title = "Branch C2", Score = 0.15, IsExpanded = true });
//            c2.ChildNodes.Add(new DemoTreeNode { Title = "Leaf C2a", Score = 0.15, IsExpanded = true });
//
//            var d = MyTree.ChildNodes.Add(new DemoTreeNode { Title = "Branch D", Score = 0.25, IsExpanded = true });
//            d.ChildNodes.Add(new DemoTreeNode { Title = "Leaf D1", Score = 0.35, IsExpanded = true });
//            d.ChildNodes.Add(new DemoTreeNode { Title = "Leaf D2", Score = 0.15, IsExpanded = true });
        }

        public async void InsertRandomNodes()
        {
            // insert 6 new nodes randomly into the tree, 1 every 5 seconds
            for (int i = 0; i < 6; i++)
            {
                await Task.Delay(5000);

                if (MyTree == null)
                    return;

                // pick a random node
                var randomIndex = random.Next(0, MyTree.Subtree.Count() - 1);
                var node = MyTree.Subtree.Skip(randomIndex).First();

                (node as DemoTreeNode).Children.Add(
                    new DemoTreeNode 
                    { 
                        Title = GetRandomTitle(),
                        Score = Math.Round(random.NextDouble(), 3),
                        IsExpanded = true
                    });
            }
        }

        string GetRandomTitle()
        {
            var title = GetRandomAdjective() + " " + GetRandomWord() + " " + GetRandomWord() + " ";

            if (random.NextDouble() < 0.30)
                title += random.Next(3, 99).ToString() + "!";

            return title;
        }

        string GetRandomAdjective()
        {
            var adjs = new string[] { "happy", "fluffy", "short", "tall", "hard", "soft", "flat", "thick", "thin", "round", "square", "rambunctious", "titillating", "merry", "fried", "limber", "bellicose", 
                "tired", "pretentious", "moody", "comical", "severe", "flabberghasted", "opinionated", "naive", "hungry", "bedazzled", "mendacious", "patient", "radical", "flummoxed", "snide", "petty" };
            var i = random.Next(0, adjs.Count() - 1);
            return adjs.Skip(i).First();
        }

        string GetRandomWord()
        {
            var words = new string[] { "bird", "hand", "dog", "fruit", "frog", "juice", "egg", "apple", "bottle", "cork", "wine", "hat", "glove", "moon", "tree", "hair", "house", "river", "flavor",
                "clown", "door", "phone", "clock", "bread", "candy", "shoe", "fish", "drink", "noodle", "ladder", "smile", "brandy", "corn", "cheese", "dog", "kitten" };
            var i = random.Next(0, words.Count() - 1);
            return words.Skip(i).First();
        }
    }
}