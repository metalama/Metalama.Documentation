// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Wpf.Configuration;

namespace Doc.Command.CanExecute_Czech;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureCommand(
            builder =>
            {
                builder.AddNamingConvention(
                    new CommandNamingConvention( "czech-1" )
                    {
                        CommandNamePattern = "^Vykonat(.*)$",
                        CanExecutePatterns = ["MůžemeVykonat{CommandName}"],
                        CommandPropertyName = "{CommandName}Příkaz"
                    } );

                builder.AddNamingConvention(
                    new CommandNamingConvention( "czech-2" )
                    {
                        CanExecutePatterns =
                            ["Můžeme{CommandName}"],
                        CommandPropertyName = "{CommandName}Příkaz"
                    } );
            } );
    }
}