// Warning LAMA0906 on `TextLoader`: `The type 'TextLoader' does not respect the naming convention set by a fabric. The type name should match the "^.*Reader$" pattern.`
using System.IO;
namespace Doc.LAMA0906.Error;
// The name of this class does not end with Reader.
internal class TextLoader : TextReader
{
}