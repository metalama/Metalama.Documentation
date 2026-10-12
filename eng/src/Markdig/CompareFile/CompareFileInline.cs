// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Tabs;

namespace BuildMetalamaDocumentation.Markdig.CompareFile;

internal class CompareFileInline : TabGroupBaseInline
{
    public string Src { get; set; } = null!;
}