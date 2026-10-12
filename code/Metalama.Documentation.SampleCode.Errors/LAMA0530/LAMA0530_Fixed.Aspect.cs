// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.LAMA0530.Fixed;

[AttributeUsage( AttributeTargets.Field )]
public class DependencyAttribute : Attribute { }

public class InjectDependenciesAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var fields = builder.Target.Fields
            .Where( f => f.Attributes.OfAttributeType( typeof(DependencyAttribute) ).Any() )
            .ToList();

        foreach ( var constructor in builder.Target.Constructors )
        {
            foreach ( var field in fields )
            {
                // Fixed: the parameter name is derived from the field name, which is unique in the type.
                var parameterName = field.Name.TrimStart( '_' );

                builder.With( constructor ).IntroduceParameter( parameterName, field.Type, TypedConstant.Default( field.Type ) );

                builder.With( constructor )
                    .AddInitializer( StatementFactory.Parse( $"this.{field.Name} = {parameterName};" ) );
            }
        }
    }
}
