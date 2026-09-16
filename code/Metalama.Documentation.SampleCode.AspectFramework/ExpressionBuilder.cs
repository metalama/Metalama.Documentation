// This is public domain Metalama sample code.

namespace Doc.ExpressionBuilder_;

public class Customer
{
    [NotIn( 0, 1, 100 )]
    public int Id { get; set; }
}