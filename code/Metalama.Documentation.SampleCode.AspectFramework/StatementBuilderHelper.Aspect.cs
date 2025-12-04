// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.StatementBuilderHelper;

// Compile-time helper class - NOT a template, so must use StatementBuilder.
[CompileTime]
public static class ValidationStatementHelper
{
    // Builds: if (paramName == null) throw new ArgumentNullException(nameof(paramName));
    public static IStatement BuildNullCheck( IParameter parameter )
    {
        var builder = new StatementBuilder();
        builder.AppendVerbatim( "if (" );
        builder.AppendExpression( parameter );
        builder.AppendVerbatim( " == null) throw new " );
        builder.AppendTypeName( typeof(ArgumentNullException) );
        builder.AppendVerbatim( "(nameof(" );
        builder.AppendExpression( parameter );
        builder.AppendVerbatim( "));" );

        return builder.ToStatement();
    }
}

// Aspect that uses the helper.
public class NullGuardAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        foreach ( var p in meta.Target.Parameters.Where(
                      p => p.Type.IsReferenceType == true && p.Type.IsNullable != true ) )
        {
            meta.InsertStatement( ValidationStatementHelper.BuildNullCheck( p ) );
        }

        return meta.Proceed();
    }
}
