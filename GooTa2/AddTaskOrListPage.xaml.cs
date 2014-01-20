using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using GooTa2.Data;

namespace GooTa2
{
  public partial class AddTaskOrListPage : PhoneApplicationPage
  {
    public AddTaskOrListPage()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
      FillListSelectors();
      SelectAddType(true);
    }

    private void FillListSelectors()
    {
      foreach (GAccount account in GAccountDataContext.AllAccounts())
      {
        AccountListPicker.Items.Add(account.Email);
      }
      foreach (TaskList list in TaskListDataContext.AllLists())
      {
        TaskListListPicker.Items.Add(list.Title);
      }
    }

    private void OnTaskButtonTap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      SelectAddType(true);
    }

    private void OnListButtonTap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      SelectAddType(false);
    }

    private void SelectAddType(bool isTask)
    {
      FontFamily selected = new FontFamily("Segoe WP Black");
      FontFamily normal = new FontFamily("Segoe WP Semibold");
      TaskButton.FontFamily = isTask ? selected : normal;
      ListButton.FontFamily = isTask ? normal : selected;
      TaskButton.Foreground = isTask ? App.AccentColor : App.White;
      ListButton.Foreground = isTask ? App.White : App.AccentColor;
      TaskForm.Visibility = isTask ? Visibility.Visible : Visibility.Collapsed;
      ListForm.Visibility = isTask ? Visibility.Collapsed : Visibility.Visible;
    }

    private void OnSaveClick(object sender, EventArgs e)
    {
      string error;
      if ((TaskButton.Foreground as SolidColorBrush).Color == Colors.White)
      {
        error = SaveList();
      }
      else
      {
        error = SaveTask();
      }

      if (error != null)
      {
        MessageBox.Show(error);
      }
      else
      {
        NavigationService.GoBack();
      }
    }

    private string SaveTask()
    {
      DateTime dueTime = (DateTime)TaskTime.Value;
      DateTime dueDate = ((DateTime)TaskDate.Value)
        .AddMinutes(dueTime.Minute)
        .AddHours(dueTime.Hour);
      Debug.WriteLine("Due " + dueDate);

      // TODO: position, parent
      Task task = new Task
      {
        IsCompleted = false,
        ListID = TaskListListPicker.SelectedItem as string,
        Notes = TaskNotes.Text,
        // This will eventually be replaced by Google magic
        TaskID = DateTime.Now.ToFileTime() + "",
        Title = TaskTitle.Text,
      };

      if (string.IsNullOrEmpty(task.ListID))
      {
        return "You must select a list for your task!";
      }

      TaskDataContext.Instance.Tasks.InsertOnSubmit(task);
      TaskDataContext.Instance.SubmitChanges();
      return null;
    }

    private string SaveList()
    {
      TaskList list = new TaskList
      {
        Account = AccountListPicker.SelectedItem as string,
        // This will eventually be replaced by Google magic
        ListID = DateTime.Now.ToFileTime() + "",
        Title = ListTitle.Text
      };
      if (string.IsNullOrEmpty(list.Title))
      {
        return "You must add a title to your list!";
      }
      if (string.IsNullOrEmpty(list.Account))
      {
        return "You must select an account for your list!";
      }

      // Add the list!
      TaskListDataContext.Instance.TaskLists.InsertOnSubmit(list);
      TaskListDataContext.Instance.SubmitChanges();
      return null;
    }
  }
}