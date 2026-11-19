// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;

namespace Doc.CompileTimeRules_OwnType;

internal sealed class LogAspect : IAspect<IMethod>
{
    private readonly LogPolicy _policy;

    public LogAspect( LogPolicy policy )
    {
        this._policy = policy;
    }

    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.OverrideMethod) );
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        var message = new InterpolatedStringBuilder();
        message.AddText( $"[{this._policy.Category}] Executing {meta.Target.Method.Name}(" );

        foreach ( var parameter in meta.Target.Parameters )
        {
            if ( parameter.Index > 0 )
            {
                message.AddText( ", " );
            }

            message.AddText( $"{parameter.Name}: " );

            if ( this._policy.HiddenParameters.Contains( parameter.Name ) )
            {
                message.AddText( "***" );
            }
            else
            {
                message.AddExpression( parameter );
            }
        }

        message.AddText( ")." );
        Console.WriteLine( message.ToValue() );

        return meta.Proceed();
    }
}
