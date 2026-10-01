using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SimpleAspects
{
    public class SimpleLogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            //Enhancing the method 
            Console.WriteLine( $"Simply logging a method..." );
            //Let the method do its own thing 
            return meta.Proceed();
        }
    }
}