// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using HtmlAgilityPack;
using PostSharp.Engineering.BuildTools.Search.Crawlers;
using System;
using System.Linq;

namespace BuildMetalamaDocumentation;

internal class MetalamaDocCrawler : DocFxDocumentParser
{
    // This method parses the breadcrumb of an article
    // (eg. Metalama > 🏠 > Conceptual documentation > Creating aspects > Ordering aspects
    // for https://doc.metalama.net/conceptual/aspects/ordering)
    // and calculates all properties required to create a respective instance
    // of the BreadcrumbInfo record.
    protected override BreadcrumbInfo GetBreadcrumbData( string[] breadcrumbLinks )
    {

        var kind = NormalizeCategoryName( breadcrumbLinks[0] );
        
        var relevantBreadCrumbTitles = breadcrumbLinks
            .Skip( 1 )
            .ToArray();

        var breadcrumb = string.Join(
            " > ",
            relevantBreadCrumbTitles );

        var isExamplesKind = kind.Contains( "example", StringComparison.OrdinalIgnoreCase );
        var hasCategory = !isExamplesKind;

        var category = !hasCategory || breadcrumbLinks.Length < 6
            ? null
            : NormalizeCategoryName( breadcrumbLinks.Skip( 5 ).First() );

        var isApiDoc = false;
        var isPageIgnored = false;
        MetalamaDocFxRank kindRank;
       
       if ( isExamplesKind )
        {
            kindRank = MetalamaDocFxRank.Examples;
        }
        else if ( kind.Contains( "concept", StringComparison.OrdinalIgnoreCase ) )
        {
            kindRank = MetalamaDocFxRank.Conceptual;
        }
        else if ( kind.Contains( "api", StringComparison.OrdinalIgnoreCase ) )
        {
            // The PostSharp API migration doc goes to another collection,
            // so it doesn't clutter the search results for Metalama.
            isPageIgnored = category?.Contains( "postsharp", StringComparison.OrdinalIgnoreCase ) ?? false;
            isApiDoc = true;
            kindRank = MetalamaDocFxRank.Api;
        }
        else
        {
            kindRank = MetalamaDocFxRank.Common;
        }

        return new BreadcrumbInfo(
            breadcrumb,
            [kind],
            (int) kindRank,
            category == null ? [] : [category],
            relevantBreadCrumbTitles.Length,
            isPageIgnored,
            isApiDoc );
    }
}