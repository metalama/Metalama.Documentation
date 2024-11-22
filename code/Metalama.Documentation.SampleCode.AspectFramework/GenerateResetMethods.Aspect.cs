// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.GenerateResetMethods;

public class GenerateResetMethodsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        foreach ( var field in builder.Target.FieldsAndProperties.Where(
                     f => !f.IsImplicitlyDeclared && f.Writeability != Writeability.None ) )
        {
            builder.IntroduceMethod(
                nameof(this.Reset),
                args: new { field, T = field.Type },
                buildMethod: m => m.Name = "Reset" + CamelCase( field.Name ) );
        }
    }

    private static string CamelCase( string s )
    {
        s = s.TrimStart( '_' );

        return s[0].ToString().ToUpperInvariant() + s.Substring( 1 );
    }

    [Template]
    public void Reset<[CompileTime] T>( IFieldOrProperty field )
    {
        field.Value = default(T);
    }
}