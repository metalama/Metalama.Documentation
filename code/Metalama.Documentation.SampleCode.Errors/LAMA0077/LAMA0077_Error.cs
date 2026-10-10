// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The message contains the generated names of the test assemblies, which change at every run.
// @RemoveDiagnosticMessage
#endif

using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;
using System.Threading.Tasks;

namespace Doc.LAMA0077.Error;

// Error: this project defines a second weaver with the same full name as the weaver of the referenced project.
// The #if directive keeps this sample compilable as plain C#, where both files share one assembly.
#if TESTRUNNER
[MetalamaPlugIn]
public class VirtualizeWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context ) => Task.CompletedTask;
}
#endif

[Virtualize]
internal class Calculator
{
    public int Add( int a, int b ) => a + b;
}
