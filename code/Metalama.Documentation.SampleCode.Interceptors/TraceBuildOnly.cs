// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @DefinedConstant(TRACE_CALLS)
#endif

using System.IO;

namespace Doc.TraceBuildOnly;

public class SettingsLoader
{
    public string Load( string path ) => File.ReadAllText( path );
}
