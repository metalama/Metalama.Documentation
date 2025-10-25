// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.SwitchStatementBuilder_FullTemplate;

public class DispatchAttribute : TypeAspect
{
    [Introduce]
    public void Execute( string messageName, string args )
    {
        var switchBuilder = new SwitchStatementBuilder( ExpressionFactory.Capture( messageName ) );

        var processMethods =
            meta.Target.Type.Methods.Where( m => m.Name.StartsWith(
                                                "Process",
                                                StringComparison.OrdinalIgnoreCase ) );

        foreach ( var processMethod in processMethods )
        {
            var nameWithoutPrefix = processMethod.Name.Substring( "Process".Length );

            switchBuilder.AddCase(
                SwitchStatementLabel.CreateLiteral( nameWithoutPrefix ),
                null,
                StatementFactory.FromTemplate(
                        nameof(this.Case),
                        new { method = processMethod, args = ExpressionFactory.Capture( args ) } )
                    .UnwrapBlock() );
        }

        switchBuilder.AddDefault(
            StatementFactory.FromTemplate( nameof(this.DefaultCase) ).UnwrapBlock(),
            false );

        meta.InsertStatement( switchBuilder.ToStatement() );
    }

    [Template]
    private void Case( IMethod method, IExpression args )
    {
        method.Invoke( args );
    }

    [Template]
    private void DefaultCase()
    {
        throw new ArgumentOutOfRangeException();
    }
}