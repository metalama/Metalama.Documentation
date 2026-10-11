// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0536.Error;

public class TimestampPullStrategy : IPullStrategy
{
    // Error: PullAction.None gives no value for the forwarding constructor to pass.
    public PullAction GetPullAction( IParameter pulledParameter, IHasParameters targetMember ) => PullAction.None;
}

public class AddTimestampAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var constructor in builder.Target.Constructors )
        {
            builder.With( constructor )
                .IntroduceParameter(
                    "creationTime",
                    typeof(DateTime),
                    pullStrategy: new TimestampPullStrategy(),
                    overloadingStrategy: ConstructorOverloadingStrategy.ForwardSourceConstructors );
        }
    }
}
