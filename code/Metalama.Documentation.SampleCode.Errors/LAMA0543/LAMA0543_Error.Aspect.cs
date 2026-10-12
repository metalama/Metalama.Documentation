// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA0543.Error;

public class ItemIndexerAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var extensionBlock = builder.With( builder.Target.ExtensionBlocks.Single() );

        extensionBlock.IntroduceIndexer( typeof(int), nameof(this.GetItem), nameof(this.SetItem) );
    }

    [Template]
    public string GetItem( int index )
    {
        Console.WriteLine( "Get." );

        return index.ToString();
    }

    // Error: the set accessor is protected, which an extension member does not allow.
    [Template]
    protected void SetItem( int index, string value )
    {
        Console.WriteLine( "Set." );
    }
}
