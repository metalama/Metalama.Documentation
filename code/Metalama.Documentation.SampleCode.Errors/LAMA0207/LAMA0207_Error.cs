// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;

namespace Doc.LAMA0207.Error;

[RunTimeOrCompileTime]
public class LogCategory : ICompileTimeSerializable
{
    public string Name { get; set; } = "Default";

    public class Serializer : ReferenceTypeSerializer
    {
        private readonly string _key;

        // The only constructor of the serializer has a parameter.
        public Serializer( string key )
        {
            this._key = key;
        }

        public override object CreateInstance( Type type, IArgumentsReader constructorArguments ) => new LogCategory();

        public override void SerializeObject( object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => initializationArguments.SetValue( this._key, ((LogCategory) obj).Name );

        public override void DeserializeFields( object obj, IArgumentsReader initializationArguments )
            => ((LogCategory) obj).Name = initializationArguments.GetValue<string>( this._key ) ?? "Default";
    }
}

// Error: the base serializer LogCategory.Serializer has no parameterless constructor.
[RunTimeOrCompileTime]
public class DatabaseLogCategory : LogCategory
{
    public int Level { get; set; }

    public DatabaseLogCategory()
    {
        this.Name = "Database";
    }
}
