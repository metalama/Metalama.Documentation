// This is public domain Metalama sample code.

namespace Doc.InheritedTypeLevel;

[InheritedAspect]
internal class BaseClass
{
    public void Method1() { }

    public virtual void Method2() { }
}

internal class DerivedClass : BaseClass
{
    public override void Method2()
    {
        base.Method2();
    }

    public void Method3() { }
}

internal class DerivedTwiceClass : DerivedClass
{
    public override void Method2()
    {
        base.Method2();
    }

    public void Method4() { }
}