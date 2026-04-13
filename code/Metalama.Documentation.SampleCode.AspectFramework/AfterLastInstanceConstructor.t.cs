using System;
using Metalama.Framework.RunTime;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.AfterLastInstanceConstructor;
[NotifyConstructed]
public partial class Connection
{
  public Connection([AspectGenerated] InitializationContext context = default)
  {
    if (!context.IsHandled(InitializationSlot.OnConstructed))
    {
      this.OnConstructed(context);
    }
  }
  public Connection(string connectionString, [AspectGenerated] InitializationContext context = default)
  {
    if (!context.IsHandled(InitializationSlot.OnConstructed))
    {
      this.OnConstructed(context);
    }
  }
  public Connection(string host, int port, [AspectGenerated] InitializationContext context = default) : this($"{host}:{port}", context)
  {
  }
  protected virtual void OnConstructed(InitializationContext context = default)
  {
    Console.WriteLine("Connection constructed.");
  }
}