using System;
namespace Doc.NamespaceFabric_
{
  namespace TargetNamespace
  {
    internal class TargetClass
    {
      public void TargetMethod()
      {
        Console.WriteLine("Entering TargetClass.TargetMethod().");
        try
        {
          Console.WriteLine("Executing TargetMethod");
          return;
        }
        finally
        {
          Console.WriteLine("Leaving TargetClass.TargetMethod().");
        }
      }
    }
  }
  namespace OtherNamespace
  {
    internal class OtherClass
    {
      public void OtherMethod()
      {
        Console.WriteLine("Executing OtherMethod");
      }
    }
  }
}