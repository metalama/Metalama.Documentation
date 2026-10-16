// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Sandbox;
using System;
using System.Globalization;
using System.Net;
using System.Text;

namespace BuildMetalamaDocumentation.Markdig.Tabs;

internal class CompareTab : BaseTab
{
    public CompareTab( string tabId, string tabHeader, string fullPath ) : base( tabId, fullPath )
    {
        this.TabHeader = tabHeader;
    }

    /// <summary>
    /// Gets the link to the file in the HelpBrowser source browser. When it is set, HelpBrowser adds a "Browse source"
    /// link to the comparison.
    /// </summary>
    public string? SourceBrowserHref { get; init; }

    private CodeTab GetSourceTab() => new( "source", this.FullPath, SandboxFileKind.TargetCode );

    public override string GetTabContent( bool fallbackToSource = true )
    {
        var sourceTab = this.GetSourceTab();
        var transformedTab = new TransformedSingleFileCodeTab( this.TabId, this.FullPath, "(Header)" );

        string NormalizeCode( string s ) => s.Replace( " ", "", StringComparison.Ordinal );

        if ( !transformedTab.Exists() || NormalizeCode( sourceTab.GetCodeForComparison() )
            == NormalizeCode( transformedTab.GetCodeForComparison() ) )
        {
            return sourceTab.GetTabContent( fallbackToSource );
        }

        var stringBuilder = new StringBuilder();
        var sourceBrowserAttribute = this.SourceBrowserHref != null
            ? $@" data-source-href=""{WebUtility.HtmlEncode( this.SourceBrowserHref )}"""
            : "";

        stringBuilder.AppendLine( CultureInfo.InvariantCulture, $@"<div id=""compare-{this.TabId}"" class=""compare""{sourceBrowserAttribute}>" );
        stringBuilder.AppendLine( $@"  <div class=""left"">" );
        stringBuilder.AppendLine( $@"    <div class=""compare-header"">Source Code</div>" );
        stringBuilder.AppendLine( sourceTab.GetTabContent( false ) );
        stringBuilder.AppendLine( $@"    </div>" ); // <div class="left">
        stringBuilder.AppendLine( $@"  <div class=""right"">" );
        stringBuilder.AppendLine( $@"    <div class=""compare-header"">Transformed Code</div>" );
        stringBuilder.AppendLine( transformedTab.GetTabContent( false ) );
        stringBuilder.AppendLine( $@"    </div>" ); // <div class="right">
        stringBuilder.AppendLine( $@"</div>" );     // top div

        return stringBuilder.ToString();
    }

    public string GetSandboxCode() => this.GetSourceTab().GetSandboxCode();

    protected override string TabHeader { get; }
}