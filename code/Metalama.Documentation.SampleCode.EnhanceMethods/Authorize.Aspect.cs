// This is public domain Metalama sample code.

using System.Security;
using Metalama.Framework.Aspects;
using System.Security.Principal;

namespace Doc.Authorize;

public class AuthorizeAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var currentUser = WindowsIdentity.GetCurrent().Name;

        if ( currentUser == "MrAllmighty" )
        {
            return meta.Proceed();
        }
        else
        {
            throw new SecurityException( "This method can only be called by MrAllmighty." );
        }
    }
}