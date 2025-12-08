// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;

namespace Doc.FactoryFrameworkLazy;

// A DI framework adapter that handles IFactoredService dependencies
// by pulling the factory from the constructor and calling Create().
[CompileTime]
public class FactoryDependencyInjectionFramework : DefaultDependencyInjectionFramework
{
    // Only handle types that implement IFactoredService.
    public override bool CanHandleDependency( DependencyProperties properties, in ScopedDiagnosticSink diagnostics )
    {
        return properties.DependencyType is INamedType namedType
               && namedType.IsConvertibleTo( typeof(IFactoredService) );
    }

    protected override DefaultDependencyInjectionStrategy GetStrategy( DependencyProperties properties )
        => properties.IsLazy
            ? new FactoryLazyDependencyInjectionStrategy( properties )
            : new FactoryEagerDependencyInjectionStrategy( properties );
}
