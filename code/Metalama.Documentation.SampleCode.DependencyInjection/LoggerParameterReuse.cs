// This is public domain Metalama sample code.

namespace Doc.LoggerParameterReuse;

// The base class has the [Log] aspect, which introduces an ILogger dependency.
public partial class BaseService
{
    [Log]
    public virtual void Serve() { }
}

// The derived class also has the [Log] aspect. Instead of adding a second ILogger parameter,
// the framework reuses the parameter from the base class constructor.
public partial class DerivedService : BaseService
{
    [Log]
    public void Process() { }
}
