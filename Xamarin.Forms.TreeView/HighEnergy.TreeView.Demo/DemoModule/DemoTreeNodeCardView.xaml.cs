using System;
using System.Collections.Generic;
using Xamarin.Forms;
using HighEnergy.Collections;
using System.ComponentModel;

namespace HighEnergy.TreeView.Demo
{
    public partial class DemoTreeCardView : ContentView
    {
        DemoTreeNode Node { get; set; }

        public DemoTreeCardView()
        {
            InitializeComponent();

            SizeChanged += (sender, e) => AdjustSpacer();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var newNode = BindingContext as DemoTreeNode;
            if (newNode == null)
            {
                if (Node != null)
                    Node.PropertyChanged -= (sender, e) => AdjustSpacer();

                Spacer.WidthRequest = 0;
                return;
            }

            Node = BindingContext as DemoTreeNode;
            if (Node != null)
                Node.PropertyChanged += (sender, e) => AdjustSpacer();
        }

        void AdjustSpacer()
        {
            if (Node != null)
                Spacer.WidthRequest = Node.IndentWidth;
        }
    }
}