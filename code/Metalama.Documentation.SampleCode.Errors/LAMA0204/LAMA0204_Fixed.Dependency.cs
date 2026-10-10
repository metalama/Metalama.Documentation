// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;

namespace Doc.LAMA0204.Fixed;

[RunTimeOrCompileTime]
public class LogCategory : ICompileTimeSerializable
{
    public string Name { get; }

    public LogCategory( string name )
    {
        this.Name = name;
    }

    // Fixed: a deserializing constructor that derived types in other projects can call.
    protected LogCategory( IArgumentsReader reader )
    {
        this.Name = reader.GetValue<string>( "Name" ) ?? "";
    }

    // A manual serializer, so Metalama doesn't generate a deserializing constructor.
    public class Serializer : ReferenceTypeSerializer
    {
        public override object CreateInstance( Type type, IArgumentsReader constructorArguments )
            => new LogCategory( constructorArguments );

        public override void SerializeObject( object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => constructorArguments.SetValue( "Name", ((LogCategory) obj).Name );

        public override void DeserializeFields( object obj, IArgumentsReader initializationArguments ) { }
    }
}
