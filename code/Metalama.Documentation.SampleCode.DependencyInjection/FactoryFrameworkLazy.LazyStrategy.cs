// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.FactoryFrameworkLazy;

// Lazy strategy: stores Func<IServiceFactory<T>> and calls factory.Create() on first access.
[CompileTime]
internal partial class FactoryLazyDependencyInjectionStrategy : DefaultDependencyInjectionStrategy, ITemplateProvider
{
    private readonly INamedType _factoryType;

    public FactoryLazyDependencyInjectionStrategy( DependencyProperties properties ) : base( properties )
    {
        _factoryType = TypeFactory.GetNamedType( typeof(IServiceFactory<>) )
            .WithTypeArguments( properties.DependencyType );
    }

    public override IntroduceDependencyResult IntroduceDependency( IAdviser<INamedType> adviser )
    {
        // Introduce a field that stores Func<IServiceFactory<T>>.
        var funcType = TypeFactory.GetNamedType( typeof(Func<>) ).WithTypeArguments( _factoryType );

        var funcFieldResult = adviser.IntroduceField(
            this.Properties.Name + "Func",
            funcType );

        if ( funcFieldResult.Outcome == AdviceOutcome.Error )
        {
            return IntroduceDependencyResult.Error;
        }

        // Introduce a field that caches the created service.
        var cacheFieldResult = adviser.IntroduceField(
            this.Properties.Name + "Cache",
            this.Properties.DependencyType.ToNullable() );

        if ( cacheFieldResult.Outcome == AdviceOutcome.Error )
        {
            return IntroduceDependencyResult.Error;
        }

        // Introduce the property with a getter that calls Create() on first access.
        var introducePropertyResult = adviser
            .WithTemplateProvider( this )
            .IntroduceProperty(
                this.Properties.Name,
                nameof(GetDependencyTemplate),
                null,
                IntroductionScope.Instance,
                OverrideStrategy.Ignore,
                propertyBuilder =>
                {
                    propertyBuilder.Type = this.Properties.DependencyType;
                    propertyBuilder.Name = this.Properties.Name;
                },
                args: new { cacheField = cacheFieldResult.Declaration, factoryFuncField = funcFieldResult.Declaration } );

        switch ( introducePropertyResult.Outcome )
        {
            case AdviceOutcome.Ignore:
                return IntroduceDependencyResult.Ignore( introducePropertyResult.Declaration );

            case AdviceOutcome.Error:
                return IntroduceDependencyResult.Error;
        }

        var pullStrategy = new InjectionStrategy( this.Properties, introducePropertyResult.Declaration, funcFieldResult.Declaration, _factoryType );

        if ( !this.TryPullDependency( adviser, funcFieldResult.Declaration, pullStrategy ) )
        {
            return IntroduceDependencyResult.Error;
        }

        return IntroduceDependencyResult.Success( introducePropertyResult.Declaration );
    }

    // Template: returns cached value or calls factory.Create() and caches.
    [Template]
    private static dynamic? GetDependencyTemplate( IField cacheField, IField factoryFuncField )
        => cacheField.Value ??= factoryFuncField.Value!.Invoke().Create();

    // Pull strategy that uses Func<IServiceFactory<T>> as the parameter type.
    private sealed class InjectionStrategy : DefaultDependencyPullStrategy
    {
        private readonly IField _funcField;
        private readonly INamedType _funcType;

        protected override IType ParameterType => this._funcType;

        public InjectionStrategy( DependencyProperties properties, IProperty mainProperty, IField funcField, INamedType factoryType )
            : base( properties, mainProperty )
        {
            this._funcField = funcField;
            this._funcType = TypeFactory.GetNamedType( typeof(Func<>) ).WithTypeArguments( factoryType ).ToNullable();
        }

        protected override IFieldOrProperty AssignedFieldOrProperty => this._funcField;
    }
}
