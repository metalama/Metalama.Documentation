// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System.IO;

namespace BuildMetalamaDocumentation.Markdig.MultipleFiles;

public class HtmlMultipleFilesInlineRenderer : HtmlObjectRenderer<MultipleFilesInline>
{
    protected override void Write( HtmlRenderer renderer, MultipleFilesInline obj )
    {
        var directory = Path.GetDirectoryName( obj.Files[0] )!;
        var tabGroup = new DirectoryTabGroup( obj.Name.Replace( '.', '_' ), directory );

        foreach ( var file in obj.Files )
        {
            var tabId = Path.GetFileNameWithoutExtension( file );
            var tabName = Path.GetFileName( file );

            switch ( obj.Mode )
            {
                case TabMode.Source:
                    tabGroup.Tabs.Add( new CodeTab( tabId, file, SandboxFileKind.Incompatible ) );

                    break;

                case TabMode.Default:
                case TabMode.Diff:
                    tabGroup.Tabs.Add( new CompareTab( tabId, tabName, file ) );

                    break;

                case TabMode.Transformed:
                    tabGroup.Tabs.Add( new TransformedSingleFileCodeTab( tabId, file, tabName + " (Transformed)" ) );

                    break;
            }
        }

        tabGroup.Render( renderer, obj );
    }
}