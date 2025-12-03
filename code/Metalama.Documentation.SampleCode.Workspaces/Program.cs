// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Workspaces;

if ( args.Length != 1 )
{
    Console.Error.WriteLine( "Usage: ArchitectureVerifier <csproj>" );

    return 1;
}

var workspace = WorkspaceCollection.Default.Load( args[0] );

// Types implementing IFactory must be named *Factory
workspace.SourceCode.Types
    .Where( t => t.Name == "IDocumentFactory" )
    .SelectMany( t => t.GetDerivedTypes() )
    .Where( t => !t.Name.EndsWith( "Factory", StringComparison.Ordinal ) )
    .Report(
        Severity.Warning,
        "ARCH001",
        "Type name must end with Factory because it implements IDocumentFactory." );

// Types implementing IDocument must be in the Documents namespace
workspace.SourceCode.Types
    .Where( t => t.Name == "IDocument" )
    .SelectMany( t => t.GetDerivedTypes() )
    .Where( t => t.ContainingNamespace.FullName != "MyApp.Documents" )
    .Report(
        Severity.Warning,
        "ARCH002",
        "Type must be in the MyApp.Documents namespace because it implements IDocument." );

// Abstractions namespace cannot reference other application namespaces
workspace.SourceCode.Types
    .Where( t => t.ContainingNamespace.FullName == "MyApp.Abstractions" )
    .SelectMany( t => t.GetOutboundReferences() )
    .Where( r =>
    {
        var ns = r.DestinationDeclaration.GetNamespace()?.FullName ?? "";

        return ns.StartsWith( "MyApp", StringComparison.Ordinal )
               && ns != "MyApp.Abstractions";
    } )
    .Report(
        Severity.Warning,
        "ARCH003",
        "Abstractions namespace must not depend on other application namespaces." );

Console.WriteLine(
    $"{DiagnosticReporter.ReportedWarnings + DiagnosticReporter.ReportedErrors} violations found." );

return DiagnosticReporter.ReportedErrors > 0 ? 2
    : DiagnosticReporter.ReportedWarnings > 0 ? 1
    : 0;
