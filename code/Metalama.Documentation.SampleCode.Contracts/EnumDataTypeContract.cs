// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;
using System;

namespace Doc.EnumDataTypeContract;

public class Message
{
    [EnumDataType( typeof(ConsoleColor) )]
    public string? Color { get; set; }
}