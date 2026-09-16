// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @Skipped(Bug 30453)
#endif

namespace Doc.InheritedMethodLevel;

internal class BaseClass
{
    [InheritedAspect]
    public virtual void ClassMethodWithAspect() { }

    public virtual void ClassMethodWithoutAspect() { }
}

internal interface IInterface
{
    [InheritedAspect]
    private void InterfaceMethodWithAspect() { }

    private void InterfaceMethodWithoutAspect() { }
}

internal class DerivedClass : BaseClass, IInterface
{
    public override void ClassMethodWithAspect()
    {
        base.ClassMethodWithAspect();
    }

    public override void ClassMethodWithoutAspect()
    {
        base.ClassMethodWithoutAspect();
    }

    public virtual void InterfaceMethodWithAspect() { }

    public virtual void InterfaceMethodWithoutAspect() { }
}

internal class DerivedTwiceClass : DerivedClass
{
    public override void ClassMethodWithAspect()
    {
        base.ClassMethodWithAspect();
    }

    public override void ClassMethodWithoutAspect()
    {
        base.ClassMethodWithoutAspect();
    }

    public override void InterfaceMethodWithAspect()
    {
        base.InterfaceMethodWithAspect();
    }

    public override void InterfaceMethodWithoutAspect()
    {
        base.InterfaceMethodWithoutAspect();
    }
}