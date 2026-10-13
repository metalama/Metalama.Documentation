using System.Collections.Generic;
using System.Linq;
namespace Doc.MethodGroups;
[LogTextCalls]
public class TagService
{
  public string NormalizeOne(string tag) => TextLogging.Normalize(tag);
  public List<string> NormalizeAll(IEnumerable<string> tags) => tags.Select(TextLogging.Normalize).ToList();
}
public static class TextUtil
{
  public static string Normalize(string text) => text.Trim().ToLowerInvariant();
}