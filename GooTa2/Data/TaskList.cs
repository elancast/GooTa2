using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GooTa2.Data
{
  [Table]
  public class TaskList : INotifyPropertyChanged, INotifyPropertyChanging
  {
    private string _id;
    [Column(IsPrimaryKey = true, CanBeNull = false)]
    public string ListID
    {
      get { return _id; }
      set
      {
        if (value != _id)
        {
          NotifyPropertyChanging("ListID");
          _id = value;
          NotifyPropertyChanged("ListID");
        }
      }
    }

    private string _account;
    [Column(CanBeNull = false)]
    public string Account
    {
      get { return _account; }
      set
      {
        if (value != _account)
        {
          NotifyPropertyChanging("Account");
          _account = value;
          NotifyPropertyChanged("Account");
        }
      }
    }

    private string _title;
    [Column]
    public string Title
    {
      get { return _title; }
      set
      {
        if (value != _title)
        {
          NotifyPropertyChanging("Title");
          _title = value;
          NotifyPropertyChanged("Title");
        }
      }
    }

    private string _updated;
    [Column]
    public string Updated
    {
      get { return _updated; }
      set
      {
        if (value != _updated)
        {
          NotifyPropertyChanging("Updated");
          _updated = value;
          NotifyPropertyChanged("Updated");
        }
      }
    }

    public SolidColorBrush AccountColorBrush
    {
      get
      {
        return AccountObject.Brush;
      }
    }

    private GAccount _accountObject;
    public GAccount AccountObject
    {
      get
      {
        if (_accountObject == null)
        {
          _accountObject = GAccountDataContext.AccountForEmail(Account);
        }
        return _accountObject;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string property)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;
    private void NotifyPropertyChanging(string property)
    {
      if (PropertyChanging != null)
      {
        PropertyChanging(this, new PropertyChangingEventArgs(property));
      }
    }
  }

  public class TaskListDataContext : DataContext
  {
    private static string DBConnection = "Data Source=isostore:/GooTaTaskList.sdf";

    private static TaskListDataContext _instance;
    public static TaskListDataContext Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new TaskListDataContext();
        }
        return _instance;
      }
    }

    public Table<TaskList> TaskLists;

    private TaskListDataContext() : base(DBConnection) { }

    public static ObservableCollection<TaskList> AllLists()
    {
      var lists = from TaskList list in Instance.TaskLists select list;
      return new ObservableCollection<TaskList>(lists);
    }
  }
}
