using Metalama.Patterns.Observability;
using System;
using System.ComponentModel;
namespace Doc.Constant;
[Observable]
public class Vector : INotifyPropertyChanged
{
  private double _x;
  public double X
  {
    get
    {
      return _x;
    }
    set
    {
      if (_x != value)
      {
        _x = value;
        OnPropertyChanged("Norm");
        OnPropertyChanged("X");
      }
    }
  }
  private double _y;
  public double Y
  {
    get
    {
      return _y;
    }
    set
    {
      if (_y != value)
      {
        _y = value;
        OnPropertyChanged("Norm");
        OnPropertyChanged("Y");
      }
    }
  }
  public double Norm => VectorHelper.ComputeNorm(this.X, this.Y);
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
public static class VectorHelper
{
  //[Constant]
  public static double ComputeNorm(double x, double y) => Math.Sqrt((x * x) + (y * y));
}