// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0050.Fixed;

// A low-level aspect, which requires the weaver Doc.LAMA0050.Fixed.VirtualizeWeaver.
[RequireAspectWeaver( "Doc.LAMA0050.Fixed.VirtualizeWeaver" )]
internal class VirtualizeAttribute : TypeAspect { }
