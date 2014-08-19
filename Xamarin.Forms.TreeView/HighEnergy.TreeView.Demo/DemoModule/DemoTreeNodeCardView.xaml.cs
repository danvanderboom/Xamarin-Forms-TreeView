using System;
using System.Collections.Generic;
using Xamarin.Forms;
using HighEnergy.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace HighEnergy.TreeView.Demo
{
    public partial class DemoTreeCardView : ContentView
    {
        DemoTreeNode Node { get; set; }

        public DemoTreeCardView()
        {
            InitializeComponent();

            Debug.WriteLine("new DemoTreeCardView");

            // TODO: replace this mechanism with binding to Width, if possible
            SizeChanged += (sender, e) => AdjustSpacer();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var newNode = BindingContext as DemoTreeNode;
            if (newNode == null)
            {
                if (Node != null)
                {
                    Debug.WriteLine("OnBindingContextChanged: Node.PropertyChanged unsubscribe");
                    Node.PropertyChanged -= (sender, e) => AdjustSpacer();
                }

                Spacer.WidthRequest = 0;
                return;
            }

            Node = BindingContext as DemoTreeNode;
            if (Node != null)
            {
                Debug.WriteLine("OnBindingContextChanged: Node.PropertyChanged subscribe");
                Node.PropertyChanged += (sender, e) => AdjustSpacer();
            }
        }

        void AdjustSpacer()
        {
            if (Node != null)
                Spacer.WidthRequest = Node.IndentWidth;
        }
    }
}