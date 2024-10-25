// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Linq;

namespace Doc.ToString;

internal class ToStringAttribute : TypeAspect
{
    [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
    public string IntroducedToString()
    {
        var stringBuilder = new InterpolatedStringBuilder();
        stringBuilder.AddText( "{ " );
        stringBuilder.AddText( meta.Target.Type.Name );
        stringBuilder.AddText( " " );

        var fields = meta.Target.Type.FieldsAndProperties
            .Where( f => f is { IsImplicitlyDeclared: false, IsStatic: false } )
            .ToList();

        var i = meta.CompileTime( 0 );

        foreach ( var field in fields )
        {
            if ( i > 0 )
            {
                stringBuilder.AddText( ", " );
            }

            stringBuilder.AddText( field.Name );
            stringBuilder.AddText( "=" );
            stringBuilder.AddExpression( field.Value );

            i++;
        }

        stringBuilder.AddText( " }" );

        return stringBuilder.ToValue();
    }
}