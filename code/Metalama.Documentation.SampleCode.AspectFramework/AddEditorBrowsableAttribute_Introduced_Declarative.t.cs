using System.ComponentModel;
namespace Doc.AddEditorBrowsableAttribute_Introduced_Declarative;
[AddEditorHiddenFieldAttribute]
public class C
{
  public int NormalField;
  [EditorBrowsable(EditorBrowsableState.Never)]
  public string? __HiddenField;
}