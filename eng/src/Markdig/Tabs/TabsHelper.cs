// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Linq;

namespace BuildMetalamaDocumentation.Markdig.Tabs;

public static class TabsHelper
{
    public static string[] SplitTabs( string tabs ) => tabs.Split( ',' ).Select( x => x.Trim() ).Where( x => !string.IsNullOrEmpty( x ) ).ToArray();
}