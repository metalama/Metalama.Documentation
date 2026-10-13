// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.GenericSameShape;

public class CheckRepositoryArgumentsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(Repository) )
                .AllMethods()
                // [<snippet WithArgs>]
                .ForEachSite(
                    site => site
                        .RedirectToSynthesizedMethod( nameof(this.CheckNotNull) )
                        .WithArgs( new { T = site.InterceptedMethod.Parameters[0].Type } ) ) );
                // [<endsnippet WithArgs>]

    // The template has a parameter of type T, which receives the type of the parameter of each
    // intercepted method.
    [Template]
    // [<snippet Template>]
    public void CheckNotNull<[CompileTime] T>( T entity )
    {
        if ( entity == null )
        {
            throw new ArgumentNullException( nameof(entity) );
        }

        meta.Proceed();
    }
    // [<endsnippet Template>]
}
