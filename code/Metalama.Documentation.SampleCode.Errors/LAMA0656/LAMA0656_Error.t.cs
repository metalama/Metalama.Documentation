// Warning LAMA0656 on `new Document { Title = title }`: `The 'Initialize' method of 'Document' is not called after this object creation expression, because the expression is in a lambda expression converted to an expression tree, and an expression tree cannot contain this call.`
using System;
using System.Linq;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.LAMA0656.Error;
[LogInitialized]
public class Document : IInitializable
{
  public string? Title { get; set; }
  public virtual void Initialize(InitializationContext context = default)
  {
    Console.WriteLine("Document initialized.");
  }
}
public class DocumentQueries
{
  public IQueryable<Document> GetDocuments(IQueryable<string> titles)
  {
    // The lambda expression is converted to an expression tree, so Initialize is not called.
    return titles.Select(title => new Document { Title = title });
  }
}