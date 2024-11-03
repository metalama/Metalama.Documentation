// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.Trim;

internal class TrimAttribute : ContractAspect
{
    public override void Validate( dynamic? value )
    {
        value = value?.Trim();
    }
}