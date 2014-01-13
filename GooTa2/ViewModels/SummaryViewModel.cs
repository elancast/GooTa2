using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using GooTa2.Data;
using GooTa2.Resources;

namespace GooTa2.ViewModels
{
  public class SummaryViewModel : INotifyPropertyChanged
  {
    public ObservableCollection<TaskList> AllLists { get; private set; }
    public List<TaskList> PopularLists { get; private set; }

    public ObservableCollection<GAccount> AllAccounts { get; private set; }

    public SummaryViewModel()
    {
      AllLists = TaskListDataContext.AllLists();
      AllAccounts = GAccountDataContext.AllAccounts();

      // For testing!!!
      if (AllLists.Count < 10)
      {
        AddTestData();
      }

      PopularLists = new List<TaskList>();
      foreach (TaskList list in AllLists)
      {
        PopularLists.Add(list);
        if (PopularLists.Count > 5) break;
      }
    }

    private void AddTestData()
    {
      GAccount account = new GAccount { Email = "elancast0421@gmail.com", Color = Colors.Orange };
      AllAccounts.Add(account);
      GAccountDataContext.Instance.Accounts.InsertOnSubmit(account);
      GAccountDataContext.Instance.SubmitChanges();

      TaskList[] tasks = 
      {
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test1", Title = "Cool list 1" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test2", Title = "Cool list 2" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test3", Title = "Cool list 3" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test4", Title = "Cool list 4" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test5", Title = "Cool list 5" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test6", Title = "Cool list 6" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test7", Title = "Cool list 7" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test8", Title = "Cool list 8" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test9", Title = "Cool list 9" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test10", Title = "Cool list 10" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test11", Title = "Cool list 11" },
        new TaskList { Account = "elancast0421@gmail.com", ListID = "test12", Title = "Cool list 12" },
      };
      foreach (TaskList list in tasks)
      {
        AllLists.Add(list);
        TaskListDataContext.Instance.TaskLists.InsertOnSubmit(list);
      }
      TaskListDataContext.Instance.SubmitChanges();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string property)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
    }
  }
}
