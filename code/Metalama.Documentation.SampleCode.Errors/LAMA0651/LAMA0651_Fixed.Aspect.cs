// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA0651.Fixed;

public class CustomHashCodeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.GetHashCodeTemplate), whenExists: OverrideStrategy.Override );
    }

    [Template( Name = "GetHashCode" )]
    public int GetHashCodeTemplate()
    {
        Console.WriteLine( $"Computing the hash code of {meta.Target.Type.Name}." );

        // Fixed: the template computes the hash code from the properties instead of calling meta.Proceed().
        var hashCode = new HashCode();

        foreach ( var property in meta.Target.Type.Properties.Where( p => !p.IsStatic && !p.IsImplicitlyDeclared ) )
        {
            hashCode.Add( property.Value );
        }

        return hashCode.ToHashCode();
    }
}
