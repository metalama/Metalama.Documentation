// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Doc.AuditFileDelete;

// [<snippet Exclude>]
[ExcludeInterceptors]
public static class Audit
// [<endsnippet Exclude>]
{
    // [<snippet Signature>]
    public static void DeleteFile(
        string path,
        string category,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0 )
    // [<endsnippet Signature>]
    {
        Console.WriteLine(
            $"[{category}] {caller} (line {line}) deletes {Path.GetFileName( path )}." );

        File.Delete( path );
    }
}
