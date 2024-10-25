// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Doc.LogCustomFramework;

public class LoggerDependencyInjectionFramework : DefaultDependencyInjectionFramework
{
    // Returns true if we want to handle this dependency, i.e. if is a dependency of type ILogger.
    public override bool CanHandleDependency(
        DependencyProperties properties,
        in ScopedDiagnosticSink diagnostics )
    {
        return properties.DependencyType.IsConvertibleTo( typeof(ILogger) );
    }

    // Return our own customized strategy.
    protected override DefaultDependencyInjectionStrategy GetStrategy(
        DependencyProperties properties )
    {
        return new InjectionStrategy( properties );
    }

    // Our customized injection strategy. Decides how to create the field or property.
    // We actually have no customization except that we return a customized pull strategy instead of the default one.
    private class InjectionStrategy : DefaultDependencyInjectionStrategy
    {
        public InjectionStrategy( DependencyProperties properties ) : base( properties ) { }

        protected override IPullStrategy GetPullStrategy(
            IFieldOrProperty introducedFieldOrProperty )
        {
            return new LoggerPullStrategy( this.Properties, introducedFieldOrProperty );
        }
    }

    // Our customized pull strategy. Decides how to assign the field or property from the constructor.
    private class LoggerPullStrategy : DefaultPullStrategy
    {
        public LoggerPullStrategy(
            DependencyProperties properties,
            IFieldOrProperty introducedFieldOrProperty ) : base(
            properties,
            introducedFieldOrProperty ) { }

        // Returns the type of the required or created constructor parameter. We return ILogger<T> where T is the declaring type
        // (The default behavior would return just ILogger).
        protected override IType ParameterType
            => ((INamedType) TypeFactory.GetType( typeof(ILogger<>) )).WithTypeArguments(
                this.IntroducedFieldOrProperty.DeclaringType );
    }
}