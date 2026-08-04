---
uid: compile-time-dependencies
level: 400
summary: "This article explains how Metalama determines which APIs are available to compile-time code, by restoring and building a small project of its own, how that result is cached, and how to configure and troubleshoot it."
keywords: "compile-time dependencies, reference assemblies, restore, nuget.config, cache, MetalamaCompileTimeTargetFrameworks, MetalamaReferenceAssemblyRestoreTimeout, LAMA0082, LAMA0083"
created-date: 2026-08-02
modified-date: 2026-08-04
---

# Restoring compile-time dependencies

Before it can compile your aspects, Metalama must determine exactly which APIs compile-time code is allowed to call.
It determines that set by generating a small project of its own, restoring it, and building it. This article describes
what that project contains, where its result is cached, how your `nuget.config` files are applied to it, and what to do
when the restore fails.

## Why Metalama restores its own dependencies

Compile-time code, such as aspects, templates, and fabrics, executes inside the compiler instead of executing in your
application. It therefore runs against a different set of APIs than your run-time code: the .NET Standard 2.0 API, the
Roslyn API, and the Metalama API.

Metalama needs to know that set precisely. It reports an error when compile-time code uses an API that isn't
available at compile time, and it compiles the compile-time part of your project against exactly these references.

To obtain the set, Metalama generates a small project of its own, restores it, builds it, and collects the reference
assemblies that the .NET SDK resolved for it. Restoring is the only reliable way to obtain them, because they come
from NuGet packages and from the targeting packs installed on the machine, neither of which Metalama can enumerate on
its own.

## When it runs

The restore runs when the compile-time pipeline of a project is initialized and no usable cached result is available.
This happens during a build and in the IDE alike.

When a usable cached result is available, which is the normal case, nothing is restored, nothing is built, and no
process is started.

## What is built

The generated project is minimal, and isolated from your repository by design:

* It targets the frameworks listed by the `MetalamaCompileTimeTargetFrameworks` property, which are
  `netstandard2.0;net8.0;net48` by default. These are the frameworks that can host the compiler. `netstandard2.0` is
  always required.
* It references the version of `Microsoft.CodeAnalysis.CSharp` that your version of Metalama is built against, plus
  whatever you added through the `MetalamaCompileTimePackage` and `MetalamaCompileTimeAssembly` items.
* It does _not_ import your `Directory.Build.props`, `Directory.Build.targets`, or `Directory.Packages.props`. Your
  build customizations therefore can't influence it, and can't break it.
* It includes a `global.json` requesting the same .NET SDK version as the one that builds your project, when that
  version is known, so that both builds use the same SDK.

The project is built with `dotnet build`, or with `MSBuild.exe` when your own project is built by `MSBuild.exe`
without a .NET SDK. The child process doesn't inherit the .NET SDK and MSBuild environment variables of its parent,
because a host such as an IDE sets them to its own bundled .NET, which doesn't necessarily include an SDK.

## Where the result is cached

The generated project and its result are stored under the Metalama temporary directory, in a path of this form:

```text
<Metalama temporary directory>\AssemblyLocator\<Metalama version>\<hash>
```

The Metalama temporary directory is `%TEMP%\Metalama` on Windows, and the `Temp` subdirectory of the Metalama
application data directory on other platforms. Setting the `METALAMA_TEMP` environment variable moves it to the
`Metalama` subdirectory of the path you give.

The `<hash>` covers everything that can change the outcome:

* the target frameworks;
* the additional compile-time packages and assemblies;
* the version of Roslyn that Metalama targets;
* the additional NuGet sources given by `MetalamaRestoreSources`;
* the content of every `nuget.config` file that applies to your project;
* the `MetalamaAssemblyLocatorSalt` property, whose only purpose is to let you force a new cache entry.

Two projects that agree on all of these share a single cache entry, so a solution normally restores once instead of
once per project. Concurrent builds are safe: the directory is protected by a system-wide lock, so only one build
populates it.

A cached result is reused only if it's still complete, which means that the list of reference assemblies exists, that
every assembly it names is still present on disk, and that the output directory of the generated project still exists.
When any of these is missing, which typically happens after a NuGet cache has been cleared, the project is restored
and built again without further notice.

Note that neither the .NET SDK version nor the `MetalamaAssemblyLocatorHooksDirectory` property is part of the hash.

The directory is deleted by the `metalama cleanup` command once it has been unused for seven days.

## How nuget.config files are merged

The generated project lives outside your repository, so the NuGet configuration of your repository wouldn't apply to
it. Metalama therefore reproduces that configuration.

It collects every `nuget.config` file from the directory of your project up to the root of the volume, merges them
into a single file, and writes that file beside the generated project. Files closer to your project are applied last
and therefore win. Your user-level and machine-level NuGet configuration continues to apply, as it does to any other
project.

The merge follows these rules:

* An element that has no attribute, such as `<packageSources>`, is merged with the element of the same name.
* An element that has a `key` attribute, such as `<add key="..." />`, replaces the element with the same name and key
  accumulated so far, so that the file closest to your project wins.
* A `<clear />` element discards everything accumulated so far, and is itself preserved, so that the system-wide
  configuration is cleared as well.
* Relative paths are rewritten as absolute paths, because the merged file is written in another directory. This
  applies to the sources of `<packageSources>` and `<fallbackPackageFolders>`, and to the `repositoryPath` and
  `globalPackagesFolder` keys of the `<config>` section. URLs, absolute paths, and values that reference an undefined
  environment variable are left unchanged.

`packageSourceMapping` is the most frequent cause of an unexpected failure. It is merged and applied like any other
section, so a pattern routing `Microsoft.CodeAnalysis.*` to a private feed also routes the dependency of the generated
project, which fails when that feed doesn't carry it. Map these packages to a source that provides them, such as
nuget.org.

## Customizing the generated project

When the generated project can't be restored in your environment without additional configuration, the
`MetalamaAssemblyLocatorHooksDirectory` property names a directory from which the generated project imports two
optional files:

| File | Where it is imported |
|------|----------------------|
| `Metalama.AssemblyLocator.Build.props` | Before the `Microsoft.NET.Sdk` props, so that it can set properties that the SDK reads. |
| `Metalama.AssemblyLocator.Build.targets` | After the `Microsoft.NET.Sdk` targets, so that it can define targets or hook into the targets of the SDK. |

Each file is imported only if it exists, and neither is required. The two positions are the same as those of
`Directory.Build.props` and `Directory.Build.targets` in an ordinary project.

```xml
<PropertyGroup>
    <MetalamaAssemblyLocatorHooksDirectory>$(MSBuildThisFileDirectory)metalama-hooks</MetalamaAssemblyLocatorHooksDirectory>
</PropertyGroup>
```

Give the property an absolute path, as above. A relative path is interpreted relative to the generated project, which
is under the Metalama temporary directory and not in your repository.

The generated project assigns some properties itself, after the props file is imported, and a hook therefore can't
change them. `RestoreAdditionalProjectSources`, which comes from `MetalamaRestoreSources`, as well as
`RestoreIgnoreFailedSources`, `TargetFrameworks`, and `LangVersion`, are among them. Set the NuGet sources through
`MetalamaRestoreSources` or through your `nuget.config` instead.

> [!WARNING]
> The hooks directory isn't part of the cache key, so the cached result is still used after you change a hook file.
> Assign a new value to the `MetalamaAssemblyLocatorSalt` property to have the project built again.

## When the restore or the build fails

The generated project is built in a separate process, whose output Metalama captures. When that build fails, Metalama
reports an error of its own and quotes what the build said:

| Diagnostic | Meaning |
|------------|---------|
| `LAMA0082` | The build of the generated project completed with an error. The diagnostic quotes the errors that this build reported and, when the cause is recognizable, names it and the way to resolve it. |
| `LAMA0083` | The build didn't complete within its time budget and was stopped. Raise the budget with the `MetalamaReferenceAssemblyRestoreTimeout` property, expressed in milliseconds, whose default value is `120000`. |

Both diagnostics give the path of an MSBuild binary log of the failed build. Open it with the MSBuild Structured Log
Viewer to see exactly what happened.

These failures are almost always caused by the environment rather than by a defect of Metalama. The most frequent
causes are:

* a NuGet feed requiring credentials, which the separate process can't obtain interactively, so that the credentials
  must be available without user interaction;
* a `packageSourceMapping` rule routing the dependency of the generated project to a feed that doesn't carry it;
* a `global.json` file requesting a .NET SDK version that isn't installed;
* no network access to the configured feeds.

> [!div class="see-also"]
> <xref:msbuild-properties>
> <xref:packages>
> <xref:pipeline>
