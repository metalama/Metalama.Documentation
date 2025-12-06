// This is public domain Metalama sample code.

namespace Doc.SuppressWarningWithFilter;

#if TEST_OPTIONS
// @ClearIgnoredDiagnostics
#endif

internal class MyService
{
    [SuppressUnusedVariable]
    public void Initialize()
    {
        // The CS0219 warning for this variable is suppressed by the aspect
        // because the variable is named "_initialized".
        var _initialized = true;

        // The CS0219 warning for this variable is NOT suppressed
        // because the filter only matches variables named "_initialized".
        var _other = 42;
    }
}
