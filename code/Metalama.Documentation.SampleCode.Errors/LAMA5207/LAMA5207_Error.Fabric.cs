// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Wpf.Configuration;

namespace Doc.LAMA5207.Error;

internal class Fabric : NamespaceFabric
{
    public override void AmendNamespace( INamespaceAmender amender )
    {
        amender.ConfigureCommand(
            b =>
            {
                b.RemoveNamingConvention( CommandOptionsBuilder.DefaultNamingConventionName );

                // Only methods named RunXxx are recognized as commands.
                b.AddNamingConvention( new CommandNamingConvention( "run" ) { CommandNamePattern = "^Run(?<CommandName>.+)$" } );
            } );
    }
}
