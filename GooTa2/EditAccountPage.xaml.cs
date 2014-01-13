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

namespace GooTa2.Data
{
  public partial class EditAccountPage : PhoneApplicationPage
  {
    private static Color[] _colors =
    {
      Colors.Red,
      Colors.Purple,
      Colors.Blue,
      Colors.Cyan,
      Colors.Green,
      Colors.Yellow,
      Colors.Orange,
      Colors.Brown
    };

    private GAccount _account;
    private Color _selectedColor;

    public EditAccountPage()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);

      string email = "";
      if (!NavigationContext.QueryString.TryGetValue("email", out email))
      {
        throw new Exception("Invalid query");
      }

      _account = GAccountDataContext.AccountForEmail(email);
      _selectedColor = _account.Color;

      DrawColorGrid();
    }

    private void DrawColorGrid()
    {
      ColorGrid.Children.Clear();
      for (int i = 0; i < _colors.Length; i++)
      {
        Border b = RenderColor(_colors[i], _colors[i] == _selectedColor);
        Grid.SetRow(b, i / 4);
        Grid.SetColumn(b, i % 4);
        ColorGrid.Children.Add(b);
      }
    }

    private Border RenderColor(Color color, bool isSelected)
    {
      Border border = new Border();
      border.Background = new SolidColorBrush(color);
      border.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(OnColorTap);
      ReflectSelection(border, isSelected);
      return border;
    }

    private void ReflectSelection(Border border, bool isSelected)
    {
      border.BorderBrush = isSelected ? new SolidColorBrush(Colors.White) : null;
      border.BorderThickness = new Thickness(isSelected ? 5 : 0);
      border.Margin = new Thickness(isSelected ? 7 : 10);
    }

    private void OnColorTap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _selectedColor = ((sender as Border).Background as SolidColorBrush).Color;
      DrawColorGrid();
    }

    private void SaveClick(object sender, EventArgs e)
    {
      _account.Color = this._selectedColor;
      GAccountDataContext.Instance.SubmitChanges();
      NavigationService.GoBack();
    }

    private void DeleteClick(object sender, EventArgs e)
    {
      // TODO: Ask if sure and then delete account + everything it owns
      NavigationService.GoBack();
    }
  }
}