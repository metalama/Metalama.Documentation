// This is public domain Metalama sample code.

using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.Decoupled_Ref;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var declarations = amender.SelectDeclarationsWithAttribute( typeof(LogAttribute) );

        declarations
            .OfType<IMethod>()
            .AddAspectIfEligible(
                m =>
                {
                    var attribute = m.Attributes.OfAttributeType( typeof(LogAttribute) )
                        .Single();

                    return new LogAspect( attribute.ToRef() );
                } );
        
        declarations
            .OfType<IProperty>()
            .AddAspectIfEligible(
                p =>
                {
                    var attribute = p.Attributes.OfAttributeType( typeof(LogAttribute) )
                        .Single();

                    return new LogAspect( attribute.ToRef() );
                } );
    }
}