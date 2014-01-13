using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using GooTa2.Data;
using GooTa2.ViewModels;

namespace GooTa2
{
  public partial class SummaryPage : PhoneApplicationPage
  {
    private static Color[] _boxColors = 
    {
      Colors.Red,
      Colors.Blue,
      Colors.Orange,
      Colors.Purple,
      Colors.Green
    };

    public SummaryPage()
    {
      InitializeComponent();
      DataContext = App.SummaryPageViewModel;

      // Initialize UI
      this.FillRecentListsGrid();
    }

    private void FillRecentListsGrid()
    {
      List<TaskList> list = App.SummaryPageViewModel.PopularLists;
      for (int i = 0; i < Math.Min(_boxColors.Length, list.Count); i++)
      {
        // TODO: Fill in with details from tasks lists!
        Border box = RenderSummaryBox(i, "Test task list", i);
        Grid.SetRow(box, (i + 1) / 2);
        Grid.SetColumn(box, (i + 1) % 2);
        RecentListsGrid.Children.Add(box);
      }
    }

    private Border RenderSummaryBox(int position, string taskTitle, int undone)
    {
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition());
      grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

      TextBlock countBlock = new TextBlock
      {
        Text = "" + undone,
        Foreground = new SolidColorBrush(Colors.White),
        VerticalAlignment = System.Windows.VerticalAlignment.Center,
        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
        FontSize = 64,
        Margin = new Thickness(0)
      };
      Grid.SetRow(countBlock, 0);
      grid.Children.Add(countBlock);

      TextBlock titleBlock = new TextBlock
      {
        Text = taskTitle,
        Foreground = new SolidColorBrush(Colors.White),
        VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
        Margin = new Thickness(0),
        FontFamily = new FontFamily("Segoe WP Semibold"),
        FontSize = 23
      };
      Grid.SetRow(titleBlock, 1);
      grid.Children.Add(titleBlock);

      return new Border
      {
        Padding = new Thickness(7),
        Margin = new Thickness(20),
        Background = new SolidColorBrush(_boxColors[position]),
        Child = grid
      };
    }

    private void OnTaskTap(object sender, System.Windows.Input.GestureEventArgs e)
    {

    }

    private void OnAccountCheckboxTap(object sender, System.Windows.Input.GestureEventArgs e)
    {

    }

    private void OnAccountTap(object sender, System.Windows.Input.GestureEventArgs e)
    {

    }
  }
}