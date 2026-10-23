using System;
using System.Linq;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.LAMA0656.Fixed;
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
    // The expression tree calls a method, and this method creates the object.
    return titles.Select(title => CreateDocument(title));
  }
  private static Document CreateDocument(string title)
  {
    return new Document
    {
      Title = title
    }.WithInitialize();
  }
}