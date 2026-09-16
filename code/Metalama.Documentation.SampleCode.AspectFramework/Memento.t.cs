namespace Doc.Memento;
[Memento]
public class Vehicle
{
  public string Name { get; }
  public decimal Payload { get; set; }
  public string Fuel { get; set; }
  public void Restore(object snapshot)
  {
    Payload = ((Snapshot)snapshot).Payload;
    Fuel = ((Snapshot)snapshot).Fuel;
  }
  public object Save()
  {
    return new Snapshot(Payload, Fuel);
  }
  private class Snapshot
  {
    public readonly string Fuel;
    public readonly decimal Payload;
    public Snapshot(decimal Payload, string Fuel)
    {
      this.Payload = Payload;
      this.Fuel = Fuel;
    }
  }
}