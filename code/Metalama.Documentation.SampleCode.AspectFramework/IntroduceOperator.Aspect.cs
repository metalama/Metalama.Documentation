// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceOperator;

internal class AddableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Introduce a binary + operator.
        builder.IntroduceMethod(
            nameof(AddTemplate),
            buildMethod: m =>
            {
                m.OperatorKind = OperatorKind.Addition;
                m.Parameters[0].Type = builder.Target;
                m.Parameters[1].Type = builder.Target;
                m.ReturnType = builder.Target;
            } );

        // Introduce a unary - operator.
        builder.IntroduceMethod(
            nameof(NegateTemplate),
            buildMethod: m =>
            {
                m.OperatorKind = OperatorKind.UnaryNegation;
                m.Parameters[0].Type = builder.Target;
                m.ReturnType = builder.Target;
            } );
    }

    [Template]
    public dynamic? AddTemplate( dynamic? left, dynamic? right )
    {
        return new Vector2D(
            (double) left!.X + (double) right!.X,
            (double) left!.Y + (double) right!.Y );
    }

    [Template]
    public dynamic? NegateTemplate( dynamic? value )
    {
        return new Vector2D(
            -(double) value!.X,
            -(double) value!.Y );
    }
}
