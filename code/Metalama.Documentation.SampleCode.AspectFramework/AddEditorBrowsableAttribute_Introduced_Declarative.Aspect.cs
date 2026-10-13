// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.ComponentModel;

namespace Doc.AddEditorBrowsableAttribute_Introduced_Declarative;

public class AddEditorHiddenFieldAttribute : TypeAspect
{
    [Introduce]
    [EditorBrowsable( EditorBrowsableState.Never )]
#pragma warning disable IDE1006 // Naming Styles
    public string? __HiddenField;
#pragma warning restore IDE1006 // Naming Styles
}