// This is public domain Metalama sample code.

using Metalama.Extensions.Metrics;
using Metalama.Framework.Metrics;
using Metalama.Framework.Workspaces;

if ( args.Length != 1 )
{
    Console.Error.WriteLine( "Usage: MetricsAnalyzer <csproj>" );

    return 1;
}

#region RegisterMetricProviders
// Register metric providers before loading the workspace.
WorkspaceCollection.Default.ServiceBuilder.AddMetrics();
#endregion

#region LoadWorkspace
// Load the workspace.
var workspace = WorkspaceCollection.Default.Load( args[0] );
#endregion

#region QueryMetrics
// Query methods and their metrics.
var complexMethods = workspace.SourceCode.Types
    .SelectMany( t => t.Methods )
    .Where( m => !m.IsAbstract )
    .Select( m => new
    {
        Method = m,
        SyntaxNodes = m.Metrics().Get<SyntaxNodesCount>().Value,
        Statements = m.Metrics().Get<StatementsCount>().Value
    } )
    .Where( m => m.SyntaxNodes > 20 )
    .OrderByDescending( m => m.SyntaxNodes );
#endregion

Console.WriteLine( "Complex methods (> 20 syntax nodes):" );
Console.WriteLine();

foreach ( var item in complexMethods )
{
    Console.WriteLine(
        $"  {item.Method.DeclaringType.Name}.{item.Method.Name}: " +
        $"{item.SyntaxNodes} nodes, {item.Statements} statements" );
}

return 0;
