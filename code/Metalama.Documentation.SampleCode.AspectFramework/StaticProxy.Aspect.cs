// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.StaticProxy;

public class ProxyAspect : TypeAspect
{
    private readonly Type _interfaceType;

    [Introduce]
    private IInterceptor _interceptor;

    public ProxyAspect( Type interfaceType )
    {
        this._interfaceType = interfaceType;
    }

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        // Add a field with the intercepted object.
        var interceptedField = builder.IntroduceField(
                "_intercepted",
                this._interfaceType,
                IntroductionScope.Instance )
            .Declaration;

        // Implement the interface.
        var implementInterfaceResult = builder.ImplementInterface( this._interfaceType );

        // Implement interface members.
        var namedType = (INamedType) TypeFactory.GetType( this._interfaceType );

        foreach ( var method in namedType.Methods )
        {
            implementInterfaceResult.ExplicitMembers.IntroduceMethod(
                method.ReturnType.Equals( SpecialType.Void )
                    ? nameof(this.VoidTemplate)
                    : nameof(this.NonVoidTemplate),
                IntroductionScope.Instance,
                buildMethod: methodBuilder =>
                {
                    methodBuilder.Name = method.Name;
                    methodBuilder.ReturnType = method.ReturnType;

                    foreach ( var parameter in method.Parameters )
                    {
                        methodBuilder.AddParameter(
                            parameter.Name,
                            parameter.Type,
                            parameter.RefKind );
                    }
                },
                args:
                /*method.ReturnType.Is( SpecialType.Void ) ? new { method, interceptedField } :*/
                new { T = method.ReturnType, method, interceptedField } );
        }

        // Add the constructor.
        builder.IntroduceConstructor(
            nameof(this.Constructor),
            buildConstructor: constructorBuilder
                => constructorBuilder.AddParameter( "intercepted", this._interfaceType ),
            args: new { interceptedField } );
    }

    [Template]
    private T NonVoidTemplate<[CompileTime] T>( IMethod method, IField interceptedField )
    {
        return this._interceptor.Invoke(
            () => (T) method.With( interceptedField ).Invoke( method.Parameters )! );
    }

    [Template]
    private void VoidTemplate( IMethod method, IField interceptedField )
    {
        this._interceptor.Invoke(
            () =>
            {
                method.With( interceptedField ).Invoke( method.Parameters );
            } );
    }

    [Template]
    public void Constructor( IInterceptor interceptor, IField interceptedField )
    {
        this._interceptor = interceptor;
        interceptedField.Value = meta.Target.Parameters["intercepted"].Value;
    }
}