// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Metalama.Documentation.QuickStart.Fabrics
{
    internal class NotifyPropertyChangedForAllPublicTypes : NamespaceFabric
    {
        public override void AmendNamespace( INamespaceAmender project )
        {
            //Add NotifyPropertyChanged to all public types 
            project
                .SelectTypes()
                .Where( t => t.Accessibility == Accessibility.Public )
                .AddAspectIfEligible<NotifyPropertyChangedAttribute>();
        }
    }
}