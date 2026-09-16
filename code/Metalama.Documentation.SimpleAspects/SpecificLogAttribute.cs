using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Metalama.Documentation.SimpleAspects
{
    public class SpecificLogAttribute : OverrideMethodAspect
    {
 
        public override dynamic OverrideMethod()
        {
            string targetMethodName = meta.Target.Method.ToDisplayString();
            try
            {
                Console.WriteLine($"Started executing {targetMethodName}");
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine($"Finished executing {targetMethodName}");
            }
        }
    }
}
