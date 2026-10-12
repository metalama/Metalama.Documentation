// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.BuildInfo;

internal partial class BuildInfo
{
    private class Fabric : TypeFabric
    {
        [Introduce]
        public string? TargetFramework { get; } = meta.Target.Project.TargetFramework;

        [Introduce]
        public string? Configuration { get; } = meta.Target.Project.Configuration;
    }
}