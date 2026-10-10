// Warning LAMA0119 on `LogFormatter`: `The compile-time declaration 'LogFormatter' contains compile-time code but it does not explicitly import any of the the 'Metalama.Framework' namespaces. This may cause an inconsistent design-time experience. Add the [CompileTimeAttribute] attribute to 'LogFormatter' and import this namespace explicitly.`
// Warning LAMA0119 on `GetEntryMessage`: `The compile-time declaration 'LogFormatter.GetEntryMessage(string)' contains compile-time code but it does not explicitly import any of the the 'Metalama.Framework' namespaces. This may cause an inconsistent design-time experience. Add the [CompileTimeAttribute] attribute to 'LogFormatter.GetEntryMessage(string)' and import this namespace explicitly.`
using System;
namespace Doc.LAMA0119.Error;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Entering Add.");
    return a + b;
  }
}