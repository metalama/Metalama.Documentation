// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.InsertParameter;

internal class IntroduceGreetAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod(
            nameof(Template),
            buildMethod: introduced =>
            {
                introduced.Name = "Greet";

                // Insert parameters at the beginning, before the template's
                // own 'greeting' parameter.
                introduced.InsertParameter( 0, "firstName", typeof(string) );
                introduced.InsertParameter( 1, "lastName", typeof(string) );
            } );
    }

    [Template]
    public void Template( string greeting = "Hello" )
    {
        Console.WriteLine( greeting );
    }
}
