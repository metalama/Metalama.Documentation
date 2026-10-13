using System.Collections.Generic;
namespace Doc.GenericLiftedTypeParameter;
// The call to List<T>.Add uses the type parameter T of the calling type.
[TraceListChanges]
public class Repository<T>
{
  private readonly List<T> _items = new();
  public void Add(T item) => MetalamaInterceptors.List_Add_Interceptor(this._items, item: item);
}