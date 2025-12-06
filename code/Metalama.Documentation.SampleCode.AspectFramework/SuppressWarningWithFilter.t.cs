// Warning CS0219 on `_other`: `The variable '_other' is assigned but its value is never used`
using System;
namespace Doc.SuppressWarningWithFilter;
internal class MyService
{
  [SuppressUnusedVariable]
  public void Initialize()
  {
    Console.WriteLine("Entering Initialize");
    // The CS0219 warning for this variable is suppressed by the aspect
    // because the variable is named "_initialized".
    var _initialized = true;
    // The CS0219 warning for this variable is NOT suppressed
    // because the filter only matches variables named "_initialized".
    var _other = 42;
  }
}
