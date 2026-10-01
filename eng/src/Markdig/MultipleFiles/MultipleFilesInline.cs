// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Tabs;

namespace BuildMetalamaDocumentation.Markdig.MultipleFiles;

public class MultipleFilesInline : TabGroupBaseInline
{
    public string[] Files { get; set; } = [];

    public TabMode Mode { get; set; } = TabMode.Default;
}