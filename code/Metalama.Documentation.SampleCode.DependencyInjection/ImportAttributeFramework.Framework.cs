// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using Metalama.Framework.Diagnostics;

namespace Doc.ImportAttributeFramework;

// A DI framework adapter that adds [Import] attributes to properties
// instead of pulling dependencies from constructor parameters.
[CompileTime]
public class ImportAttributeFramework : IDependencyInjectionFramework
{
    public bool CanHandleDependency( DependencyProperties properties, in ScopedDiagnosticSink diagnostics )
    {
        // Handle all non-static dependencies.
        return !properties.IsStatic;
    }

    // Called when the programmatic IntroduceDependency adviser extension method is used.
    public IntroduceDependencyResult IntroduceDependency( DependencyProperties properties, IAdviser<INamedType> adviser )
    {
        var importType = (INamedType) TypeFactory.GetType( typeof(ImportAttribute) );

        // Introduce the property with the [Import] attribute.
        var result = adviser.IntroduceAutomaticProperty(
            properties.Name,
            properties.DependencyType,
            IntroductionScope.Instance,
            OverrideStrategy.Ignore,
            b =>
            {
                b.Accessibility = Accessibility.Public;
                b.AddAttribute( AttributeConstruction.Create( importType ) );
            } );

        if ( result.Outcome != AdviceOutcome.Default )
        {
            return IntroduceDependencyResult.Ignore( result.Declaration );
        }

        return IntroduceDependencyResult.Success( result.Declaration );
    }

    // Called for the declarative scenario, i.e., when [Dependency] is applied to an existing field or property.
    public bool TryImplementDependency( DependencyProperties properties, IAdviser<IFieldOrProperty> adviser )
    {
        // Add [Import] via the adviser.
        var importType = (INamedType) TypeFactory.GetType( typeof(ImportAttribute) );

        if ( !adviser.Target.Attributes.Any( importType ) )
        {
            adviser.IntroduceAttribute( AttributeConstruction.Create( importType ) );
        }

        return true;
    }
}
