// This is public domain Metalama sample code.

using System.Linq;

namespace Doc.LAMA0656.Error;

[LogInitialized]
public class Document
{
    public string? Title { get; set; }
}

public class DocumentQueries
{
    public IQueryable<Document> GetDocuments( IQueryable<string> titles )
    {
        // The lambda expression is converted to an expression tree, so Initialize is not called.
        return titles.Select( title => new Document { Title = title } );
    }
}
