// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Utilities;
using System;

namespace Doc.LAMA0883.Error;

[RunTimeOrCompileTime]
[Durable]
public abstract class CategorizedAttribute : Attribute
{
    // The compile-time serializer of derived aspects requires a parameterless constructor.
    protected CategorizedAttribute() : this( "Default" ) { }

    protected CategorizedAttribute( string category )
    {
        this.Category = category;
    }

    public string Category { get; }
}

// Warning: the aspect derives from a base class that isn't immutable.
public class LogAttribute : CategorizedAttribute, IAspect<IMethod>
{
    public LogAttribute( string category ) : base( category ) { }

    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.OverrideMethod) );
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
