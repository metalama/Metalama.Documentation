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
                .SelectTypes()
                .SelectMany( t => t.Methods )
                .Where( t => t.Accessibility == Accessibility.Public );

            this.AddLoggingAspect( allPublicMethods );
            this.AddRetryAspect( allPublicMethods );
        }

        public void AddLoggingAspect( IQuery<IMethod> methods ) => methods.AddAspectIfEligible<LogAttribute>();

        public void AddRetryAspect( IQuery<IMethod> methods )
        {
            methods

                //Additional filter on the public methods
                .Where( t => t.Name.StartsWith( "Try", StringComparison.Ordinal ) )
                .AddAspectIfEligible<RetryAttribute>();
        }
    }
}