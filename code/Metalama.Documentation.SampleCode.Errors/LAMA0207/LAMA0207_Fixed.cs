// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;

namespace Doc.LAMA0207.Fixed;

[RunTimeOrCompileTime]
public class LogCategory : ICompileTimeSerializable
{
    public string Name { get; set; } = "Default";

    public class Serializer : ReferenceTypeSerializer
    {
        private readonly string _key;

        public Serializer( string key )
        {
            this._key = key;
        }

        // Fixed: a parameterless constructor that derived serializers can call.
        public Serializer() : this( "Name" ) { }
        public override object CreateInstance( Type type, IArgumentsReader constructorArguments ) => new LogCategory();

        public override void SerializeObject( object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => initializationArguments.SetValue( this._key, ((LogCategory) obj).Name );

        public override void DeserializeFields( object obj, IArgumentsReader initializationArguments )
            => ((LogCategory) obj).Name = initializationArguments.GetValue<string>( this._key ) ?? "Default";
    }
}

[RunTimeOrCompileTime]
public class DatabaseLogCategory : LogCategory
{
    public int Level { get; set; }

    public DatabaseLogCategory()
    {
        this.Name = "Database";
    }
}
