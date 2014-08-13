using System;
using System.Collections.Generic;
using Xamarin.Forms;

public static class GridExtensions
{
    public static void PlaceInGrid(this BindableObject obj, int row, int column, int rowSpan = 1, int columnSpan = 1)
    {
        Grid.SetRow(obj, row);
        Grid.SetColumn(obj, column);
        Grid.SetRowSpan(obj, rowSpan);
        Grid.SetColumnSpan(obj, columnSpan);
    }
}