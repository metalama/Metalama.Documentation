// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Collections.Generic;
using System.Linq;

namespace Doc.CustomSyntaxSerializer;

public class MemberCountAspect : TypeAspect
{
    // Introduces a method that returns a dictionary of method names with the number of overloads
    // of this method.
    [Introduce]
    public Dictionary<string, MethodOverloadCount> GetMethodOverloadCount()
    {
        var dictionary = meta.Target.Type.Methods
            .GroupBy( m => m.Name )
            .Select( g => new MethodOverloadCount( g.Key, g.Count() ) )
            .ToDictionary( m => m.Name, m => m );

        return dictionary;
    }
}

// This class is both compile-time and run-time.
// It implements IExpressionBuilder to convert its compile-time value to an expression that results
// in the equivalent run-time value.
public class MethodOverloadCount : IExpressionBuilder
{
    public MethodOverloadCount( string name, int count )
    {
        this.Name = name;
        this.Count = count;
    }

    public string Name { get; }

    public int Count { get; }

    public IExpression ToExpression()
    {
        var builder = new ExpressionBuilder();
        builder.AppendVerbatim( "new " );
        builder.AppendTypeName( typeof(MethodOverloadCount) );
        builder.AppendVerbatim( "(" );
        builder.AppendLiteral( this.Name );
        builder.AppendVerbatim( ", " );
        builder.AppendLiteral( this.Count );
        builder.AppendVerbatim( ")" );

        return builder.ToExpression();
    }
}