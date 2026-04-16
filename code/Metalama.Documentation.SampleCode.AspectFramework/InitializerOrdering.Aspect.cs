// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

[assembly: AspectOrder(
    AspectOrderDirection.RunTime,
    typeof(Doc.InitializerOrdering.AspectAAttribute),
    typeof(Doc.InitializerOrdering.AspectBAttribute) )]

namespace Doc.InitializerOrdering;

[Inheritable]
public class AspectAAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.Before),
            InitializerKind.AfterLastInstanceConstructor,
            InitializerPosition.BeforeBase );

        builder.AddInitializer(
            nameof(this.After),
            InitializerKind.AfterLastInstanceConstructor,
            InitializerPosition.AfterBase );
    }

    [Template]
    private void Before()
        => Console.WriteLine( $"AspectA before in {meta.Target.Type.Name}" );

    [Template]
    private void After()
        => Console.WriteLine( $"AspectA after in {meta.Target.Type.Name}" );
}

[Inheritable]
public class AspectBAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.Before),
            InitializerKind.AfterLastInstanceConstructor,
            InitializerPosition.BeforeBase );

        builder.AddInitializer(
            nameof(this.After),
            InitializerKind.AfterLastInstanceConstructor,
            InitializerPosition.AfterBase );
    }

    [Template]
    private void Before()
        => Console.WriteLine( $"AspectB before in {meta.Target.Type.Name}" );

    [Template]
    private void After()
        => Console.WriteLine( $"AspectB after in {meta.Target.Type.Name}" );
}
