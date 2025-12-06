// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Linq;

namespace Doc.ProgrammaticInitializer;

internal class AddMethodNamesAspect : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Create an expression that contains the array with all method names.
        var arrayBuilder = new ArrayBuilder( typeof(string) );

        foreach ( var methodName in builder.Target.Methods.Select( m => m.Name ).Distinct() )
        {
            arrayBuilder.Add( ExpressionFactory.Literal( methodName ) );
        }

        // Introduce a field and initialize it to that array.
        builder.IntroduceField(
            "_methodNames",
            typeof(string[]),
            buildField: f => f.InitializerExpression = arrayBuilder.ToExpression() );
    }
}