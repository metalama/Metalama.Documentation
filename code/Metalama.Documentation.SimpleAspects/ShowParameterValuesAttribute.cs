using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SimpleAspects
{
    public class ShowParameterValuesAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            string methodName = meta.Target.Method.ToDisplayString();
            try
            {
                Console.WriteLine($"Started {methodName}");
                foreach (var parameter in meta.Target.Method.Parameters)
                {
                    Console.WriteLine($"{parameter.Name} = {parameter.Value}");
                }
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine($"Finished {methodName}");
            }
        }
    }
}
