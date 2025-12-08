// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.FactoryFramework;

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
        var templateArgs = new TemplateArgs();

        // Introduce the visible property with a getter that calls _factoryFunc().Create().
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
                args: new { args = templateArgs } );

        switch ( introducePropertyResult.Outcome )
        {
            case AdviceOutcome.Ignore:
                return IntroduceDependencyResult.Ignore( introducePropertyResult.Declaration );

            case AdviceOutcome.Error:
                return IntroduceDependencyResult.Error;
        }

        if ( !this.TryAddFields( adviser, introducePropertyResult.Declaration, templateArgs ) )
        {
            return IntroduceDependencyResult.Error;
        }

        return IntroduceDependencyResult.Success( introducePropertyResult.Declaration );
    }

    private bool TryAddFields( IAdviser<INamedType> adviser, IProperty property, TemplateArgs templateArgs )
    {
        // Introduce a field that stores the Func<IServiceFactory<T>>
        var funcType = ((INamedType) TypeFactory.GetType( typeof(Func<>) )).WithTypeArguments( _factoryType );

        var introduceFuncFieldResult = adviser.IntroduceField(
            property.Name + "Func",
            funcType );

        if ( introduceFuncFieldResult.Outcome == AdviceOutcome.Error )
        {
            return false;
        }

        templateArgs.FactoryFuncField = introduceFuncFieldResult.Declaration;

        // Introduce a field that caches the created service
        var introduceCacheFieldResult = adviser.IntroduceField(
            property.Name + "Cache",
            property.Type.ToNullable() );

        if ( introduceCacheFieldResult.Outcome == AdviceOutcome.Error )
        {
            return false;
        }

        templateArgs.CacheField = introduceCacheFieldResult.Declaration;

        var pullStrategy = new InjectionStrategy( this.Properties, property, introduceFuncFieldResult.Declaration, _factoryType );

        return this.TryPullDependency( adviser, templateArgs.FactoryFuncField, pullStrategy );
    }

    public class TemplateArgs
    {
        public IField? CacheField { get; set; }

        public IField? FactoryFuncField { get; set; }
    }

    // Template: returns cached value or calls factory.Create() and caches.
    [Template]
    private static dynamic? GetDependencyTemplate( TemplateArgs args )
        => args.CacheField!.Value ??= args.FactoryFuncField!.Value!.Invoke().Create();

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
            this._funcType = ((INamedType) TypeFactory.GetType( typeof(Func<>) )).WithTypeArguments( factoryType ).ToNullable();
        }

        protected override IFieldOrProperty AssignedFieldOrProperty => this._funcField;
    }
}
