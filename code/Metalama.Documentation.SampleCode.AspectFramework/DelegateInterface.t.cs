namespace Doc.DelegateInterface;
public interface IGreeter
{
  string Name { get; set; }
  string Greet(string message);
}
internal class InternalGreeter : IGreeter
{
  public string Name { get; set; } = "World";
  public string Greet(string message) => $"{message}, {this.Name}!";
}
// The aspect implements IGreeter by delegating to the _greeter field.
[DelegateInterface(typeof(IGreeter), "_greeter")]
public partial class MyService : IGreeter
{
  private readonly IGreeter _greeter = new InternalGreeter();
  string IGreeter.Name
  {
    get
    {
      return _greeter.Name;
    }
    set
    {
      _greeter.Name = value;
    }
  }
  string IGreeter.Greet(string message)
  {
    return _greeter.Greet(message);
  }
}
