using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace GooTa2.Data
{
  [Table]
  public class Task : INotifyPropertyChanged, INotifyPropertyChanging
  {
    private string _id;
    [Column(IsPrimaryKey = true, CanBeNull = false)]
    public String TaskID
    {
      get { return _id; }
      set
      {
        if (value != _id)
        {
          NotifyPropertyChanging("TaskID");
          _id = value;
          NotifyPropertyChanged("TaskID");
        }
      }
    }

    private string _listID;
    [Column(CanBeNull = false)]
    public string ListID
    {
      get { return _listID; }
      set
      {
        if (value != _listID)
        {
          NotifyPropertyChanging("ListID");
          _listID = value;
          NotifyPropertyChanged("ListID");
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

    private string _parent;
    [Column]
    public string Parent
    {
      get { return _parent; }
      set
      {
        if (value != _parent)
        {
          NotifyPropertyChanging("Parent");
          _parent = value;
          NotifyPropertyChanged("Parent");
        }
      }
    }

    private string _position;
    [Column]
    public string Position
    {
      get { return _position; }
      set
      {
        if (value != _position)
        {
          NotifyPropertyChanging("Position");
          _position = value;
          NotifyPropertyChanged("Position");
        }
      }
    }

    private string _notes;
    [Column]
    public string Notes
    {
      get { return _notes; }
      set
      {
        if (value != _notes)
        {
          NotifyPropertyChanging("Notes");
          _notes = value;
          NotifyPropertyChanged("Notes");
        }
      }
    }

    private string _status;
    [Column]
    private string Status
    {
      get { return _status; }
      set
      {
        if (value != _status)
        {
          NotifyPropertyChanging("Status");
          _status = value;
          NotifyPropertyChanged("Status");
        }
      }
    }
    public bool IsCompleted
    {
      get { return _status == "completed"; }
      set { _status = value ? "completed" : "needsAction"; }
    }

    private string _due;
    [Column]
    public string Due
    {
      get { return _due; }
      set
      {
        if (value != _due)
        {
          NotifyPropertyChanging("Due");
          _due = value;
          NotifyPropertyChanged("Due");
        }
      }
    }

    private bool _deleted;
    [Column]
    public bool Deleted
    {
      get { return _deleted; }
      set
      {
        if (value != _deleted)
        {
          NotifyPropertyChanging("Deleted");
          _deleted = value;
          NotifyPropertyChanged("Deleted");
        }
      }
    }

    private bool _hidden;
    [Column]
    public bool Hidden
    {
      get { return _hidden; }
      set
      {
        if (value != _hidden)
        {
          NotifyPropertyChanging("Hidden");
          _hidden = value;
          NotifyPropertyChanged("Hidden");
        }
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

  public class TaskDataContext : DataContext
  {
    private static string DBConnection = "Data Source=isostore:/GooTaTask.sdf";

    private static TaskDataContext _instance;
    public static TaskDataContext Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new TaskDataContext();
        }
        return _instance;
      }
    }

    public Table<Task> Tasks;

    private TaskDataContext() : base(DBConnection) { }

    public static List<Task> GetAllTasks()
    {
      var tasks = from Task task in Instance.Tasks select task;
      return new List<Task>(tasks);
    }
  }
}
