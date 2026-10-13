// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Tabs;

namespace BuildMetalamaDocumentation.Markdig.ProjectButtons;

public class ProjectButtonsInline : TabGroupBaseInline
{
    public string Directory { get; set; } = null!;
}