---
uid: release-notes-2024.0
summary: "Metalama 2024.0 adds support for C# 12, offers multi C# version support, and introduces deterministic build for all Metalama assemblies. Other improvements include published symbol packages, warnings and errors deduplication, and updated licensing."
keywords: "Metalama 2024.0, release notes"
created-date: 2024-07-12
modified-date: 2024-08-22
---

# Metalama 2024.0

The primary goal of this release was to provide support for C# 12. Additionally, it addresses a few remaining tasks from previous releases when we made the Metalama source code available.

## Platform update

* `Metalama.Compiler`: We merged Roslyn 4.8 RTM.
* Support for C# 12:

   * Default lambda parameters,
   * Inline arrays in run-time code,
   * `nameof`: Access to instance members from a static context,
   * Primary constructors,
   * Type aliases,
   * Collection expressions (also known as collection literals) like `[1,2,..array]`,
   * `AppendParameter` advice (utilized in dependency injection scenarios) in primary constructors,
   * Primary constructor parameters in initializer expressions.

## Multi C# version support

Metalama 2024.0 is the first version to support multiple versions of C#.

* Metalama will utilize different C# code generation patterns based on the C# version of the current project. The supported versions are 10, 11, and 12.
* Metalama identifies the version used by each T# template and will report an error if a template is used in a project targeting a lower version of C# than required. The new MSBuild property `MetalamaTemplateLanguageVersion` limits the version of C# that can be used in templates. Define this property if you want to prevent the accidental use of a higher version of C# than intended.
* Currently, there is no way for a template to conditionally generate code patterns according to the C# version of the target project.

## Other improvements

* **Deterministic build** is now implemented for all Metalama assemblies. This feature enables users to verify that the released binaries were indeed built from our source code. The only differences between the official assemblies and your own builds should normally be strong-name and Authenticode signatures. Note that building Metalama from source code requires a source subscription, which is available for an additional fee.
* **Symbol packages**: Now published for all Metalama NuGet packages, allowing for source code debugging via SourceLink.
* **Warnings and errors deduplication**. Currently not supported in the user API.
* **Licensing**: Aspect inheritance is now permitted for all license types.

## Breaking changes

In the <xref:Metalama.Framework.Code.RefKind> enum, `In` and `RefReadOnly` are no longer synonymous.

## In progress

We have been working on the following projects, but they are not yet stable:

* [Metalama.Patterns.Observability](https://github.com/postsharp/Metalama.Patterns/tree/release/2024.0/src/Metalama.Patterns.Observability) is an aspect implementing the `INotifyPropertyChanged` interface. It supports computed properties and child objects.
* [Metalama.Patterns.Xaml](https://github.com/postsharp/Metalama.Patterns/tree/release/2024.0/src/Metalama.Patterns.Xaml) are aspects implementing WPF commands and dependency properties. This package has been renamed to `Metalama.Patterns.Wpf` in Metalama 2024.2.



