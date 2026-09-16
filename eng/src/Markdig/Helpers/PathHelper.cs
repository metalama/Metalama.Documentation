// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Docfx.MarkdigEngine.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BuildMetalamaDocumentation.Markdig.Helpers;

internal static class PathHelper
{
    public static string GetRelativePath( string projectDir, string targetPath )
        => new Uri( Path.Combine( projectDir, "_" ) ).MakeRelativeUri( new Uri( targetPath ) )
            .ToString();

    public static IReadOnlyList<string> GetObjPaths(
        string projectDir,
        string targetPath,
        string extension )
    {
        var relativePath = GetRelativePath( projectDir, targetPath );

        return DotNetVersions.Versions.Select(
                v => Path.GetFullPath(
                    Path.Combine(
                        projectDir,
                        "obj",
                        "html",
                        v,
                        Path.ChangeExtension( relativePath, extension ) ) ) )
            .ToArray();
    }

    public static string ResolvePath( string path )
    {
        var rootDirectory = Environment.CurrentDirectory;
        var sourceFileRelativePath = InclusionContext.File.ToString()!;

        var sourceFilePath = Path.Combine( rootDirectory, sourceFileRelativePath );

        if ( path.StartsWith( "~", StringComparison.Ordinal ) )
        {
            var baseDirectory = GitHelper.GetGitDirectory( sourceFilePath );
            path = path.Replace( "~", baseDirectory, StringComparison.Ordinal ).Replace( "/", "\\", StringComparison.Ordinal );
        }
        else
        {
            var baseDirectory = Path.GetDirectoryName( sourceFilePath )!;
            path = path.Replace( "/", "\\", StringComparison.Ordinal );
            path = Path.Combine( baseDirectory, path );
        }

        path = Path.GetFullPath( path );

        return path;
    }
}