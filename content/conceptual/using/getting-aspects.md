---
uid: getting-aspects
level: 100
summary: "The document provides information about using pre-built aspects in projects, including demo aspects and those from the Metalama community, without altering source code."
keywords: "pre-built aspects, source code, NuGet package, Metalama.Documentation.QuickStart, Metalama Marketplace, open-source aspects"
created-date: 2023-03-02
modified-date: 2025-12-07
---
# Getting aspects

This chapter doesn't explore creating aspects. It assumes you already have pre-built aspects available for use in your projects. These aspects may have been provided by your colleagues, our team, or the community.

## Demo aspects

In the examples in this chapter, we'll use the following pre-built aspects:

|Aspect | Purpose |
|-------|----------|
|`Log` | Logs calls to a method. |
|`Retry` | Retries a method multiple times. |
|`NotifyingPropertyChanged` | Implements the `INotifyPropertyChanged` interface. |

The NuGet package that contains these aspects is [Metalama.Documentation.QuickStart](https://www.nuget.org/packages/Metalama.Documentation.QuickStart). Add this package to your projects while following the tutorials in this chapter.

When applied, these aspects change the behavior of your source code without altering the source. They transform the source code before passing it to the compiler.

> [!NOTE]
> The implementation of these aspects isn't the focus of this chapter. Instead, this chapter focuses on how to _use_ them.

## Metalama Marketplace

Don't use demo aspects in real projects. Instead, visit [Metalama Marketplace](https://www.postsharp.net/metalama/marketplace) to find dozens of open-source aspects and extensions.

> [!div class="see-also"]
>
> **See also**
>
> <xref:using-metalama>
> <xref:quickstart-adding-aspects>
> <xref:distributing>
