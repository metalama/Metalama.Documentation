// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.IntroduceId;

internal class IntroduceIdAttribute : TypeAspect
{
    [Introduce]
    public Guid Id { get; } = Guid.NewGuid();
}