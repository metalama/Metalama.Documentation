// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.ChildAspect;

[Inheritable]
public class AuditedObjectAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        builder.Outbound
            .SelectMany( b => b.Methods )
            .Where( m => m is { Accessibility: Accessibility.Public } )
            .AddAspectIfEligible<AuditedMemberAttribute>();

        builder.Outbound
            .SelectMany( b => b.Properties )
            .Where(
                m => m is { Accessibility: Accessibility.Public, Writeability: Writeability.All } )
            .AddAspectIfEligible<AuditedMemberAttribute>();
    }
}