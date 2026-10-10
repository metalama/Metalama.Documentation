namespace Doc.LAMA0506.Fixed;
// Fixed: the class no longer declares its own Version field, so the aspect can introduce the property.
[Versioned]
public partial class Document
{
  public string? Title { get; set; }
  public int Version { get; set; }
}