// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;

namespace Doc.LAMA0204.Error;

[RunTimeOrCompileTime]
public class LogCategory : ICompileTimeSerializable
{
    public string Name { get; }

    // LogCategory has neither a parameterless constructor nor a deserializing constructor.
    public LogCategory( string name )
    {
        this.Name = name;
    }

    // A manual serializer, so Metalama doesn't generate a deserializing constructor.
    public class Serializer : ReferenceTypeSerializer
    {
        public override object CreateInstance( Type type, IArgumentsReader constructorArguments )
            => new LogCategory( constructorArguments.GetValue<string>( "Name" ) ?? "" );

        public override void SerializeObject( object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => constructorArguments.SetValue( "Name", ((LogCategory) obj).Name );

        public override void DeserializeFields( object obj, IArgumentsReader initializationArguments ) { }
    }
}
