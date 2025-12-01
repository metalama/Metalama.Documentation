using Metalama.Documentation.QuickStart;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Code;

namespace Metalama.Documentation.QuickStart.Fabrics
{
    public class AddNoNullsToPublicStringProperties : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender)
        {
            amender
            .SelectMany(type => type.Types)
            .SelectMany(z => z.FieldsAndProperties.Where(fieldOrProp => fieldOrProp.Accessibility == Accessibility.Public))
            .Where(publicPropOrField => publicPropOrField.Type.Equals(SpecialType.String))
            .AddAspectIfEligible<NoNullStringAttribute>();
        }
    }
}