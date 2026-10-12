// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;
using System.Text;

namespace Doc.LAMA0288.Error;

public class ToStringAttribute : TypeAspect
{
    [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
    public string IntroducedToString()
    {
        var stringBuilder = meta.CompileTime( new StringBuilder() );
        stringBuilder.Append( meta.Target.Type.Name );
        stringBuilder.Append( ": " );

        var properties = meta.Target.Type.Properties.Where( p => !p.IsStatic ).ToList();

        // 'i' is a run-time variable, so 'if ( i > 0 )' is a run-time condition.
        var i = 0;

        foreach ( var property in properties )
        {
            if ( i > 0 )
            {
                // Error: a compile-time method call with side effects under a run-time condition.
                stringBuilder.Append( ", " );
            }

            stringBuilder.Append( property.Name );

            i++;
        }

        return stringBuilder.ToString();
    }
}
