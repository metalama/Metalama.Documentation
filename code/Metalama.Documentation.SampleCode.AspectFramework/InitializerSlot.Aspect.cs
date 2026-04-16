// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.RunTime.Initialization;
using System.Linq;

[assembly: AspectOrder(
    AspectOrderDirection.RunTime,
    typeof(Doc.InitializerSlot.PublishAttribute),
    typeof(Doc.InitializerSlot.ValidateAttribute) )]

namespace Doc.InitializerSlot;

[Inheritable]
public class ValidateAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var slotField = TypeFactory.GetNamedType( typeof(InitializerSlots) )
            .Fields.OfName( nameof(InitializerSlots.Validate) ).Single();

        builder.AddInitializer(
            nameof(this.Template),
            InitializerKind.AfterObjectInitializer,
            slotFields: new[] { slotField } );
    }

    [Template]
    private void Template( InitializationContext context )
    {
        if ( !context.IsHandled( InitializerSlots.Validate ) )
        {
            ValidationService.Validate( meta.This );
        }
    }
}

[Inheritable]
public class PublishAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var slotField = TypeFactory.GetNamedType( typeof(InitializerSlots) )
            .Fields.OfName( nameof(InitializerSlots.Publish) ).Single();

        builder.AddInitializer(
            nameof(this.Template),
            InitializerKind.AfterObjectInitializer,
            slotFields: new[] { slotField } );
    }

    [Template]
    private void Template( InitializationContext context )
    {
        if ( !context.IsHandled( InitializerSlots.Publish ) )
        {
            PublishService.Publish( meta.This );
        }
    }
}
