// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.ComponentModel;
using System.Linq;

#pragma warning disable CA1310 // Specify StringComparison for correctness

namespace Doc.AddEditorBrowsableAttribute;

public class HideFieldsFromEditorAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Construct a custom attribute.
        var attribute = AttributeConstruction.Create(
            typeof(EditorBrowsableAttribute),
            new object[] { EditorBrowsableState.Never } );

        // Add a copy of it to each field whose name starts with a double underscore.
        foreach ( var field in builder.Target.Fields
                     .Where( f => f.Name.StartsWith( "__" ) && !f.IsImplicitlyDeclared ) )
        {
            builder.With( field ).IntroduceAttribute( attribute );
        }
    }
}