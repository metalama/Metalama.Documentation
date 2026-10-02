// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Markdig.Syntax.Inlines;

namespace BuildMetalamaDocumentation.Markdig.Tabs;

public abstract class TabGroupBaseInline : LeafInline
{
    public string Name { get; set; } = "";

    public string Title { get; set; } = "";

    public string[] Tabs { get; set; } = [];

    public bool AddLinks { get; set; } = true;

    public DiffSide DiffSide { get; set; }
}