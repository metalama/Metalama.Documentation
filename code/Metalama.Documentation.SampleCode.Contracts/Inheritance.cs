// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.Inheritance;

public interface ICustomer
{
    [Phone]
    string? Phone { get; set; }

    [Url]
    string? Url { get; set; }

    [Range( 1900, 2100 )]
    int? BirthYear { get; set; }

    [Required]
    string FirstName { get; set; }

    [Required]
    string LastName { get; set; }
}

public class Customer : ICustomer
{
    public string? Phone { get; set; }

    public string? Url { get; set; }

    public int? BirthYear { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Customer( [Required] string firstName, [Required] string lastName )
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}