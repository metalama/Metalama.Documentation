// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation.Markdig.Helpers;

public static class GitHelper
{
    private static readonly Regex _gitSshRegex = new( "git@(?<server>[^:]+):" );
    private static readonly ConcurrentDictionary<string, string> _branchCache = new();

    public static string GetRepoUrl( string directory )
    {
        // Execute the git command to get the remote URL of the origin
        var psi = new ProcessStartInfo( "git", "remote get-url origin" )
        {
            RedirectStandardOutput = true, WorkingDirectory = directory, UseShellExecute = false
        };

        var process = Process.Start( psi )!;
        process.WaitForExit();

        // Read the output of the command
        var output = process.StandardOutput.ReadToEnd().Trim( ' ', '\n', '\r' );

        if ( output == "" )
        {
            throw new InvalidOperationException( $"'{directory}' is not a git repository." );
        }

        if ( !output.EndsWith( ".git", StringComparison.Ordinal ) )
        {
            throw new InvalidOperationException( $"The git remote url '{output}' does not end with '.git'." );
        }

        var gitSshMatch = _gitSshRegex.Match( output );

        if ( gitSshMatch.Success )
        {
            output = _gitSshRegex.Replace( output, "https://$1/" );
        }

        return output.Substring( 0, output.Length - ".git".Length );
    }

    public static string GetGitDirectory( string path )
    {
        for ( var directory = Path.GetDirectoryName( path );
              directory != null;
              directory = Path.GetDirectoryName( directory ) )
        {
            if ( Directory.Exists( Path.Combine( directory, ".git" ) ) )
            {
                return directory;
            }
        }

        throw new InvalidOperationException( $"Cannot find the project directory for '{path}'." );
    }

    public static string GetBranch( string directory )
    {
        return _branchCache.GetOrAdd( directory, dir =>
        {
            var psi = new ProcessStartInfo( "git", "rev-parse --abbrev-ref HEAD" )
            {
                RedirectStandardOutput = true, WorkingDirectory = dir, UseShellExecute = false
            };

            var process = Process.Start( psi )!;
            process.WaitForExit();

            return process.StandardOutput.ReadToEnd().Trim( ' ', '\n', '\r' );
        } );
    }

    public static string GetOnlineUrl( string path )
    {
        var gitDirectory = GetGitDirectory( path );

        var relativePath = PathHelper.GetRelativePath( gitDirectory, path );
        var gitUrl = GetRepoUrl( gitDirectory );
        var branch = GetBranch( gitDirectory );

        var fullUrl = gitUrl + "/blob/" + branch + "/" + relativePath.Replace( "\\", "/", StringComparison.Ordinal );

        return fullUrl;
    }
}