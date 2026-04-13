// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Doc.IntroduceRequiredParameter;

internal class AddTimestampAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var constructor in builder.Target.Constructors )
        {
            builder.With( constructor )
                .IntroduceParameter(
                    "creationTime",
                    typeof(DateTime),
                    pullStrategy: PullStrategy.UseExpression(
                        ExpressionFactory.Parse( "global::System.DateTime.Now" ) ),
                    overloadingStrategy:
                        ConstructorOverloadingStrategy.ForwardSourceConstructors );
        }
    }
}
