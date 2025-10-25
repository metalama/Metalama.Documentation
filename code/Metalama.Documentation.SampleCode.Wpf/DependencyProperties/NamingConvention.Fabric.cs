// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Wpf.Configuration;

namespace Doc.DependencyProperties.NamingConvention;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyProperty( builder =>
                                                 builder.AddNamingConvention(
                                                     new DependencyPropertyNamingConvention(
                                                         "czech" )
                                                     {
                                                         ValidatePattern =
                                                             "Kontrolovat{PropertyName}"
                                                     } ) );
    }
}