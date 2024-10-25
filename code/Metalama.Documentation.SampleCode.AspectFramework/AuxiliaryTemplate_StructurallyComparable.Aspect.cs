// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Doc.StructurallyComparable;

public class StructuralEquatableAttribute : TypeAspect
{
    [Introduce( Name = nameof(Equals), WhenExists = OverrideStrategy.Override )]
    public bool EqualsImpl( object? other )
    {
        foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties.Where(
                     t => t is { IsAutoPropertyOrField: true, IsImplicitlyDeclared: false } ) )
        {
            meta.InvokeTemplate(
                nameof(this.CompareFieldOrProperty),
                args: new
                {
                    TFieldOrProperty = fieldOrProperty.Type,
                    fieldOrProperty,
                    other = ExpressionFactory.Capture( other! )
                } );
        }

        return true;
    }

    [Template]
    private void CompareFieldOrProperty<[CompileTime] TFieldOrProperty>(
        IFieldOrProperty fieldOrProperty,
        IExpression other )
    {
        if ( !EqualityComparer<TFieldOrProperty>.Default.Equals(
                fieldOrProperty.Value,
                fieldOrProperty.With( other ).Value ) )
        {
            meta.Return( false );
        }
    }

    [Introduce( Name = nameof(GetHashCode), WhenExists = OverrideStrategy.Override )]
    public int GetHashCodeImpl()
    {
        var hashCode = new HashCode();

        foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties.Where(
                     t => t is { IsAutoPropertyOrField: true, IsImplicitlyDeclared: false } ) )
        {
            hashCode.Add( fieldOrProperty.Value );
        }

        return hashCode.ToHashCode();
    }
}