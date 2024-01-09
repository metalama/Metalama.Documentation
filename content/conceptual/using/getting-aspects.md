---
uid: getting-aspects
level: 100
summary: "The document provides information about using pre-built aspects in projects, including demo aspects and those from the Metalama community, without altering source code."
---
# Getting aspects

As previously noted, this chapter will not explore the creation of aspects. We will assume that you already have pre-built aspects available for use in your projects. These aspects may have been provided by your colleagues, our team, or the community.

## Demo aspects

In the examples provided in this chapter, we will utilize the following pre-built aspects:

|Aspect | Purpose |
|-------|----------|
|`Log` | For logging calls to a method. |
|`Retry` | For retrying a method multiple times. |
|`NotifyingPropertyChanged` | For easy implementation of the `INotifyPropertyChanged` interface. |

The NuGet package that contains these aspects is [Metalama.Documentation.QuickStart](https://www.nuget.org/packages/Metalama.Documentation.QuickStart). You can add this package to your projects while following the tutorials in this chapter.

When applied, these aspects change the behavior of your source code without altering the source level. They transform the source code before it is passed to the compiler.

> [!NOTE]
> The implementation of these aspects is not the focus of this chapter. Instead, we will focus on how to _use_ them.

## Metalama Marketplace

Don't use the demo aspects in real projects. Instead, [Metalama Marketplace](https://www.postsharp.net/metalama/marketplace) and find dozens of open-source aspects and extensions.




