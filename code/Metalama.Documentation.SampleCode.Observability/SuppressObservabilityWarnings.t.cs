// Warning LAMA5162 on `ComputeNorm`: `The 'VectorHelper.ComputeNorm(Vector)' method cannot be analysed, and has not been configured with an observability contract. Mark this method with [ConstantAttribute] or ConfigureObservability via a fabric.`
using Metalama.Patterns.Observability;
using System;
using System.ComponentModel;
namespace Doc.SuppressObservabilityWarnings;
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
        OnPropertyChanged("Y");
      }
    }
  }
  public double NormWithWarning => VectorHelper.ComputeNorm(this);
  [SuppressObservabilityWarnings]
  public double NormWithoutWarning1 => VectorHelper.ComputeNorm(this);
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
public static class VectorHelper
{
  public static double ComputeNorm(Vector v) => Math.Sqrt(v.X * v.X + v.Y * v.Y);
}