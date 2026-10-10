// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0050.Error;

// A low-level aspect, which requires the weaver Doc.LAMA0050.Error.VirtualizeWeaver.
[RequireAspectWeaver( "Doc.LAMA0050.Error.VirtualizeWeaver" )]
internal class VirtualizeAttribute : TypeAspect { }
