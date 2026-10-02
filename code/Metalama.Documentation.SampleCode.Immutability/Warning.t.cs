// Warning LAMA5021 on `FirstName`: `The 'Person.FirstName' property must not have a setter because of the [Immutable] aspect.`
using Metalama.Patterns.Immutability;
namespace Metalama.Documentation.SampleCode.Immutability.Warning;
[Immutable]
public class Person
{
  public required string FirstName { get; set; }
  public required string LastName { get; init; }
}