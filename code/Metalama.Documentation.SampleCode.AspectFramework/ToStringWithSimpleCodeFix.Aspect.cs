// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.CodeFixes;
using System;
using System.Linq;

namespace Doc.ToStringWithSimpleToString;

[AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
public class NotToStringAttribute : Attribute { }

[EditorExperience( SuggestAsLiveTemplate = true )]
public class ToStringAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        // For each field, suggest a code fix to remove from ToString.
        foreach ( var field in builder.Target.FieldsAndProperties.Where(
                     f => f is { IsStatic: false, IsImplicitlyDeclared: false } ) )
        {
            if ( !field.Attributes.Any(
                    a => a.Type.IsConvertibleTo( typeof(NotToStringAttribute) ) ) )
            {
                builder.Diagnostics.Suggest(
                    CodeFixFactory.AddAttribute(
                        field,
                        typeof(NotToStringAttribute),
                        "Exclude from [ToString]" ),
                    field );
            }
        }
    }

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
            if ( field.Attributes.Any(
                    a => a.Type.IsConvertibleTo( typeof(NotToStringAttribute) ) ) )
            {
                continue;
            }

            if ( i > 0 )
            {
                stringBuilder.AddText( ", " );
            }

            stringBuilder.AddText( field.Name );
            stringBuilder.AddText( "=" );
            stringBuilder.AddExpression( field );

            i++;
        }

        stringBuilder.AddText( " }" );

        return stringBuilder.ToValue();
    }
}