// This is public domain Metalama sample code.

using Flashtrace.Formatters;
using System.IO;

namespace Doc.Formatter;

internal class FileInfoFormatter : Formatter<FileInfo>
{
    public FileInfoFormatter( IFormatterRepository repository ) : base( repository ) { }

    public override void Format( UnsafeStringBuilder stringBuilder, FileInfo? value )
        => stringBuilder.Append( value?.FullName ?? "<null>" );
}