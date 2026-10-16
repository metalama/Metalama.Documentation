using System;
using Metalama.Framework.RunTime;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.InitializerOrdering;
[AspectA]
[AspectB]
public partial class BaseClass
{
  public BaseClass([AspectGenerated] InitializationContext context)
  {
    Console.WriteLine("BaseClass constructor");
    if (!context.IsHandled(InitializationSlot.OnConstructed))
    {
      this.OnConstructed(context);
    }
  }
  public BaseClass() : this(context: default)
  {
  }
  protected virtual void OnConstructed(InitializationContext context = default)
  {
    Console.WriteLine("AspectA before in BaseClass");
    Console.WriteLine("AspectB before in BaseClass");
    Console.WriteLine("AspectB after in BaseClass");
    Console.WriteLine("AspectA after in BaseClass");
  }
}
public partial class DerivedClass : BaseClass
{
  public DerivedClass([AspectGenerated] InitializationContext context) : base(context.Descend(InitializationSlot.OnConstructed))
  {
    Console.WriteLine("DerivedClass constructor");
    if (!context.IsHandled(InitializationSlot.OnConstructed))
    {
      this.OnConstructed(context);
    }
  }
  public DerivedClass() : this(context: default)
  {
  }
  protected override void OnConstructed(InitializationContext context = default)
  {
    Console.WriteLine("AspectA before in DerivedClass");
    Console.WriteLine("AspectB before in DerivedClass");
    base.OnConstructed(context);
    Console.WriteLine("AspectB after in DerivedClass");
    Console.WriteLine("AspectA after in DerivedClass");
  }
}