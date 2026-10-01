// This is public domain Metalama sample code.

namespace Doc.Eligibility_DeclaringType;

internal record SomeClass( int Foo )
{
    [StaticLog]
    private void SomeMethod() { }
}