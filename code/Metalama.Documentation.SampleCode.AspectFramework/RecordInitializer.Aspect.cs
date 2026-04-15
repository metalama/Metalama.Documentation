// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.RecordInitializer;

public class LogConstructionAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.BeforeConstructor),
            InitializerKind.BeforeInstanceConstructor );
    }

    [Template]
    private void BeforeConstructor()
    {
        Console.WriteLine( $"Constructing {meta.Target.Type.Name}." );
    }
}
