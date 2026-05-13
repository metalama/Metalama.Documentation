// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Collections.Immutable;

namespace BuildMetalamaDocumentation.Markdig.Helpers;

internal static class DotNetVersions
{
    public static ImmutableArray<string> Versions { get; } =
        ImmutableArray.Create(
            "net6.0",
            "net7.0",
            "net10.0-windows",
            "net7.0-windows",
            "net10.0",
            "net10.0-windows" );
}