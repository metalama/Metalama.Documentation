// This is public domain Metalama sample code.

using Metalama.Extensions.CodeFixes;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using System.IO;
using System.Linq;

namespace Doc.ProjectValidatorWithFix;

internal class MyProjectFabric : ProjectFabric
{
    private static readonly DiagnosticDefinition<IField> _warning = new(
        "MY001",
        Severity.Warning,
        "The field {0} must be private." );

    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectMany(
                p => p.Types.SelectMany(
                    t => t.Fields.Where(
                        f => f.Accessibility != Accessibility.Private
                             && f.Type.IsConvertibleTo( typeof(TextWriter) ) ) ) )
            .ReportDiagnostic(
                f => _warning.WithArguments( f )
                    .WithCodeFixes(
                        CodeFixFactory.ChangeAccessibility( f, Accessibility.Private ) ) );
    }
}