// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0527.Fixed;

public class IndexerAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceIndexer(
            new[] { ( typeof(int), "index" ) },
            nameof(this.GetItem),
            nameof(this.SetItem),
            // Fixed: the default scope introduces an instance indexer.
            buildIndexer: indexer => { indexer.Accessibility = Accessibility.Public; } );
    }

    [Template]
    public dynamic? GetItem()
    {
        Console.WriteLine( "Getting an item." );

        return meta.Proceed();
    }

    [Template]
    public void SetItem( dynamic? value )
    {
        Console.WriteLine( "Setting an item." );
        meta.Proceed();
    }
}
