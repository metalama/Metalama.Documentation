// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System.Linq;

namespace Doc.SuppressWarning;

internal class LogAttribute : OverrideMethodAspect
{
    private static readonly SuppressionDefinition _suppression = new( "CS0169" );

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        var loggerField = builder.Target.DeclaringType.Fields.OfName( "_logger" ).FirstOrDefault();

        if ( loggerField != null )
        {
            // Suppress "Field is never read" warning from Intellisense warning for this field.
            builder.Diagnostics.Suppress( _suppression, loggerField );
        }
    }

    public override dynamic? OverrideMethod()
    {
        meta.This._logger.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}