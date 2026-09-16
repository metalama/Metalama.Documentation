// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Newtonsoft.Json;
using PKT.LZStringCSharp;
using System.Collections.Generic;

namespace BuildMetalamaDocumentation.Markdig.Sandbox;

public class SandboxPayload
{
    [JsonProperty( PropertyName = "f" )]
    public IReadOnlyList<SandboxFile> Files { get; }

    public SandboxPayload( IReadOnlyList<SandboxFile> files )
    {
        this.Files = files;
    }

    public string ToCompressedString()
    {
        var tryPayloadJson = JsonConvert.SerializeObject( this );

        return LZString.CompressToEncodedURIComponent( tryPayloadJson );
    }
}