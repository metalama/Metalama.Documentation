using Metalama.Framework.Fabrics;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Observability.Configuration;
using System;
using System.ComponentModel;
namespace Doc.Constant_Fabric;
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
  public double Norm => VectorHelper.ComputeNorm(this);
  public Vector Direction => VectorHelper.Normalize(this);
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
public static class VectorHelper
{
  public static double ComputeNorm(Vector v) => Math.Sqrt((v.X * v.X) + (v.Y * v.Y));
  public static Vector Normalize(Vector v)
  {
    var norm = ComputeNorm(v);
    return new Vector
    {
      X = v.X / norm,
      Y = v.Y / norm
    };
  }
}
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}