﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.QuickStart;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace DebugDemo2
{
    public class AddLogAspectInGivenNamespaceFabric : ProjectFabric
    {
        /// <summary>
        /// Amends the project by adding Log aspect 
        /// to many eligible methods inside given namespace.
        /// </summary>
        public override void AmendProject( IProjectAmender amender )
        {
            //Adding Log attribute to all mehtods of all types 
            //that are available inside "Outer.Inner" namespace 

            amender.Outbound.SelectMany(
                    t => t.GlobalNamespace
                        .DescendantsAndSelf()
                        .Where( z => z.FullName.StartsWith( "Outer.Inner", StringComparison.Ordinal ) ) )
                .SelectMany( ns => ns.Types.SelectMany( t => t.Methods ) )
                .AddAspectIfEligible<LogAttribute>();
        }
    }
}