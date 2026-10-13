// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.IntroduceOperator;

public class EquatableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Implement IEquatable<T>.
        var equatable = builder.Target.Compilation.Factory
            .GetNamedTypeByReflectionType( typeof(IEquatable<>) )
            .MakeGenericInstance( builder.Target );

        builder.ImplementInterface( equatable );

        // Introduce the typed Equals(T) for IEquatable<T>.
        builder.IntroduceMethod(
            nameof(TypedEquals),
            buildMethod: m => m.Name = "Equals",
            args: new { T = builder.Target } );

        // Introduce == operator.
        builder.IntroduceMethod(
            nameof(EqualityOperator),
            buildMethod: m =>
            {
                m.OperatorKind = OperatorKind.Equality;
                m.Parameters[0].Type = builder.Target;
                m.Parameters[1].Type = builder.Target;
            } );

        // Introduce != operator.
        builder.IntroduceMethod(
            nameof(InequalityOperator),
            buildMethod: m =>
            {
                m.OperatorKind = OperatorKind.Inequality;
                m.Parameters[0].Type = builder.Target;
                m.Parameters[1].Type = builder.Target;
            } );
    }

    [Template]
    public bool TypedEquals<[CompileTime] T>( T? other )
    {
        if ( other is null )
        {
            return false;
        }

        var otherExpression = ExpressionFactory.Capture( other );

        foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties.Where(
                     f => f is { IsAutoPropertyOrField: true, IsImplicitlyDeclared: false, IsStatic: false } ) )
        {
            meta.InvokeTemplate(
                nameof(this.CompareFieldOrProperty),
                args: new
                {
                    TField = fieldOrProperty.Type,
                    fieldOrProperty,
                    other = otherExpression
                } );
        }

        return true;
    }

    [Template]
    private void CompareFieldOrProperty<[CompileTime] TField>(
        IFieldOrProperty fieldOrProperty,
        IExpression other )
    {
        if ( !EqualityComparer<TField>.Default.Equals(
                fieldOrProperty.Value,
                fieldOrProperty.WithObject( other ).Value ) )
        {
            meta.Return( false );
        }
    }

    [Introduce( Name = nameof(Equals), WhenExists = OverrideStrategy.Override )]
    public bool EqualsOverride( object? obj )
    {
        if ( obj is null || obj.GetType() != meta.Target.Type.ToType() )
        {
            return false;
        }

        return meta.This.Equals( meta.Cast( meta.Target.Type, obj ) );
    }

    [Introduce( Name = nameof(GetHashCode), WhenExists = OverrideStrategy.Override )]
    public int GetHashCodeOverride()
    {
        var hashCode = new HashCode();

        foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties.Where(
                     f => f is { IsAutoPropertyOrField: true, IsImplicitlyDeclared: false, IsStatic: false } ) )
        {
            hashCode.Add( fieldOrProperty.Value );
        }

        return hashCode.ToHashCode();
    }

    [Template]
    public static bool EqualityOperator( dynamic? left, dynamic? right )
    {
        return object.Equals( left, right );
    }

    [Template]
    public static bool InequalityOperator( dynamic? left, dynamic? right )
    {
        return !object.Equals( left, right );
    }
}
