﻿using Metalama.Documentation.QuickStart;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.ComponentModel;

namespace DebugDemo2
{

    public class AddLogAspectInGivenNamespaceFabric : ProjectFabric
    {
        /// <summary>
        /// Amends the project by adding Log aspect 
        /// to many eligible methods inside given namespace.
        /// </summary>
        public override void AmendProject(IProjectAmender amender)
        {
            //Adding Log attribute to all mehtods of all types 
            //that are available inside "Outer.Inner" namespace 

            amender.Outbound.SelectMany(t => t.GlobalNamespace
                                              .DescendantsAndSelf()
                                              .Where(z => z.FullName.StartsWith("Outer.Inner")))
                            .SelectMany(ns => ns.Types.SelectMany(t => t.Methods))
                            .AddAspectIfEligible<LogAttribute>();
        }
    }
}


