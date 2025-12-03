// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using MyExtension.AspectContracts;
using System;

namespace MyExtension;

public class GreetingAspect : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Get the custom service from the project's service provider.
        var service = builder.Project.ServiceProvider.GetService<IGreetingService>()
            ?? throw new InvalidOperationException( "IGreetingService not found." );

        // Use the service at compile time to generate a greeting.
        var greeting = service.GetGreeting( builder.Target.Name );

        // Introduce a method that prints the greeting at run time.
        builder.IntroduceMethod( nameof( SayHello ), args: new { greeting } );
    }

    [Template]
    public static void SayHello( [CompileTime] string greeting )
    {
        Console.WriteLine( greeting );
    }
}
