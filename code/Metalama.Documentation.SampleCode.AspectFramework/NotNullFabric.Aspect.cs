// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.NotNullFabric;

internal class NotNullAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        foreach ( var parameter in
                 builder.Target.Parameters.Where( p => p.RefKind is RefKind.None or RefKind.In
                                                       && p.Type.IsNullable != true
                                                       && p.Type.IsReferenceType == true ) )
        {
            builder.With( parameter )
                .AddContract( nameof(this.Validate), args: new { parameterName = parameter.Name } );
        }
    }

    [Template]
    private void Validate( dynamic? value, [CompileTime] string parameterName )
    {
        if ( value == null )
        {
            throw new ArgumentNullException( parameterName );
        }
    }
}

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectMany( a => a.Types
                                .Where( t => t.Accessibility == Accessibility.Public )
                                .SelectMany( t => t.Methods )
                                .Where( m => m.Accessibility == Accessibility.Public ) )
            .AddAspect<NotNullAttribute>();
    }
}