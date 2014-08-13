using System;
using System.Collections.Generic;
using Xamarin.Forms;
using HighEnergy.Collections;

namespace HighEnergy.Controls
{
    // analog to ITreeNodeList<T>
    public partial class TreeNodeView : StackLayout
    {
        static Random random = new Random(DateTime.Now.Millisecond);
        Grid HeaderGrid;
        ContentView HeaderView;
        ContentView Spacer;
        Image FolderImage;

        public static readonly BindableProperty FolderImageSourceProperty = BindableProperty.Create("FolderImageSource", typeof(ImageSource), typeof(TreeNodeView), null, BindingMode.OneWay, null, 
            delegate(BindableObject bindable, object oldValue, object newValue)
            {
                //bindable.InvalidateMeasure();
            }, 
            null, null);

        public ImageSource FolderImageSource
        {
            get { return (ImageSource)GetValue(FolderImageSourceProperty); }
            set { SetValue(FolderImageSourceProperty, value); }
        }

        public ImageSource GetFolderImageSource()
        {
            if (FolderImageSource != null)
                return FolderImageSource;

            if (Parent != null && Parent is TreeNodeView)
                return (Parent as TreeNodeView).FolderImageSource;

            return null;
        }

        // TOOD: move this to TreeNodeViewModel
        public int Depth
        {
            get { return Parent is TreeNodeView ? (Parent as TreeNodeView).Depth + 1 : 0; }
        }

        public View HeaderContent
        {
            get { return HeaderView.Content; }
            set { HeaderView.Content = value; }
        }

        bool _IsExpanded = true;
        public bool IsExpanded 
        { 
            get { return _IsExpanded; }
            set
            {
                _IsExpanded = value;

                foreach (View control in Children)
                    if (control != HeaderGrid)
                        control.IsVisible = IsExpanded;
            }
        }





        




        public TreeNodeView() : base()
        {
            IsExpanded = true;

            HeaderView = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = this.BackgroundColor
            };

            var button = new Button
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#00000000")
            };

            HeaderGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            HeaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            HeaderGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            HeaderGrid.Children.Add(HeaderView);
            HeaderGrid.Children.Add(button);

            Children.Add(HeaderGrid);

            button.Clicked += 
                // toggle visibility of children in the tree
                (object sender, EventArgs e) => 
                {
                    IsExpanded = !IsExpanded;

                    foreach (View control in Children)
                        if (control != HeaderGrid)
                            control.IsVisible = IsExpanded;
                };

            Spacing = 0;
            //Padding = new Thickness(16, 0, 0, 0);
            Padding = new Thickness(0);
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.Start;





            // TODO: this TreeView item template UI belongs somewhere separate from TreeView!

            var MyTreeNodeViewCard = new Grid
            {
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };
            MyTreeNodeViewCard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            MyTreeNodeViewCard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            MyTreeNodeViewCard.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // left side

            var leftView = new StackLayout
            { 
                HeightRequest = 50,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            MyTreeNodeViewCard.Children.Add(leftView);
            leftView.PlaceInGrid(0, 0);

            Spacer = new ContentView();
            SetSpacerWidth();

            leftView.Children.Add(Spacer);

            FolderImage = new Image 
                { 
                    Source = FolderImageSource, 
                    WidthRequest = 24,
                    Aspect = Aspect.AspectFit
                };
            leftView.Children.Add(FolderImage);

            leftView.Children.Add(new Image { Source = FolderImageSource });
            leftView.Children.Add(
                new Label 
                {
                    Text = "Title Text", 
                    Font = Font.SystemFontOfSize(20, FontAttributes.Bold),
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Center 
                });

            // right side

            var barView = new StackLayout
                { 
                    HeightRequest = 50,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(16, 0, 0, 0)
                };
            MyTreeNodeViewCard.Children.Add(barView);
            barView.PlaceInGrid(0, 1);

//            var spacer2 = new ContentView { WidthRequest = 60 };
//            barView.Children.Add(spacer2);

            barView.Children.Add(
                new Label
                {
                    Text = "33%",
                    Font = Font.SystemFontOfSize(16),
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 40
                });

            var bar = new ContentView 
                { 
                    HeightRequest = 20,
                    WidthRequest = GetRandomBarWidth(),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.Blue
                };
            barView.Children.Add(bar);

            HeaderView.Content = MyTreeNodeViewCard;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            SetSpacerWidth();
            SetFolderImage();
        }

        protected void SetSpacerWidth()
        {
            Spacer.WidthRequest = 40 * Depth;

            foreach (View child in Children)
                if (child is TreeNodeView)
                    (child as TreeNodeView).SetSpacerWidth();
        }

        protected void SetFolderImage()
        {
            FolderImage.Source = GetFolderImageSource();

            foreach (View child in Children)
                if (child is TreeNodeView)
                    (child as TreeNodeView).SetFolderImage();
        }

        double GetRandomBarWidth()
        {
            return random.NextDouble() * 250 + 50;
        }
    }
}