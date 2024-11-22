// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Fabrics;

public class SharedFabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender
            .SelectTypes()
            .SelectMany( t => t.Methods )
            .AddAspectIfEligible<LogAttribute>();
    }
}