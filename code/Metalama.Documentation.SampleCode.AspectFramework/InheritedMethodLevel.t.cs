using System;

namespace Doc.InheritedTypeLevel
{
    [InheritedAspect]
    internal class BaseClass
    {
        public void Method1()
        {
            Console.WriteLine("Hacked!");
            return;
        }
        public virtual void Method2()
        {
            Console.WriteLine("Hacked!");
            return;
        }
    }

    internal class DerivedClass : BaseClass
    {
        public override void Method2()
        {
            Console.WriteLine("Hacked!");
            base.Method2();
            return;
        }

        public void Method3()
        {
            Console.WriteLine("Hacked!");
            return;
        }
    }

    internal class DerivedTwiceClass : DerivedClass
    {
        public override void Method2()
        {
            Console.WriteLine("Hacked!");
            base.Method2();
            return;
        }

        public void Method4()
        {
            Console.WriteLine("Hacked!");
            return;
        }
    }
}
