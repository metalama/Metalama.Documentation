// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;

namespace Doc.TransitiveFabric;

// <Fabric>
// This transitive fabric applies logging to all public methods in any project
// that references this assembly.
public class LoggingPolicyFabric : TransitiveProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender
            .SelectTypes()
            .Where( t => t.Accessibility == Accessibility.Public )
            .SelectMany( t => t.Methods )
            .Where( m => m.Accessibility == Accessibility.Public && m.Name != "ToString" )
            .AddAspectIfEligible<LogAttribute>();
    }
}
// </Fabric>

// <Aspect>
public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Leaving {meta.Target.Method}" );
        }
    }
}
// </Aspect>
