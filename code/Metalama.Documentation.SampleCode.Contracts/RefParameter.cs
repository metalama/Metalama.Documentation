// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;
using System.Text.RegularExpressions;

namespace Doc.Contracts.RefParameter;

public interface IWordCounter
{
    void CountWords(
        string text,
        [NonNegative( Direction = ContractDirection.Both )]
        ref int wordCount );
}

public class WordCounter : IWordCounter
{
    public void CountWords( string text, ref int wordCount )
    {
        var regex = new Regex( @"\b\w+\b" );
        wordCount += regex.Matches( text ).Count;
    }
}