using System;
using System.Diagnostics;
using Xamarin.Forms;
using HighEnergy.Controls;

namespace HighEnergy.TreeView.Demo
{
    public class DemoTreeView : HighEnergy.Controls.TreeView
    {
        DemoTreeViewModel ViewModel;

        public DemoTreeView()
        {
            // these properties have to be set in a specific order, letting us know that we're doing some dumb things with properties and will need to 
            // TODO: fix this later

            ViewModel = new DemoTreeViewModel();

            NodeCreationFactory =
                () => new TreeNodeView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    BackgroundColor = Color.Blue
                };

            HeaderCreationFactory = 
                () => new DemoTreeCardView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start
                };

            HeaderCreationFactory = 
                () =>
                {
                    var result = new DemoTreeCardView
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start
                    };
                    Debug.WriteLine("HeaderCreationFactory: new DemoTreeCardView");
                    return result;
                };

            BindingContext = ViewModel.MyTree;

            ViewModel.InsertRandomNodes();
        }
    }
}