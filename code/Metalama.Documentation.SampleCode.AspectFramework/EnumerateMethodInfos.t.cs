using System;
using System.Collections.Generic;
using System.Reflection;
namespace Doc.EnumerateMethodInfos;
[EnumerateMethodAspect]
internal class Foo
{
  private void Method1()
  {
  }
  private void Method2(int x, string y)
  {
  }
  public IReadOnlyList<MethodInfo> GetMethods()
  {
    var methods = new List<MethodInfo>();
    methods.Add(typeof(Foo).GetMethod("Method1", BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null) ?? throw new MissingMethodException("The method 'Foo.Method1()' could not be found using reflection."));
    methods.Add(typeof(Foo).GetMethod("Method2", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(int), typeof(string) }, null) ?? throw new MissingMethodException("The method 'Foo.Method2(int, string)' could not be found using reflection."));
    return methods;
  }
}