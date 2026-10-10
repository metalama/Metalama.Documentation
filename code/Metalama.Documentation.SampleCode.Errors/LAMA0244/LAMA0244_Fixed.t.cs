using System;
using Metalama.Framework.Aspects;
namespace Doc.LAMA0244.Fixed;
// Fixed: [RunTimeOrCompileTime] makes the interface available at compile time.
[RunTimeOrCompileTime]
public interface INameFormatter
{
  string Format(string name);
}
// The compile-time class can now implement the interface.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[CompileTime]
public class UpperCaseNameFormatter : INameFormatter
{
  public string Format(string name) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing ADD.");
    return a + b;
  }
}