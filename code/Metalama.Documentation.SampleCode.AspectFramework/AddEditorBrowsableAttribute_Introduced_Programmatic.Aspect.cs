// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.ComponentModel;

namespace Doc.AddEditorBrowsableAttribute_Introduced_Programmatic;

public class AddEditorHiddenFieldAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var attribute = AttributeConstruction.Create(
            typeof(EditorBrowsableAttribute),
            new object[] { EditorBrowsableState.Never } );

        builder.IntroduceField(
            "__HiddenField",
            typeof(int),
            buildField: f => f.AddAttribute( attribute ) );
    }
}