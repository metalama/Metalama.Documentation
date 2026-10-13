using System.IO;
namespace Doc.TraceBuildOnly;
public class SettingsLoader
{
  public string Load(string path) => MetalamaInterceptors.File_ReadAllText_Interceptor(path: path);
}