using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooTa2.Data
{
  [Table]
  public class GAccount : INotifyPropertyChanged, INotifyPropertyChanging
  {
    private string _email;
    [Column(IsPrimaryKey = true, CanBeNull = false)]
    public string Email
    {
      get { return _email; }
      set
      {
        if (_email != value)
        {
          NotifyPropertyChanging("Email");
          _email = value;
          NotifyPropertyChanged("Email");
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

  public class GAccountDataContext : DataContext
  {
    private static string DBConnection = "Data Source=isostore:/GooTaGAccount.sdf";

    private static GAccountDataContext _instance;
    public static GAccountDataContext Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new GAccountDataContext();
        }
        return _instance;
      }
    }

    public Table<GAccount> Accounts;

    private GAccountDataContext() : base(DBConnection) { }

    public static List<GAccount> AllAccounts()
    {
      var accounts = from GAccount account in Instance.Accounts select account;
      return new List<GAccount>(accounts);
    }
  }
}
