namespace Doc.LAMA0517.Fixed;
[Trackable]
internal partial class Order : ITrackable
{
  public int Id { get; set; }
  public bool IsChanged { get; set; }
}