using System.Collections.Generic;
using System.Linq;
namespace Doc.CallsOnly;
[LogTextCalls]
public class TagService
{
  public string NormalizeOne(string tag) => MetalamaInterceptors.TextUtil_Normalize_Interceptor(text: tag);
  public List<string> NormalizeAll(IEnumerable<string> tags) => tags.Select(TextUtil.Normalize).ToList();
}
public static class TextUtil
{
  public static string Normalize(string text) => text.Trim().ToLowerInvariant();
}