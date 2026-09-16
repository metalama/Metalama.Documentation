// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BuildMetalamaDocumentation.Markdig.Sandbox;

[JsonConverter( typeof(StringEnumConverter) )]
public enum SandboxFileKind
{
    [EnumMember( Value = "!none" )]
    None,

    [EnumMember( Value = "!incompatible" )]
    Incompatible,

    [EnumMember( Value = "t" )]
    TargetCode,

    [EnumMember( Value = "a" )]
    AspectCode,

    [EnumMember( Value = "e" )]
    ExtraCode
}