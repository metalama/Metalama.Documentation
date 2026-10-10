// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;
using System.Text;

namespace Doc.LAMA0288.Fixed;

public class ToStringAttribute : TypeAspect
{
    [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
    public string IntroducedToString()
    {
        var stringBuilder = meta.CompileTime( new StringBuilder() );
        stringBuilder.Append( meta.Target.Type.Name );
        stringBuilder.Append( ": " );

        var properties = meta.Target.Type.Properties.Where( p => !p.IsStatic ).ToList();

        // Fixed: 'i' is a compile-time variable, so 'if ( i > 0 )' is a compile-time condition.
        var i = meta.CompileTime( 0 );

        foreach ( var property in properties )
        {
            if ( i > 0 )
            {
                stringBuilder.Append( ", " );
            }

            stringBuilder.Append( property.Name );

            i++;
        }

        return stringBuilder.ToString();
    }
}
