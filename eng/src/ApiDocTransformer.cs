// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation;

/// <summary>
/// Transforms DocFx API YML files for inclusion in the AI skill: strips sections that are only
/// used for HTML rendering (they account for ~88% of file size) and relocates the legacy
/// PostSharp API documentation to a quarantine subdirectory so that searches over the main API
/// don't surface it.
/// </summary>
internal static class ApiDocTransformer
{
    /// <summary>
    /// Top-level YML sections that DocFx uses for hyperlink rendering only. They contain no
    /// documentation content (the <c>items:</c> section has summary, syntax, parameters, returns).
    /// </summary>
    private static readonly HashSet<string> _strippedSections = new( StringComparer.Ordinal ) { "references", "memberLayout" };

    private static readonly Regex _topLevelKeyRegex = new( "^([A-Za-z][A-Za-z0-9]*):", RegexOptions.Compiled );

    /// <summary>
    /// Subdirectory of <c>api/</c> receiving the legacy PostSharp API documentation, which is
    /// shipped only to support PostSharp-to-Metalama migration.
    /// </summary>
    public const string MigrationSubdirectory = "migration";

    public static bool IsMigrationFile( string fileName ) => fileName.StartsWith( "PostSharp.", StringComparison.Ordinal );

    /// <summary>
    /// Removes the <c>references:</c> and <c>memberLayout:</c> top-level sections from a DocFx
    /// ManagedReference YML document.
    /// </summary>
    public static string StripRenderingSections( string yaml )
    {
        var stringBuilder = new StringBuilder( yaml.Length );
        var skipping = false;

        foreach ( var rawLine in yaml.Split( '\n' ) )
        {
            var line = rawLine.TrimEnd( '\r' );
            var keyMatch = _topLevelKeyRegex.Match( line );

            if ( keyMatch.Success )
            {
                skipping = _strippedSections.Contains( keyMatch.Groups[1].Value );
            }

            if ( !skipping )
            {
                stringBuilder.Append( line );
                stringBuilder.Append( '\n' );
            }
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Rewrites the <c>.manifest</c> UID-to-file index so that entries pointing to relocated
    /// PostSharp files include the <see cref="MigrationSubdirectory"/> prefix.
    /// </summary>
    public static string TransformManifest( string manifestJson )
    {
        var map = JsonSerializer.Deserialize<Dictionary<string, string>>( manifestJson )
                  ?? throw new InvalidOperationException( "The API manifest could not be parsed." );

        foreach ( var key in map.Keys.ToList() )
        {
            if ( IsMigrationFile( map[key] ) )
            {
                map[key] = MigrationSubdirectory + "/" + map[key];
            }
        }

        // Keep the manifest sorted and one-entry-per-line so it remains greppable.
        var sorted = new SortedDictionary<string, string>( map, StringComparer.Ordinal );
        var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        return JsonSerializer.Serialize( sorted, options );
    }
}
