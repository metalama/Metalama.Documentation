---
uid: requirements
summary: "Requirements for using Metalama, including build environment, supported IDEs, target frameworks, and version synchronization guidelines."
keywords: "Metalama, .NET SDK, Roslyn-based IDEs, Visual Studio, version synchronization, build environment, compatibility issues, C# features, SDK-style projects, target frameworks"
level: 200
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Requirements

## Build environment

* Install [.NET SDK](https://dotnet.microsoft.com/download) 8.0 or newer.
* Metalama is tested on Windows (x64), Ubuntu (x64), and macOS (ARM). For a list of tested platforms, see <https://github.com/metalama/Metalama.Tests.DotNetSdk/>.

## IDEs

Metalama integrates with Roslyn, making it compatible with any Roslyn-based IDE.

| IDE                                                | Earliest supported version | Design-time correctness | Code fixes | Additional UI features                               |
| -------------------------------------------------- | -------------------------- | ----------------------- | ---------- | ---------------------------------------------------- |
| Visual Studio 2022 _with_ Visual Studio tooling    | 17.12                      | Yes                     | Yes        | Transformed code diff, info bar, syntax highlighting |
| Visual Studio 2022 _without_ Visual Studio tooling | 17.12                      | Yes                     | Yes        |                                                      |
| Rider                                              |                            | Yes                     | Yes        |                                                      |
| Visual Studio Code (C# Dev Kit)                    |                            | Yes                     | Yes        |                                                      |

> [!NOTE]
> Visual Studio Tools for Metalama isn't required but is highly recommended.

## Target frameworks

Only SDK-style projects are supported.

Your projects can target any framework that supports .NET Standard 2.0, including:

| Framework                  | Versions            | Testing status |
| -------------------------- | ------------------- | -------------- |
| .NET                       | 8.0, 9.0, 10.0      | Tested         |
| .NET Framework             | 4.7.2 to 4.8.0      | Tested         |
| MAUI                       |                     | Tested         |
| MAUI Blazor                |                     | Tested         |
| Blazor WebAssembly         |                     | Tested         |
| .NET Core                  | 2.0 or later        | Untested       |
| .NET                       | 5.0, 6.0, 7.0       | Untested       |
| Mono                       | 5.4 or later        | Untested       |
| Xamarin.iOS                | 10.14 or later      | Untested       |
| Xamarin.Mac                | 3.8 or later        | Untested       |
| Xamarin.Android            | 8.0 or later        | Untested       |
| Universal Windows Platform | 10.0.16299 or later | Untested       |

_Untested_ platforms should work because of .NET Standard compatibility, but we don't test them in our continuous integration builds.

For a detailed list of tested target frameworks, <https://github.com/metalama/Metalama.Tests.DotNetSdk/>.

## Synchronizing versions of Metalama, Visual Studio, and .NET SDK

Metalama includes a fork of Roslyn, which comes with Visual Studio. This raises the question of whether you need to synchronize updates of Metalama and your IDEs.

> [!NOTE]
> Even if you're not using Visual Studio, your IDE is still bound to a specific version of Roslyn. Roslyn is a part of the Visual Studio product family. Therefore, the support and versioning policies of your IDE are linked to those of Visual Studio.

To avoid versioning issues, follow these guidelines:

* Update your IDE or .NET SDK at any time without impacting Metalama projects, as long as you don't start using new C# features. Simply updating Visual Studio won't cause issues.
* Before using new C# features in a Metalama project, update Metalama to a version that supports the new C# version. Otherwise, your code may fail to compile.
* Use a version of Visual Studio that's under active [mainstream support](https://learn.microsoft.com/en-us/lifecycle/policies/fixed#mainstream-support) by Microsoft. When a version falls out of support, update to a supported version within three months. Unsupported versions limit you to language features of the last supported C# version below the version you're using, potentially leaving you stuck with an unsupported Metalama version.

We add support for new Roslyn versions within three weeks of their stable release and remove support for obsolete versions no sooner than three months after they fall out of mainstream support by Microsoft.

> [!WARNING]
> We're dedicated to keeping Metalama forward-compatible with future .NET SDK and Visual Studio releases. While we actively address compatibility issues, we can't guarantee that new updates to .NET or Visual Studio won't introduce breaking changes. For a smooth experience, keep your maintenance subscription current and update Metalama alongside your development environment.

For more information on Visual Studio support policies, see [Visual Studio Product Lifecycle and Servicing](https://learn.microsoft.com/en-us/visualstudio/productinfo/vs-servicing) and [Visual Studio Channels and Release Rhythm](https://learn.microsoft.com/en-us/visualstudio/productinfo/release-rhythm).

Here's the rationale behind these guidelines:

> [!NOTE]
> The `Metalama.Compiler` package replaces the C# compiler included in Visual Studio or the .NET SDK. Your code builds against the version of Roslyn that Metalama was built for, regardless of your installed IDE or .NET SDK version. To avoid incompatibilities after .NET SDK updates, `Metalama.Compiler` also includes a backup copy of all Roslyn analyzers normally included in the .NET SDK. If incompatibility occurs, these backup copies replace the ones from your locally installed .NET SDK.

> [!div class="see-also"]
>
> See also
>
> <xref:conceptual>
> <xref:installing>
> <xref:configuration>
