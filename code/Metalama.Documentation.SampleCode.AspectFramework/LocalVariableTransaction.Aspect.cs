// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LocalVariableTransaction;

public class TransactedMethodAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var fieldsAndProperties = meta.Target.Type.FieldsAndProperties.Where(
                f => f is
                {
                    IsAutoPropertyOrField: true,
                    IsStatic: false,
                    Writeability: Writeability.All,
                    IsImplicitlyDeclared: false
                } )
            .OrderBy( f => f.Name );

        var variables = new List<(IExpression Variable, IFieldOrProperty FieldOrProperty)>();

        foreach ( var fieldAndProperty in fieldsAndProperties )
        {
            var variable = meta.DefineLocalVariable(
                fieldAndProperty.Name,
                fieldAndProperty );

            variables.Add( (variable, fieldAndProperty) );
        }

        try
        {
            return meta.Proceed();
        }
        catch
        {
            foreach ( var pair in variables )
            {
                pair.FieldOrProperty.Value = pair.Variable.Value;
            }

            throw;
        }
    }
}