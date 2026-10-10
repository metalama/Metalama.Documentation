namespace Doc.LAMA0517.Fixed;
[Trackable]
internal partial class Order : ITrackable
{
  public int Id { get; set; }
  bool ITrackable.IsChanged { get; set; }
}