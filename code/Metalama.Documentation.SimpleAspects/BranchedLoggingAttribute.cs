using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SimpleAspects
{
    public class BranchedLoggingAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            string methodName = meta.Target.Method.ToDisplayString();
            if (meta.Target.Method.ReturnType.Is(SpecialType.Void))
            {
                Console.WriteLine($"void method {methodName} is called");
            }
            else
            {
                Console.WriteLine($"method {methodName} is called");
            }
            return meta.Proceed();
        }
    }
}
