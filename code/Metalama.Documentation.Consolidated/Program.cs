// This is public domain Metalama sample code.

using Metalama.Compiler;
using Metalama.Framework.Aspects;
using Metalama.Framework.Engine.AspectWeavers;
using Metalama.Framework.Introspection;
using Metalama.Framework.Workspaces;
using Metalama.LinqPad;
using System.Reflection;

// This code is executed during build of this project. It copies all assemblies in the current directory to a subdirectory named 'all'.

// This is to make sure that all packages are properly referenced.
_ = new[]
{
    typeof(IAspect),
    typeof(AspectWeaverContext),
    typeof(Workspace),
    typeof(IIntrospectionAspectClass),
    typeof(ISourceTransformer),
    typeof(MetalamaWorkspaceDriver),
    typeof(System.Security.Cryptography.Aes),
    typeof(System.Runtime.Intrinsics.Vector64),
    typeof(System.Formats.Asn1.Asn1Tag),
    typeof(StreamJsonRpc.JsonRpc),
    typeof(System.CommandLine.Command),
    typeof(NuGet.Credentials.CredentialResponse),
    typeof(NuGet.Versioning.SemanticVersion),
    typeof(NuGet.Packaging.Manifest),
    typeof(NuGet.Protocol.ProtocolConstants),
    typeof(NuGet.Resolver.PackageResolver),
    typeof(NuGet.Frameworks.FrameworkException),
    typeof(System.IO.Ports.SerialPort)
};

var currentDirectory = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location )!;

foreach ( var assembly in Directory.GetFiles( currentDirectory, "*.dll", SearchOption.TopDirectoryOnly ) )
{
    Assembly.LoadFile( assembly );
}

var targetDirectory = Path.Combine( currentDirectory, "all" );

if ( Directory.Exists( targetDirectory ) )
{
    Console.WriteLine( $"Cleaning directory '{targetDirectory}'" );

    Directory.Delete( targetDirectory, recursive: true );
}

Directory.CreateDirectory( targetDirectory );

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

foreach ( var assembly in assemblies )
{
    try
    {
        var assemblyLocation = assembly.Location;

        if ( !string.IsNullOrEmpty( assemblyLocation ) && File.Exists( assemblyLocation ) )
        {
            Copy( assemblyLocation );
            
            var pdbFile = Path.ChangeExtension( assemblyLocation, ".pdb" );
            Copy( pdbFile );
            
            var docFile = Path.ChangeExtension( assemblyLocation, ".xml" );
            Copy( docFile );
        }
    }
    catch ( Exception e )
    {
        Console.Error.WriteLine( $"error : Error copying assembly: '{assembly.FullName}'. Exception: '{e.Message}'" );

        return -1;
    }
}

Console.WriteLine( "Finished copying assemblies." );

return 0;

void Copy( string? sourceFile )
{
    if ( !string.IsNullOrEmpty( sourceFile ) && File.Exists( sourceFile ) )
    {
        // Get the file name of the assembly
        var fileName = Path.GetFileName( sourceFile );

        // Create the destination file path
        var destinationPath = Path.Combine( targetDirectory, fileName );

        // Copy the assembly to the target directory
        File.Copy( sourceFile, destinationPath, overwrite: true );

        Console.WriteLine( $"Copied {fileName} to {destinationPath}" );
    }
}