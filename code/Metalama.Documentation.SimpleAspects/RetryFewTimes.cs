using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SimpleAspects
{
    public class RetryFewTimes : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            for(int i = 0; ; i++)
            {
                try
                {
                    return meta.Proceed();
                }
                catch(Exception e) when (i < 3)
                {
                    Console.WriteLine($"{e.Message}. Retrying in 100 ms.");
                    Thread.Sleep(100);
                }
            }
        }
    }
}
