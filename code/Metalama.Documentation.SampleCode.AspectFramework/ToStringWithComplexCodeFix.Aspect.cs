// This is public domain Metalama sample code.

using Metalama.Extensions.CodeFixes;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Doc.ToStringWithComplexCodeFix;

[AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
[RunTimeOrCompileTime]
public class NotToStringAttribute : Attribute { }

[EditorExperience( SuggestAsLiveTemplate = true )]
public class ToStringAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        // Suggest to switch to manual implementation.
        if ( builder.AspectInstance.Predecessors[0].Instance is IAttribute attribute )
        {
            builder.Diagnostics.Suggest(
                new CodeFix(
                    "Switch to manual implementation",
                    codeFixBuilder => this.ImplementManually( codeFixBuilder, builder.Target ) ),
                attribute );
        }
    }

    [CompileTime]
    private async Task ImplementManually( ICodeActionBuilder builder, INamedType targetType )
    {
        await builder.ApplyAspectAsync( targetType, this );
        await builder.RemoveAttributesAsync( targetType, typeof(ToStringAttribute) );
        await builder.RemoveAttributesAsync( targetType, typeof(NotToStringAttribute) );
    }

    [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
    public string IntroducedToString()
    {
        var stringBuilder = new InterpolatedStringBuilder();
        stringBuilder.AddText( "{ " );
        stringBuilder.AddText( meta.Target.Type.Name );
        stringBuilder.AddText( " " );

        var fields = meta.Target.Type.FieldsAndProperties
            .Where( f => f is { IsStatic: false, IsImplicitlyDeclared: false } )
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
            stringBuilder.AddExpression( field.Value );

            i++;
        }

        stringBuilder.AddText( " }" );

        return stringBuilder.ToValue();
    }
}