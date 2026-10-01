// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.BeforeTypeConstructor;

public class RegisterMessageHandlerAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var selfType = builder.Target.MakeGenericInstance(
            builder.Target.TypeParameters.ToArray<IType>() );

        builder.AddInitializer(
            nameof(this.RegisterHandler),
            InitializerKind.BeforeTypeConstructor,
            args: new
            {
                TSelf = selfType,
                TMessage = builder.Target.TypeParameters[0]
            } );
    }

    [Template]
    private static void RegisterHandler<[CompileTime] TSelf, [CompileTime] TMessage>()
        where TSelf : new()
        where TMessage : IMessage
    {
        MessageRouter.Register<TSelf, TMessage>();
    }
}
