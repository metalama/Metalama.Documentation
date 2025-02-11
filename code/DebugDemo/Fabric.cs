// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.QuickStart;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace DebugDemo
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            var allPublicMethods = amender
                .SelectTypes() // Get all types 
                .SelectMany( t => t.Methods )  // Get all methods
                .Where( z => z.Accessibility == Accessibility.Public );

            // Adding Log aspect 
            allPublicMethods.AddAspectIfEligible<LogAttribute>();
        }
    }
}