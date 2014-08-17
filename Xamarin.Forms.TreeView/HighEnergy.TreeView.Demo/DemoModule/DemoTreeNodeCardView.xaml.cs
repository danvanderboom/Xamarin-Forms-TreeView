using System;
using System.Collections.Generic;
using Xamarin.Forms;
using HighEnergy.Collections;

namespace HighEnergy.TreeView.Demo
{
    public partial class DemoTreeCardView : ContentView
    {
        DemoTreeNode Node { get; set; }

        public DemoTreeCardView()
        {
            InitializeComponent();

            SizeChanged += (sender, e) => AdjustSpacer(Node);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var newNode = BindingContext as DemoTreeNode;
            if (newNode == null)
            {
                if (Node != null)
                    Node.PropertyChanged -= (sender, e) => AdjustSpacer(Node);

                Spacer.WidthRequest = 0;
                return;
            }

            Node = BindingContext as DemoTreeNode;

            if (Node != null)
                Node.PropertyChanged += (sender, e) => AdjustSpacer(Node);
        }

        void AdjustSpacer(DemoTreeNode node)
        {
            Spacer.WidthRequest = node.IndentWidth;
        }
    }
}