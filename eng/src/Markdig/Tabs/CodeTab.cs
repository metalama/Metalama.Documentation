// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Sandbox;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation.Markdig.Tabs;

public class CodeTab : BaseTab
{
    private readonly string? _htmlPath;

    private static readonly Regex _startSnippetRegex =
        new("""\[\&lt;snippet\s+(?<name>\w+)\s*\&gt;\]""", RegexOptions.Compiled);

    private static readonly Regex _endSnippetRegex =
        new("""\[\&lt;endsnippet\s+(?<name>\w+)\s*\&gt;\]""", RegexOptions.Compiled);
    
    private static readonly Regex _memberRegex =
        new("""<span class='line-number' data-member='([^']*)'>""", RegexOptions.Compiled);

    private static readonly Regex _emptyLineRegex =
        new(
            """^\s*<span class='line-number'[^>]*>\d+<\/span>(<span class="cr-NeutralTrivia">\s*</span>?(<span class="cr-NeutralTrivia cs-comment">\/\*&lt;\w*&gt;\*\/<\/span>)?)\s*$""", RegexOptions.Compiled);

    private static readonly Regex _captureIndentRegex =
        new(
            """^\s*<span class='line-number'[^>]*>\d+<\/span><span class="cr-NeutralTrivia">(?<indent>\s*)<\/span>""", RegexOptions.Compiled);

    public CodeTab(
        string tabId,
        string fullPath,
        SandboxFileKind sandboxFileKind,
        string? marker = null,
        string? member = null,
        string? tabHeader = null,
        string? htmlPath = null ) : base(
        tabId,
        fullPath )
    {
        this._htmlPath = htmlPath;
        this.SandboxFileKind = sandboxFileKind;
        this.Member = member;
        this.TabHeader = tabHeader ?? tabId + " Code";
        this.Marker = marker;
    }

    public SandboxFileKind SandboxFileKind { get; }

    public string? Member { get; }

    public string? Marker { get; }

    protected virtual IEnumerable<string> HtmlExtensions => [".cs.html"];

    protected override string TabHeader { get; }

    protected override bool IsContentEmpty( string[] lines )
        => base.IsContentEmpty( lines ) || lines.All( l => l.TrimStart().StartsWith( "//", StringComparison.Ordinal ) );

    private IEnumerable<string> GetPossibleHtmlPaths()
    {
        if ( this._htmlPath != null )
        {
            return [this._htmlPath];
        }
        else
        {
            return this.HtmlExtensions
                .SelectMany( e => PathHelper.GetObjPaths( this.GetProjectDirectory(), this.FullPath, e ) );
        }
    }

    private string? GetExistingHtmlPath( bool throwIfMissing )
    {
        var possibleHtmlPaths = this.GetPossibleHtmlPaths().ToList();

        var htmlPath = possibleHtmlPaths.FirstOrDefault( File.Exists );

        if ( htmlPath == null && throwIfMissing )
        {
            throw new FileNotFoundException(
                $"No HTML file for '{this.FullPath}' could be found. The following locations were tried: {string.Join( ", ", possibleHtmlPaths )}",
                htmlPath );
        }

        return htmlPath;
    }

    public bool Exists() => this.GetPossibleHtmlPaths().Any( File.Exists );

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var htmlPath = this.GetExistingHtmlPath( !fallbackToSource );

        if ( htmlPath != null )
        {
            if ( !string.IsNullOrEmpty( this.Marker ) || !string.IsNullOrEmpty( this.Member ) )
            {
                var outputLines = new List<string>();

                var captureLine = false;
                var foundStartMarker = false;
                var foundEndMarker = false;
                var foundMember = false;

                // Read and filter lines.
                foreach ( var htmlLine in File.ReadAllLines( htmlPath ) )
                {
                    // Process the [<snippet x>] marker.
                    var matchStartMarker = _startSnippetRegex.Match( htmlLine );

                    if ( matchStartMarker.Success )
                    {
                        if ( matchStartMarker.Groups["name"].Value == this.Marker )
                        {
                            captureLine = true;
                            foundStartMarker = true;
                        }
                        
                        // Skip the whole line.
                        continue;
                    }
                    
                    // Process the [<endsnippet x>] marker.
                    var matchEndMarker = _endSnippetRegex.Match( htmlLine );

                    if ( matchEndMarker.Success )
                    {
                        if ( matchEndMarker.Groups["name"].Value == this.Marker )
                        {
                            captureLine = false;
                            foundEndMarker = true;
                        }
                        
                        // Skip the whole line.
                        continue;
                    }
                    
                    // Capture the line if the member matches.
                    if ( !string.IsNullOrEmpty( this.Member ) )
                    {
                        var matchMember = _memberRegex.Match( htmlLine );

                        captureLine = matchMember.Success
                                         && this.Member == matchMember.Groups[1].Value;

                        if ( captureLine )
                        {
                            foundMember = true;
                        }
                    }

                    if ( captureLine )
                    {
                        outputLines.Add( htmlLine );
                    }
                }

                // Check that we found the markers.
                if ( this.Marker != null && !foundStartMarker )
                {
                    throw new InvalidOperationException(
                        $"The '[<snippet {this.Marker}>]' marker was not found in '{htmlPath}'." );
                }

                if ( this.Marker != null && !foundEndMarker )
                {
                    throw new InvalidOperationException(
                        $"The '[<endsnippet {this.Marker}>]' marker was not found in '{htmlPath}'." );
                }

                if ( this.Member != null && !foundMember )
                {
                    throw new InvalidOperationException( $"The member '{this.Member}' was not found in '{htmlPath}'." );
                }

                // Trim lines.
                while ( outputLines.Count > 0 && _emptyLineRegex.IsMatch( outputLines[0] ) )
                {
                    outputLines.RemoveAt( 0 );
                }

                while ( outputLines.Count > 0
                        && _emptyLineRegex.IsMatch( outputLines[^1] ) )
                {
                    outputLines.RemoveAt( outputLines.Count - 1 );
                }

                // Reduce indentation
                var minIndentation = outputLines.Select( l =>
                    {
                        var match = _captureIndentRegex.Match( l );
                        if ( match.Success && match.Groups["indent"].Success )
                        {
                            return match.Groups["indent"].Length; // Minimum must be 1.
                        }
                        else
                        {
                            // We have a blank line.
                            return int.MaxValue;
                        }
                    } )
                    .Min();

                if ( minIndentation > 1 && minIndentation != int.MaxValue )
                {
                    outputLines = outputLines.Select( l =>
                        {
                            var match = _captureIndentRegex.Match( l );
                            if ( match.Success && match.Groups["indent"].Success )
                            {
                                var indentation = match.Groups["indent"];
                                return l.Substring( 0, indentation.Index )
                                       + new string( ' ', indentation.Length - minIndentation)
                                       + l.Substring( indentation.Index + indentation.Length );
                            }
                            else
                            {
                                // Blank line.
                                return l;
                            }
                        }
                    ).ToList();
                }


                // Return the final html.
                return "<pre><code class=\"nohighlight\">" + string.Join( "\n", outputLines )
                                                           + "</code></pre>";
            }

            var html = File.ReadAllText( htmlPath );

            var cleanHtml = _startSnippetRegex.Replace(
                _endSnippetRegex.Replace( html, "" ),
                "" );

            return cleanHtml;
        }

        // When the HTML file does not exist, we will rely on run-time formatting.
        // Strip leading comments (copyright headers) like GetSandboxCode does.
        // Also filter out snippet marker lines like "// <Fabric>" or "// </Fabric>".
        var snippetMarkerRegex = new Regex( @"^\s*//\s*</?[A-Za-z]+>\s*$" );

        var lines = File.ReadAllLines( this.FullPath )
            .SkipWhile( l => l.TrimStart().StartsWith( "//", StringComparison.Ordinal ) )
            .Where( l => !snippetMarkerRegex.IsMatch( l ) )
            .ToList();

        // Trim leading/trailing empty lines.
        while ( lines.Count > 0 && string.IsNullOrWhiteSpace( lines[0] ) )
        {
            lines.RemoveAt( 0 );
        }

        while ( lines.Count > 0 && string.IsNullOrWhiteSpace( lines[^1] ) )
        {
            lines.RemoveAt( lines.Count - 1 );
        }

        return "<pre><code class=\"lang-csharp\">" + string.Join( Environment.NewLine, lines )
                                                   + "<code></pre>";
    }

    public string GetCodeForComparison()
    {
        var document = new HtmlDocument();

        var htmlPath = this.GetExistingHtmlPath( true )!;

        document.Load( htmlPath );

        var diagLines = document.DocumentNode.SelectNodes( "//span[@class='diagLines']" )?.ToList()
                        ?? new List<HtmlNode>();

        foreach ( var diag in diagLines )
        {
            diag.Remove();
        }

        return document.DocumentNode.InnerText;
    }

    public string GetSandboxCode()
    {
        var lines = File.ReadAllLines( this.FullPath )
            .SkipWhile( l => l.TrimStart().StartsWith( "//", StringComparison.Ordinal ) )
            .ToList();

        // Trim.
        while ( lines.Count > 0 && string.IsNullOrWhiteSpace( lines[0] ) )
        {
            lines.RemoveAt( 0 );
        }

        while ( lines.Count > 0 && string.IsNullOrWhiteSpace( lines[^1] ) )
        {
            lines.RemoveAt( lines.Count - 1 );
        }

        return string.Join( Environment.NewLine, lines );
    }
}