// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Doc.LAMA0536.Fixed;

public class TimestampPullStrategy : IPullStrategy
{
    // Fixed: the forwarding constructor passes the current time.
    public PullAction GetPullAction( IParameter pulledParameter, IHasParameters targetMember )
        => PullAction.UseExpression( ExpressionFactory.Parse( "global::System.DateTime.Now" ) );
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
