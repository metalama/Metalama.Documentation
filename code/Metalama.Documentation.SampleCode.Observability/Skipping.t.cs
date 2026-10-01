using Metalama.Patterns.Observability;
using System;
using System.ComponentModel;
namespace Doc.Skipping;
[Observable]
public class DateTimeViewModel : INotifyPropertyChanged
{
  private DateTime _dateTime;
  public DateTime DateTime
  {
    get
    {
      return _dateTime;
    }
    set
    {
      if (_dateTime != value)
      {
        _dateTime = value;
        OnPropertyChanged("DateTime");
      }
    }
  }
  [NotObservable]
  public double MinutesFromNow => (DateTime.Now - this.DateTime).TotalMinutes;
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}