// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

#pragma warning disable CS8618

namespace Doc.ChildAspect;

[AttributeUsage( AttributeTargets.Method | AttributeTargets.Property )]
public class AuditedMemberAttribute : Attribute, IAspect<IMethod>, IAspect<IProperty>
{
    [IntroduceDependency]
    private readonly IAuditSink _auditSink;

    public AuditedMemberAttribute() : this( true ) { }

    public AuditedMemberAttribute( bool isEnabled )
    {
        this.IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; }

    void IAspect<IMethod>.BuildAspect( IAspectBuilder<IMethod> builder )
    {
        if ( !this.IsEnabled )
        {
            builder.SkipAspect();

            return;
        }

        builder.Override( nameof(this.MethodTemplate) );
    }

    void IAspect<IProperty>.BuildAspect( IAspectBuilder<IProperty> builder )
    {
        if ( !this.IsEnabled )
        {
            builder.SkipAspect();

            return;
        }

        builder.OverrideAccessors( null, nameof(this.MethodTemplate) );
    }

    [Template]
    private dynamic? MethodTemplate()
    {
        this._auditSink.Audit(
            meta.This,
            meta.Target.Method.Name,
            string.Join( ", ", meta.Target.Parameters.ToValueArray() ) );

        return meta.Proceed();
    }
}