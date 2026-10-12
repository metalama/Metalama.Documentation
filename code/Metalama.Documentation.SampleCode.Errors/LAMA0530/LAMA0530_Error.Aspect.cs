// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.LAMA0530.Error;

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
                // Error: the parameter name is derived from the field type, so two fields of the same type
                // produce two parameters with the same name.
                var typeName = ((INamedType) field.Type).Name;
                var parameterName = char.ToLowerInvariant( typeName[0] ) + typeName.Substring( 1 );

                builder.With( constructor ).IntroduceParameter( parameterName, field.Type, TypedConstant.Default( field.Type ) );

                builder.With( constructor )
                    .AddInitializer( StatementFactory.Parse( $"this.{field.Name} = {parameterName};" ) );
            }
        }
    }
}
