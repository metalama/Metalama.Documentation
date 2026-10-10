// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LAMA0200.Fixed;

public class MethodOverloadCountAttribute : TypeAspect
{
    // Introduces a method that returns the number of overloads of each method.
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

// This class exists both at compile time and at run time.
// Fixed: it implements IExpressionBuilder, so Metalama can convert it to a run-time value.
[RunTimeOrCompileTime]
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
