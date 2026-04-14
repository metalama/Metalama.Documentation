using Metalama.Framework.RunTime;
using Microsoft.Extensions.Logging;
namespace Doc.LoggerParameterReuse;
// The base class has the [Log] aspect, which introduces an ILogger dependency.
public partial class BaseService
{
  [Log]
  public virtual void Serve()
  {
    _logger.LogWarning("BaseService.Serve() called.");
  }
  private ILogger _logger;
  public BaseService([AspectGenerated] ILogger<BaseService> logger = null)
  {
    this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
  }
}
// The derived class also has the [Log] aspect. Instead of adding a second ILogger parameter,
// the framework reuses the parameter from the base class constructor.
public partial class DerivedService : BaseService
{
  [Log]
  public void Process()
  {
    _logger.LogWarning("DerivedService.Process() called.");
  }
  private ILogger _logger;
  public DerivedService([AspectGenerated] ILogger<DerivedService> logger = null) : base(logger)
  {
    this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
  }
}