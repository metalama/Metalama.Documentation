// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;

namespace Doc.CustomRegexContract;

[RunTimeOrCompileTime]
public class PasswordAttribute : RegularExpressionAttribute
{
    public PasswordAttribute() : base(
        "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,20}$\n" ) { }
}