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
      PropertyChanged(this, new PropertyChangedEventArgs(property));
    }

    public event PropertyChangingEventHandler PropertyChanging;
    private void NotifyPropertyChanging(string property)
    {
      PropertyChanging(this, new PropertyChangingEventArgs(property));
    }
  }

  public class GAccountDataContext : DataContext
  {
    private static string DBConnection = "Data Source=isostore:/GooTaGAccount.sdf";

    public GAccountDataContext() : base(DBConnection) { }

    public Table<GAccount> GAccounts;
  }
}
