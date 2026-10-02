// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System.Linq;

namespace Doc.Architecture.RequireDefaultConstructorAspect;

[Inheritable]
public class RequireDefaultConstructorAttribute : TypeAspect
{
    private static readonly DiagnosticDefinition<INamedType> _warning = new(
        "MY001",
        Severity.Warning,
        "The type '{0}' must have a public default constructor." );

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        if ( builder.Target.IsAbstract )
        {
            return;
        }

        var defaultConstructor =
            builder.Target.Constructors.SingleOrDefault( c => c.Parameters.Count == 0 );

        if ( defaultConstructor == null
             || defaultConstructor.Accessibility != Accessibility.Public )
        {
            builder.Diagnostics.Report( _warning.WithArguments( builder.Target ) );
        }
    }
}