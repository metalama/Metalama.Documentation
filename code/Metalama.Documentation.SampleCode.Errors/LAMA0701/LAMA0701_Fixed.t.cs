using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.RunTime;
using System;
namespace Doc.LAMA0701.Fixed;
public interface IClock
{
  DateTime Now { get; }
}
public partial class InvoiceService
{
  // Fixed: the default framework is registered and handles this dependency.
  [Dependency]
  private IClock _clock;
  public DateTime GetDueDate() => this._clock.Now.AddDays(30);
  public InvoiceService([AspectGenerated] IClock? clock = null)
  {
    this._clock = clock ?? throw new System.ArgumentNullException(nameof(clock));
  }
}