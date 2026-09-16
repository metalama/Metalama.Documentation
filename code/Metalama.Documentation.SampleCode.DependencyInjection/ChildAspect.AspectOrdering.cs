// This is public domain Metalama sample code.

using Doc.ChildAspect;
using Metalama.Framework.Aspects;

[assembly:
    AspectOrder(
        AspectOrderDirection.CompileTime,
        typeof(AuditedObjectAttribute),
        typeof(AuditedMemberAttribute) )]