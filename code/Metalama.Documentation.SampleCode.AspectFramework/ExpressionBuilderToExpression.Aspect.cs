// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Threading;

namespace Doc.ExpressionBuilderToExpression;

public class AutoIncrementIdAspect : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Introduce the static counter field.
        var counterField = builder.IntroduceField(
            "_nextId",
            typeof(int),
            buildField: f => f.IsStatic = true );

        // Build the initializer expression: Interlocked.Increment(ref _nextId)
        var initializerBuilder = new ExpressionBuilder();
        initializerBuilder.AppendTypeName( typeof(Interlocked) );
        initializerBuilder.AppendVerbatim( ".Increment(ref " );
        initializerBuilder.AppendVerbatim( counterField.Declaration.Name );
        initializerBuilder.AppendVerbatim( ")" );

        // Introduce the instance ID field with the computed initializer.
        builder.IntroduceField(
            "_id",
            typeof(int),
            buildField: f =>
            {
                f.Writeability = Writeability.ConstructorOnly;
                f.InitializerExpression = initializerBuilder.ToExpression();
            } );
    }
}
