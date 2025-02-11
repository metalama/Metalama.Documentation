// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Metalama.Documentation.QuickStart.Fabrics
{
    internal class AddLoggingToBaseClassChildren : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender
                    
                //Locate all derived types of a given base class
                .SelectTypesDerivedFrom( typeof(BaseClass) )

                //Find all methods of all of these types
                .SelectMany( t => t.Methods )

                //Find all the public member functions of these types
                .Where( method => method.Accessibility == Accessibility.Public )

                //Add `Log` attribute to all of these  
                .AddAspectIfEligible<LogAttribute>();
        }
    }
}