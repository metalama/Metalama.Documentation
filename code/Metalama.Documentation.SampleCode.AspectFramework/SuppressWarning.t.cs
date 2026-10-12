using System;
using System.IO;
namespace Doc.SuppressWarning;
internal class Program
{
  private TextWriter _logger = Console.Out;
  [Log]
  private void Foo()
  {
    _logger.WriteLine("Executing Program.Foo().");
  }
  private static void Main()
  {
    new Program().Foo();
  }
}