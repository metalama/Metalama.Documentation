// This is public domain Metalama sample code.

using Doc.ValidateAfterAllAspects;
using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System.IO;
using System.Linq;

// Note that aspects are applied in inverse order than they appear in the next line.
[assembly:
    AspectOrder( AspectOrderDirection.RunTime, typeof(AddLoggerAttribute), typeof(LogAttribute) )]

namespace Doc.ValidateAfterAllAspects;

internal class LogAttribute : OverrideMethodAspect
{
    private static readonly DiagnosticDefinition<INamedType> _error = new(
        "MY001",
        Severity.Error,
        "The type {0} must have a field named _logger." );

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Outbound
            .Select( m => m.DeclaringType )
            .Validate( 
                this.ValidateDeclaringType, 
                new DeclarationValidationOptions() { Time = DeclarationValidationTime.AfterAllAspects } );
    }

    private void ValidateDeclaringType( DeclarationValidationContext context )
    {
        var type = (INamedType) context.Declaration;

        if ( !type.AllFields.OfName( "_logger" ).Any() )
        {
            context.Diagnostics.Report( _error.WithArguments( type ) );
        }
    }

    public override dynamic? OverrideMethod()
    {
        meta.This._logger.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}

internal class AddLoggerAttribute : TypeAspect
{
    [Introduce]
    private TextWriter _logger = File.CreateText( "log.txt" );
}