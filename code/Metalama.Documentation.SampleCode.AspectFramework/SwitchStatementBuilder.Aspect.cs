// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.SwitchStatementBuilder_;

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
            var invokeExpression = (IExpression) processMethod.Invoke( args )!;

            switchBuilder.AddCase(
                SwitchStatementLabel.CreateLiteral( nameWithoutPrefix ),
                null,
                StatementFactory.FromExpression( invokeExpression ).AsList() );
        }

        switchBuilder.AddDefault(
            StatementFactory.FromTemplate( nameof(this.DefaultCase) ).UnwrapBlock(),
            false );

        meta.InsertStatement( switchBuilder.ToStatement() );
    }

    [Template]
    private void DefaultCase()
    {
        throw new ArgumentOutOfRangeException();
    }
}