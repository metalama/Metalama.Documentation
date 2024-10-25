// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.UpdateMethod;

internal class UpdateMethodAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod(
            nameof(this.Update),
            buildMethod:
            m =>
            {
                var fieldsAndProperties =
                    builder.Target.FieldsAndProperties
                        .Where(
                            f => f is
                            {
                                IsImplicitlyDeclared: false, Writeability: Writeability.All
                            } );

                foreach ( var field in fieldsAndProperties )
                {
                    m.AddParameter( field.Name, field.Type );
                }
            } );
    }

    [Template]
    public void Update()
    {
        var index = meta.CompileTime( 0 );

        foreach ( var parameter in meta.Target.Parameters )
        {
            var field = meta.Target.Type.FieldsAndProperties.OfName( parameter.Name ).Single();

            field.Value = meta.Target.Parameters[index].Value;
            index++;
        }
    }
}