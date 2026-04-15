// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Tabs;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation.Markdig.AspectTests;

public class AspectTestInline : TabGroupBaseInline
{
    public string Src { get; set; } = null!;

    public Regex? DiffFilesPattern { get; set; }
}