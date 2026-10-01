// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.Tags_Property;

internal class TagsAspect : TypeAspect
{
    [CompileTime]
    private record Tags( int NumberOfProperties, int NumberOfFields );

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.Tags = new Tags(
            builder.Target.Properties.Count,
            builder.Target.Fields.Count );
    }

    [Introduce]
    private void PrintInfo()
    {
        var tags = (Tags) meta.Tags.Source!;

        Console.WriteLine(
            $"This method has {tags.NumberOfFields} fields and {tags.NumberOfProperties} properties." );
    }
}