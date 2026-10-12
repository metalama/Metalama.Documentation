// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System.Linq;

namespace Doc.ReportError;

internal class LogAttribute : OverrideMethodAspect
{
    // You MUST have a static field that defines the diagnostic.
    private static readonly DiagnosticDefinition<INamedType> _error = new(
        "MY001",
        Severity.Error,
        "The type {0} must have a field named '_logger'." );

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        // Validation must be done in BuildAspect. In OverrideMethod, it's too late.
        if ( !builder.Target.DeclaringType.Fields.OfName( "_logger" ).Any() )
        {
            builder.Diagnostics.Report( _error.WithArguments( builder.Target.DeclaringType ) );
        }
    }

    public override dynamic? OverrideMethod()
    {
        meta.This._logger.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}