// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;

namespace Doc.LAMA0208.Error;

[RunTimeOrCompileTime]
public class RetryPolicy : ICompileTimeSerializable
{
    public int MaxAttempts { get; set; } = 3;

    public class Serializer : ReferenceTypeSerializer
    {
        public override object CreateInstance( Type type, IArgumentsReader constructorArguments ) => new RetryPolicy();

        public override void SerializeObject( object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => initializationArguments.SetValue( "maxAttempts", ((RetryPolicy) obj).MaxAttempts );

        public override void DeserializeFields( object obj, IArgumentsReader initializationArguments )
            => ((RetryPolicy) obj).MaxAttempts = initializationArguments.GetValue<int>( "maxAttempts" );
    }

    // Error: a second nested type implements ISerializer, so Metalama can't choose between them.
    public class LegacySerializer : ReferenceTypeSerializer
    {
        public override object CreateInstance( Type type, IArgumentsReader constructorArguments ) => new RetryPolicy();

        public override void SerializeObject( object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => initializationArguments.SetValue( "attempts", ((RetryPolicy) obj).MaxAttempts );

        public override void DeserializeFields( object obj, IArgumentsReader initializationArguments )
            => ((RetryPolicy) obj).MaxAttempts = initializationArguments.GetValue<int>( "attempts" );
    }
}
