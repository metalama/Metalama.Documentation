using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using Metalama.Documentation.QuickStart;


namespace DebugDemo3
{
    public class AddLogAspectInGivenNamespaceFabric : ProjectFabric
    {
        /// <summary>
        /// Amends the project by adding Log aspect 
        /// to many eligible methods inside given namespace.
        /// </summary>
        public override void AmendProject(IProjectAmender amender)
        {
            //Adding Log attribute to all mehtods of all types 
            //that are available inside "Outer.Inner" namespace 

            amender
                .SelectMany(t => t.GlobalNamespacef() )
                .Where(ns => ns.FullName.StartsWith("Outer.Inner"))
                .SelectTypes()
                .SelectMany(t => t.Methods)
                .AddAspectIfEligible<LogAttribute>();
        }
    }
}
