// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace BuildMetalamaDocumentation.Markdig.AspectTests;

internal class HtmlAspectTestInlineRenderer : HtmlObjectRenderer<AspectTestInline>
{
    private static readonly ImmutableHashSet<string> _compileTimeNamespaces = ImmutableHashSet.Create<string>(
        StringComparer.Ordinal,
        "using Metalama.Framework.Aspects",
        "using Metalama.Framework.Code",
        "using Metalama.Framework.Diagnostics",
        "using Metalama.Framework.Engine",
        "using Metalama.Framework.Fabrics",
        "using Metalama.Framework.Project",
        "using Metalama.Framework.Services",
        "using Metalama.Extensions.Validation",
        "using Metalama.Framework.Options",
        "using Metalama.Framework.Metrics",
        "using Metalama.Framework.Eligibility",
        "using Metalama.Framework.Advising",
        "using Metalama.Framework.Serialization",
        "using Metalama.Extensions.CodeFixes" );

    private enum TabOrder
    {
        Aspect,
        Target,
        Introduced,
        Dependency,
        Auxiliary,
        ProgramOutput
    }

    protected override void Write( HtmlRenderer renderer, AspectTestInline obj )
    {
        if ( !File.Exists( obj.Src ) )
        {
            throw new FileNotFoundException( $"The file '{obj.Src}' does not exist." );
        }

        var id = Path.GetFileNameWithoutExtension( obj.Src ).ToLowerInvariant().Replace( '.', '_' );
        var directory = Path.GetDirectoryName( obj.Src )!;

        List<(BaseTab Tab, TabOrder Order)> tabs = [];

        void AddCodeTab(
            string tabId,
            string suffix,
            SandboxFileKind sandboxFileKind,
            DiffSide diffSide,
            string tabHeader,
            TabOrder order )
        {
            var tabPath = suffix == "" ? obj.Src : Path.ChangeExtension( obj.Src, suffix + ".cs" );

            switch ( diffSide )
            {
                case DiffSide.Both:
                    tabs.Add( (new CompareTab( tabId, tabHeader, tabPath ), order) );

                    break;

                case DiffSide.Source:
                    tabs.Add( (new CodeTab( tabId, tabPath, sandboxFileKind ), order) );

                    break;

                case DiffSide.Transformed:
                    tabs.Add( (new TransformedSingleFileCodeTab( tabId, tabPath, tabHeader ), order) );

                    break;
            }
        }

        void AddOtherTab( string extension, Func<string, BaseTab> createTab, TabOrder order )
        {
            var tabPath = Path.ChangeExtension( obj.Src, extension );

            if ( File.Exists( tabPath ) )
            {
                tabs.Add( (createTab( tabPath ), order) );
            }
        }

        foreach ( var file in Directory.GetFiles( directory, $"{id}.*.cs" )
                     .OrderBy( x => x ) )
        {
            if ( file.EndsWith( ".t.cs", StringComparison.Ordinal ) )
            {
                continue;
            }

            var text = File.ReadAllText( file );
            var isCompileTime = _compileTimeNamespaces.Any( ns => text.Contains( ns, StringComparison.Ordinal ) );

            var fileName = Path.GetFileNameWithoutExtension( file );
            var fileNameParts = fileName.Split( '.' );
            var fileKind = fileNameParts[^1];
            var fileSuffix = string.Join( ".", fileNameParts.Skip( 1 ) );

            var (sandboxFileKind, diffSide, tabHeader, order) = (fileKind.ToLowerInvariant(), isCompileTime)
                switch
                {
                    ("i", _) => (SandboxFileKind.None, DiffSide.Transformed,
                                 fileNameParts[1] + " (Introduced)", TabOrder.Introduced), // Introduced code
                    ("dependency", _) => (SandboxFileKind.Incompatible, DiffSide.Both, "Referenced Project",
                                          TabOrder.Dependency),
                    (_, true) => (SandboxFileKind.AspectCode, DiffSide.Source, "Aspect Code", TabOrder.Aspect),
                    (_, false) => (SandboxFileKind.ExtraCode, DiffSide.Source, "Extra Code", TabOrder.ProgramOutput)
                };

            AddCodeTab( fileNameParts[1], fileSuffix, sandboxFileKind, diffSide, tabHeader, order );
        }

        AddCodeTab( "target", "", SandboxFileKind.TargetCode, DiffSide.Both, "Target Code", TabOrder.Target );

        AddOtherTab( ".t.txt", p => new ProgramOutputTab( p ), TabOrder.ProgramOutput );

        var tabGroup = new AspectTestTabGroup( id );
        tabGroup.Tabs.AddRange( tabs.OrderBy( t => (int) t.Order ).ThenBy( t => t.Tab.TabId ).Select( t => t.Tab ) );
        tabGroup.Render( renderer, obj );
    }
}