// This is public domain Metalama sample code.

using System.Linq;

namespace Doc.LAMA0656.Fixed;

[LogInitialized]
public class Document
{
    public string? Title { get; set; }
}

public class DocumentQueries
{
    public IQueryable<Document> GetDocuments( IQueryable<string> titles )
    {
        // The expression tree calls a method, and this method creates the object.
        return titles.Select( title => CreateDocument( title ) );
    }

    private static Document CreateDocument( string title ) => new Document { Title = title };
}
