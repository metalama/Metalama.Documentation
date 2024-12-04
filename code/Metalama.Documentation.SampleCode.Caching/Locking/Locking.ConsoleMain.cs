// This is public domain Metalama sample code.

using System;
using Metalama.Documentation.Helpers.ConsoleApp;
using System.Threading.Tasks;

namespace Doc.Locking;

public sealed class ConsoleMain : IConsoleMain
{
    private readonly CloudService _cloudService;

    public ConsoleMain( CloudService cloudService )
    {
        this._cloudService = cloudService;
    }

    public void Execute()
    {
        void ExecuteParallel( Func<byte[]> func )
        {
            var task1 = Task.Run( func );
            var task2 = Task.Run( func );

            Task.WaitAll( task1, task2 );

            Console.WriteLine(
                $"Returned same array: {ReferenceEquals( task1.Result, task2.Result )}" );
        }

        Console.WriteLine( "Without lock" );
        ExecuteParallel( () => this._cloudService.ReadFileWithoutLock( "TheFile.txt" ) );

        Console.WriteLine( "With locks" );
        ExecuteParallel( () => this._cloudService.ReadFileWithLock( "TheFile.txt" ) );
    }
}