using BuildMetalamaDocumentation.Markdig.Helpers;
using Markdig.Helpers;

namespace BuildMetalamaDocumentation.UnitTests;

public class StringSliceExtensionsTests
{
    [Theory]
    [InlineData( "", new string[0] )]
    [InlineData( "key=value", new[] { "key=value" } )]
    [InlineData( "quoted=\"quoted value\"", new[] { "quoted=quoted value" } )]
    [InlineData( "unnamed", new[] { "<null>=unnamed" } )]
    [InlineData( "key=value unnamed", new[] { "key=value", "<null>=unnamed" } )]
    [InlineData( "key=value quoted=\"quoted value\"", new[] { "key=value", "quoted=quoted value" } )]
    [InlineData( "key1=value1 key2=value2", new[] { "key1=value1", "key2=value2" } )]
    public void MatchArgumentMatchesArguments( string input, IEnumerable<string> expectedArguments )
    {
        var slice = new StringSlice( input );

        List<string> actualArguments = new();

        while ( slice.MatchArgument( out var name, out var value ) )
        {
            actualArguments.Add( $"{name ?? "<null>"}={value ?? "<null>"}" );
        }

        Assert.Equal( expectedArguments, actualArguments );
    }

    [Theory]
    [InlineData( "key=\"value" )]
    public void MatchArgumentThrowsException( string input )
    {
        var slice = new StringSlice( input );

        Assert.Throws<InvalidOperationException>( () => slice.MatchArgument( out var _, out var _ ) );
    }
}