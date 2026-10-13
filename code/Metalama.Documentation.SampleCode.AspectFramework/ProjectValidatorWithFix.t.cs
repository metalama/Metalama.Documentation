// Warning MY001 on `Writer`: `The field SomeType.Writer must be private.`
//    CodeFix: Make private`
using System.IO;
namespace Doc.ProjectValidatorWithFix;
internal class SomeType
{
  public TextWriter Writer;
}