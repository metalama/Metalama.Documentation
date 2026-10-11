// Warning LAMA0119 on `LogFormatter`: `The compile-time declaration 'LogFormatter' is in a file that does not have a using directive for the 'Metalama.Framework' namespace or one of its main child namespaces. This may cause an inconsistent design-time experience. Add a using directive such as 'using Metalama.Framework.Aspects;' to the file.`
// Warning LAMA0119 on `GetEntryMessage`: `The compile-time declaration 'LogFormatter.GetEntryMessage(string)' is in a file that does not have a using directive for the 'Metalama.Framework' namespace or one of its main child namespaces. This may cause an inconsistent design-time experience. Add a using directive such as 'using Metalama.Framework.Aspects;' to the file.`
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