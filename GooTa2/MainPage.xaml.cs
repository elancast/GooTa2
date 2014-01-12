using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using GooTa2.Data;

namespace GooTa2
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      // Set the data context of the listbox control to the sample data
      DataContext = App.ViewModel;

      List<GAccount> accounts = GAccountDataContext.AllAccounts();
      Debug.WriteLine("Accounts: " + accounts.Count);
      GAccount account = new GAccount 
      {
        Email = "test email " + System.DateTime.UtcNow.ToFileTime()
      };
      GAccountDataContext.Instance.Accounts.InsertOnSubmit(account);
      GAccountDataContext.Instance.SubmitChanges();

      List<Task> tasks = TaskDataContext.GetAllTasks();
      Debug.WriteLine("Tasks: " + tasks.Count);
      Task task = new Task 
      {
        TaskID = System.DateTime.UtcNow.ToFileTime() + "id",
        ListID = System.DateTime.UtcNow.ToFileTime() + "listid"
      };
      TaskDataContext.Instance.Tasks.InsertOnSubmit(task);
      TaskDataContext.Instance.SubmitChanges();
    }

    // Load data for the ViewModel Items
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      if (!App.ViewModel.IsDataLoaded)
      {
        App.ViewModel.LoadData();
      }
    }
  }
}