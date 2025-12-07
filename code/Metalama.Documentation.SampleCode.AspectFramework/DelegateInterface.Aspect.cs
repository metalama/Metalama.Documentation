// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.DelegateInterface;

/// <summary>
/// Implements an interface by delegating all members to a field of the same type.
/// </summary>
public class DelegateInterfaceAttribute : TypeAspect
{
    private readonly Type _interfaceType;
    private readonly string _fieldName;

    public DelegateInterfaceAttribute( Type interfaceType, string fieldName )
    {
        this._interfaceType = interfaceType;
        this._fieldName = fieldName;
    }

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Get the interface type.
        var interfaceType = (INamedType) TypeFactory.GetType( this._interfaceType );

        // Get the field to delegate to.
        var field = builder.Target.Fields.Single( f => f.Name == this._fieldName );

        // Implement the interface.
        var implementResult = builder.ImplementInterface( interfaceType, OverrideStrategy.Ignore );

        // Introduce explicit implementations for all methods.
        foreach ( var method in interfaceType.Methods )
        {
            implementResult.ExplicitMembers.IntroduceMethod(
                nameof(this.DelegateMethodTemplate),
                buildMethod: m =>
                {
                    m.Name = method.Name;
                    m.ReturnType = method.ReturnType;

                    // Copy parameters.
                    foreach ( var param in method.Parameters )
                    {
                        m.AddParameter( param.Name, param.Type, param.RefKind );
                    }
                },
                args: new { field, method } );
        }

        // Introduce explicit implementations for all properties.
        foreach ( var property in interfaceType.Properties )
        {
            implementResult.ExplicitMembers.IntroduceProperty(
                property.Name,
                property.GetMethod != null ? nameof(this.DelegateGetterTemplate) : null,
                property.SetMethod != null ? nameof(this.DelegateSetterTemplate) : null,
                buildProperty: p => p.Type = property.Type,
                args: new { field, property } );
        }
    }

    [Template]
    public dynamic? DelegateMethodTemplate( IField field, IMethod method )
    {
        return method.WithObject( field ).Invoke( meta.Target.Parameters.Cast<IExpression>() );
    }

    [Template]
    public dynamic? DelegateGetterTemplate( IField field, IProperty property )
    {
        return property.WithObject( field ).Value;
    }

    [Template]
    public void DelegateSetterTemplate( IField field, IProperty property )
    {
        property.WithObject( field ).Value = meta.Target.Parameters[0].Value;
    }
}
