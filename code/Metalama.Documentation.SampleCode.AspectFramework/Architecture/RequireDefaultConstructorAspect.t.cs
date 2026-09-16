// Warning MY001 on `InvalidClass`: `The type 'InvalidClass' must have a public default constructor.`
namespace Doc.Architecture.RequireDefaultConstructorAspect;
// Apply the aspect to the base class. It will be inherited to all derived classes.
[RequireDefaultConstructor]
public class BaseClass
{
}
// This class has an implicit default constructor.
public class ValidClass1 : BaseClass
{
}
// This class has an explicit default constructor.
public class ValidClass2 : BaseClass
{
  public ValidClass2()
  {
  }
}
// This class has no default constructor.
public class InvalidClass : BaseClass
{
  public InvalidClass(int x)
  {
  }
}