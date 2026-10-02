// This is public domain Metalama sample code.

namespace Doc.AddEditorBrowsableAttribute;

[HideFieldsFromEditor]
public class C
{
    public int NormalField;
#pragma warning disable IDE1006 // Naming Styles
    public string? __HiddenField;
#pragma warning restore IDE1006 // Naming Styles
}