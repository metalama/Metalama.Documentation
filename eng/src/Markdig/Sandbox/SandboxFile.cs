// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Newtonsoft.Json;

namespace BuildMetalamaDocumentation.Markdig.Sandbox;

public class SandboxFile
{
    [JsonProperty( PropertyName = "n" )]
    public string Name { get; }

    [JsonProperty( PropertyName = "c" )]
    public string Content { get; }

    [JsonProperty( PropertyName = "k" )]
    public SandboxFileKind Kind { get; }

    public SandboxFile( string name, string content, SandboxFileKind kind )
    {
        this.Name = name;
        this.Content = content;
        this.Kind = kind;
    }
}