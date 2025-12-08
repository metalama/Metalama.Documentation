// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Doc.FactoryFramework;

// Eager strategy: calls factory.Create() immediately in the constructor.
[CompileTime]
internal class FactoryEagerDependencyInjectionStrategy : DefaultDependencyInjectionStrategy
{
    public FactoryEagerDependencyInjectionStrategy( DependencyProperties properties ) : base( properties ) { }

    protected override IDependencyPullStrategy GetDependencyPullStrategy( IFieldOrProperty introducedFieldOrProperty )
        => new FactoryEagerPullStrategy( this.Properties, introducedFieldOrProperty );
}

// Pull strategy for eager initialization: pulls IServiceFactory<T> and calls Create() in constructor.
[CompileTime]
internal class FactoryEagerPullStrategy : DefaultDependencyPullStrategy
{
    private readonly INamedType _factoryType;
    private readonly IFieldOrProperty _fieldOrProperty;

    public FactoryEagerPullStrategy( DependencyProperties properties, IFieldOrProperty fieldOrProperty )
        : base( properties, fieldOrProperty )
    {
        _fieldOrProperty = fieldOrProperty;
        _factoryType = TypeFactory.GetNamedType( typeof(IServiceFactory<>) )
            .WithTypeArguments( properties.DependencyType );
    }

    // Use IServiceFactory<T> as the parameter type instead of T.
    protected override IType ParameterType => _factoryType;

    // Call factory.Create() in constructor.
    public override IStatement GetAssignmentStatement( IParameter existingParameter )
    {
        var assignmentCode = $"this.{_fieldOrProperty.Name} = {existingParameter.Name}.Create();";

        return StatementFactory.Parse( assignmentCode );
    }
}
