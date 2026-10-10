// Warning LAMA0282 on `Add`: `The aspect 'Log' uses features of C# 11.0, but it is used in a project built with C# 10.0. Consider specifying <LangVersion>11.0</LangVersion> in this project or removing newer language features from the template 'LogAttribute.OverrideMethod()' and then specifying <MetalamaTemplateLanguageVersion> in the aspect project.`
using System;
namespace Doc.LAMA0282.Error;
public class Calculator
{
  // The [Log] aspect uses C# 11 features.
  [Log]
  public int Add(int a, int b)
  {
    // Logged by the "Log" aspect.
    Console.WriteLine("Entering Calculator.Add(int, int).");
    return a + b;
  }
}