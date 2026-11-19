// Warning LAMA0892 on `_defaultFormat`: `'LogAttribute' is loaded in the compiler process, but its static member '_defaultFormat' is of type 'LogFormat', which is not durable. A static field of a compile-time type is shared by every aspect instance, every target and every compilation, and at design time it keeps its value for as long as the analysis process runs. The member must therefore not reference a compilation, a syntax tree, a symbol or a code model declaration. Path: LogAttribute -> _defaultFormat -> LogFormat (the type is not marked [Durable]).`
using System;
namespace Doc.LAMA0892.Error;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Entering Calculator.Add(int, int).");
    return a + b;
  }
}