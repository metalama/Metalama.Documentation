namespace Doc.LAMA0534.Fixed;
internal interface IDocument
{
  string Title { get; }
}
// Fixed: the [Versioned] aspect is applied to the class that implements the interface.
[Versioned]
internal partial class Document : IDocument
{
  public string Title { get; set; } = "";
  private int _version;
}