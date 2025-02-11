// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.Architecture.RequireDefaultConstructorFabric;

// Reusable implementation of the architecture rule.
[CompileTime]
internal static class ArchitectureExtensions
{
    private static readonly DiagnosticDefinition<INamedType> _warning = new(
        "MY001",
        Severity.Warning,
        "The type '{0}' must have a public default constructor." );

    public static void MustHaveDefaultConstructor( this IQuery<INamedType> verifier )
    {
        verifier
            .Where(
                t => !t.IsStatic
                     && t.Constructors.FirstOrDefault( c => c.Parameters.Count == 0 ) is null or
                         { Accessibility: not Accessibility.Public } )
            .ReportDiagnostic( t => _warning.WithArguments( t ) );
    }
}

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Using the reusable MustHaveDefaultConstructor rule.
        // Note that we only apply the rule to public types. 
        amender.SelectTypes()
            .Where( t => t.Accessibility == Accessibility.Public )
            .MustHaveDefaultConstructor();
    }
}

// This class has an implicit default constructor.
public class ValidClass1 { }

// This class has an explicit default constructor.
public class ValidClass2
{
    public ValidClass2() { }
}

// This class does not havr any default constructor.
public class InvalidClass
{
    public InvalidClass( int x ) { }
}