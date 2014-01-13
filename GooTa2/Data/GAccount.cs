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

    private byte _red;
    [Column]
    public byte Red
    {
      get { return _red; }
      set
      {
        if (_red != value)
        {
          NotifyPropertyChanging("Red");
          _red = value;
          NotifyPropertyChanged("Red");
        }
      }
    }

    private byte _green;
    [Column]
    public byte Green
    {
      get { return _green; }
      set
      {
        if (_green != value)
        {
          NotifyPropertyChanging("Green");
          _green = value;
          NotifyPropertyChanged("Green");
        }
      }
    }

    private byte _blue;
    [Column]
    public byte Blue
    {
      get { return _blue; }
      set
      {
        if (_blue != value)
        {
          NotifyPropertyChanging("Blue");
          _blue = value;
          NotifyPropertyChanged("Blue");
        }
      }
    }

    private byte _alpha;
    [Column]
    public byte Alpha
    {
      get { return _alpha; }
      set
      {
        if (_alpha != value)
        {
          NotifyPropertyChanging("Alpha");
          _alpha = value;
          NotifyPropertyChanged("Alpha");
        }
      }
    }

    public Color Color
    {
      get
      {
        return Color.FromArgb(Alpha, Red, Green, Blue);
      }        
      set
      {
        if (value != null)
        {
          Alpha = value.A;
          Red = value.R;
          Green = value.G;
          Blue = value.B;
        }
      }
    }

    public SolidColorBrush Brush
    {
      get
      {
        return new SolidColorBrush(Color);
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

    public static ObservableCollection<GAccount> AllAccounts()
    {
      var accounts = from GAccount account in Instance.Accounts select account;
      return new ObservableCollection<GAccount>(accounts);
    }

    public static GAccount AccountForEmail(string email)
    {
      var accounts = from GAccount account
                       in Instance.Accounts
                     where account.Email.Equals(email)
                     select account;
      List<GAccount> listy = new List<GAccount>(accounts);
      return listy.Count > 0 ? listy[0] : null;
    }
  }
}
