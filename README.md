HighEnergy.TreeView for Xamarin.Forms
=====================================

HighEnergy.TreeView is a layout control for Xamarin.Forms cross-platform apps. TreeView (so far) derives from StackLayout, and the tree is visually defined as a nested collection of StackPanels. The item template for the header is completely flexible, as any content can be inserted into a TreeNodeView header.

As with most new projects, this one is a bit messy, and it's by no means a proper and full-fledged control, but it will get there!

Also included in this repository is a library (HighEnergy.Tree) that enable you to easily define, manipulate, and traverse through non-binary Tree data structures. This is a continuation of the work I started in writing this library back in 2008:
http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/

If anyone is interested in contributing to an open source TreeView control for Xamarin Forms, let's talk!

The goal is to support iOS, Android, and Windows Phone, but I'd like to see the same Tree<T> model able to data bind to other platforms outside Xamarin Forms as well (with new controls being needed for each UI platform).

The Tree data structure library is PCL, and the TreeView library is PCL with a Xamarin.Forms dependency.

Did I mention it's free and open source?
