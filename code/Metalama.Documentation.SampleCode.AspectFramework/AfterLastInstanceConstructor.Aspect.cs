// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.AfterLastInstanceConstructor;

[Inheritable]
public class NotifyConstructedAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.OnConstructed),
            InitializerKind.AfterLastInstanceConstructor );
    }

    [Template]
    private void OnConstructed()
    {
        Console.WriteLine( $"{meta.Target.Type.Name} constructed." );
    }
}
