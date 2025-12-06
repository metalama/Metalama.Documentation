// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Doc.IntroduceParameter;

internal class RegisterInstanceAttribute : ConstructorAspect
{
    public override void BuildAspect( IAspectBuilder<IConstructor> builder )
    {
        builder.IntroduceParameter(
            "instanceRegistry",
            TypeFactory.GetNamedType( typeof(IInstanceRegistry) ).ToNullable(),
            TypedConstant.Default( typeof(IInstanceRegistry) ),
            pullStrategy: PullStrategy.IntroduceParameterAndPull(
                "instanceRegistry",
                TypeFactory.GetType( typeof(IInstanceRegistry) ),
                TypedConstant.Default( typeof(IInstanceRegistry) ) ) );

        builder.AddInitializer( StatementFactory.Parse( "instanceRegistry.Register( this );" ) );
    }
}

public interface IInstanceRegistry
{
    void Register( object instance );
}