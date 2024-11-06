---
uid: requirements
summary: "The document provides the requirements for using Metalama, a tool that integrates with Roslyn-based IDEs. It details the build environment, supported IDEs, target frameworks, and guidelines for synchronizing versions of Metalama, Visual Studio and .NET SDK."
keywords: "Metalama, .NET SDK, Roslyn-based IDEs, Visual Studio, version synchronization, build environment, compatibility issues, C# features, SDK-style projects, target frameworks"
created-date: 2023-01-26
modified-date: 2024-08-22
---

# Requirements

## Build environment

* The [.NET SDK](https://dotnet.microsoft.com/download) 6.0 or newer must be installed.
* Metalama has been tested on Windows (x64), Ubuntu (x64), and macOS (ARM).

## IDEs

Metalama integrates with Roslyn, so it is theoretically compatible with any Roslyn-based IDE.

| IDE                                                | Earliest supported version | Design-Time Correctness | Code Fixes | Additional UI Features                               |
| -------------------------------------------------- | -------------------------- | ----------------------- | ---------- | ---------------------------------------------------- |
| Visual Studio 2022 _with_ Visual Studio tooling    | 17.6                       | Yes                     | Yes        | Transformed code diff, info bar, syntax highlighting |
| Visual Studio 2022 _without_ Visual Studio tooling | 17.6                       | Yes                     | Yes        |                                                      |
| Rider                                              |                            | Yes                     | Yes        |                                                      |
| Visual Studio Code (C# Dev Kit)                    |                            | Yes                     | Yes        |                                                      |

> [!NOTE]
> While using Visual Studio, the utilization of Visual Studio Tools for Metalama and PostSharp is not mandatory but is highly recommended.

## Target frameworks

Only SDK-style projects are supported.

Your projects can target any framework that supports .NET Standard 2.0, including:

| Framework                  | Versions            | Testing status |
| -------------------------- | ------------------- | -------------- |
| .NET Core                  | 2.0 or later        | Untested       |
| .NET                       | 5.0                 | Untested       |
| .NET                       | 6.0 to 9.0          | Tested         |
| .NET Framework             | 4.7.2 to 4.8.0      | Tested         |
| Mono                       | 5.4 or later        | Untested       |
| Xamarin.iOS                | 10.14 or later      | Untested       |
| Xamarin.Mac                | 3.8 or later        | Untested       |
| Xamarin.Android            | 8.0 or later        | Untested       |
| Universal Windows Platform | 10.0.16299 or later | Untested       |
| MAUI                       |                     | Tested         |
| MAUI Blazor                |                     | Tested         |
| Blazor WebAssembly         |                     | Tested         |

_Untested_ means that the platform _should_ work because of .NET Standard compatibility, but we are not testing the platform as a part of our continuous integration builds.

## Synchronizing versions of Metalama, Visual Studio, and .NET SDK

Since Metalama includes a fork of Roslyn, which comes with Visual Studio, you might question whether you need to synchronize the updates of Metalama and your IDEs.

> [!NOTE]
> Even if you're not using Visual Studio, your IDE is still bound to a specific version of Roslyn. Roslyn is a part of the Visual Studio product family. Therefore, the support and versioning policies of your IDE are linked to those of Visual Studio.

To avoid versioning issues, consider the following suggestions:

* You can update your IDE or .NET SDK at any time without impacting Metalama projects, provided you do not start using new C# features in Metalama projects. In other words, merely updating Visual Studio should not cause any issues.
* Before you begin using new C# features in a Metalama project, ensure you update Metalama to a version that supports the new C# version. If you do not update, your code may fail to compile.
* Always use a version of Visual Studio that is under active [mainstream support](https://docs.microsoft.com/en-us/lifecycle/policies/fixed#mainstream-support) by Microsoft. When a version of Visual Studio falls out of support, update to a supported version within three months. If you use an unsupported version, you will only be able to use the language features of the last supported C# version _below_ the version that you are using. If you do not use a supported version of Visual Studio, you may be stuck with an unsupported version of Metalama.

As per our policy, we make our best effort to add support for new Roslyn versions no later than three weeks after their stable release and to remove support for obsolete versions no sooner than three months after they fall out of mainstream support by Microsoft.

> [!WARNING]
> We're dedicated to keeping Metalama forward-compatible with future .NET SDK and Visual Studio releases. While we actively address compatibility issues, we cannot guarantee that new updates to .NET or Visual Studio won't introduce breaking changes. For a smooth experience, keep your maintenance subscription current and update Metalama alongside your development environment.

For more information on the support policies of Visual Studio, see [Visual Studio Product Lifecycle and Servicing](https://docs.microsoft.com/en-us/visualstudio/productinfo/vs-servicing) and [Visual Studio Channels and Release Rhythm](https://docs.microsoft.com/en-us/visualstudio/productinfo/release-rhythm).

The rationale behind these suggestions is as follows:

> [!NOTE]
> The `Metalama.Compiler` package replaces the C# compiler included in Visual Studio or the .NET SDK. Therefore, your code will build against the version of Roslyn that Metalama was built for, regardless of the installed version of your IDE or the .NET SDK. To avoid incompatibilities after updates of the .NET SDK, the `Metalama.Compiler` package also includes a backup copy of all Roslyn analyzers normally included in the .NET SDK. In case of incompatibility, these backup copies will be used instead of the ones provided by your locally installed .NET SDK.
