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
public partial class SecureConnection : Connection
{
  public SecureConnection(string connectionString, string certificate, [AspectGenerated] InitializationContext context = default) : base(connectionString, context.Descend(InitializationSlot.OnConstructed))
  {
    if (!context.IsHandled(InitializationSlot.OnConstructed))
    {
      this.OnConstructed(context);
    }
  }
  protected override void OnConstructed(InitializationContext context = default)
  {
    base.OnConstructed(context);
    Console.WriteLine("SecureConnection constructed.");
  }
}