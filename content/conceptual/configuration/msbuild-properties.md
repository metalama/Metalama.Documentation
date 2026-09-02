---
uid: msbuild-properties
level: 300
summary: "This article lists MSBuild properties and environment variables, including their types, descriptions, and default values, related to the Metalama compiler."
keywords: "MSBuild properties, Metalama, environment variables, temporary directory, execution order, transformers, debug transformed code, transformed code files, output path, memory leaks"
created-date: 2023-03-03
modified-date: 2026-08-04
---

# MSBuild properties and environment variables

## Environment variables

| Property                                     | Type                     | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| -------------------------------------------- | ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `METALAMA_TEMP` | String | The root path of Metalama temporary directory. The default value is the result of `Path.GetTempPath()`.

## MSBuild properties

All environment variables are imported as MSBuild properties by default.

| Property                                    | Type                     | Description                                                                                                                                                                                                                                                                                                                                                                          |
|----------------------------------------------|--------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `MetalamaCompilerTransformerOrder`           | Semicolon-separated list | Specifies the execution order of transformers in the current project. Transformers are identified by their namespace-qualified type name, excluding the assembly name. This property is generally unimportant because the only transformer is typically _Metalama.Framework_.                                                                                                                                               |
| `MetalamaDebugTransformedCode`               | Boolean                  | Indicates whether to debug the _transformed_ code instead of the _source_ code. The default value is `False`.                                                                                                                                                                                                                                                                                                      |
| `MetalamaEmitCompilerTransformedFiles`       | Boolean                  | Indicates whether `Metalama.Compiler` should write the transformed code files to disk. The default is `True` if `MetalamaDebugTransformedCode` is enabled, and `False` otherwise.                                                                                                                                                                                                                                          |
| `MetalamaCompilerTransformedFilesOutputPath` | Path                     | Specifies the directory path where transformed code files are written. The default is `obj/$(Configuration)/$(TargetFramework)/metalama` (for example, `obj/LamaDebug/net10.0/metalama`).                                                                                                                                                                                                                                                                                                                                                                                    |
| `MetalamaDebugCompiler`                      | Boolean                  | Specifies whether to attach a debugger to the compiler process. The default value is `False`.                                                                                                                                                                                                                                                                                                                                                                                                           |
| `MetalamaLicense`                            | String                   | Represents a Metalama license key or license server URL. Any license key or license server URL provided this way takes precedence over the license registered via the `metalama` global tool.                                                                                                                                                                                                                                                                                                                 |
| `MetalamaEnabled`                            | Boolean                  | When set to `False`, specifies that _Metalama.Framework_ won't execute in this project, even though the _Metalama.Framework_ package is referenced. It doesn't affect the _Metalama.Compiler_ package.                                                                                                                                                                                                                                                                                                      |
| `MetalamaFormatOutput`                       | Boolean                  | Indicates whether the transformed code should be nicely formatted. The default value is `True` if `MetalamaDebugTransformedCode` is `True` and `False` otherwise. This default is evaluated after the project file, so setting `MetalamaDebugTransformedCode` to `True` in your `.csproj` correctly triggers `MetalamaFormatOutput`. You can also set `MetalamaFormatOutput` explicitly in your project file to override the default. Formatting the transformed code has a performance overhead and should only be performed when the code is being troubleshot or exported. When formatting is enabled, the build emits warning `LAMA0066` as a reminder of this overhead. This warning is expected and harmless. |
| `MetalamaFormatCompileTimeCode`              | Boolean                  | Indicates whether the compile-time code should be nicely formatted. The default value is `False`. Formatting the compile-time code has a performance overhead and should only be performed when the code is being troubleshot or exported.                                                                                                                                                                                                                                                                             |
| `MetalamaCompileTimeProject`                 | Boolean                  | Indicates whether the complete project is compile-time code. This property is set to `True` by the _Metalama.Framework.Sdk_ package. Otherwise, the default value is `False`.                                                                                                                                                                                                                                                                                                                                    |
| `MetalamaDesignTimeEnabled`                  | Boolean                  | Indicates whether the real-time design-time experience is enabled. The default value is `True`, and it can be set to `False` to work around performance issues. When this property is set to `False`, refreshing the IntelliSense cache requires you to rebuild the project.                                                                                                                                                                                                                                     |
| `MetalamaRemoveCompileTimeOnlyCode`          | Boolean                  | Indicates whether Metalama should replace compile-time-only code with `throw new NotSupportedException()` in produced assemblies. The default value is `True` because Metalama normally executes compile-time-only code from the compile-time sub-project embedded as a managed resource in the assembly. This property should be set to `False` in public assemblies referenced by a weaver-style project (using Metalama SDK) because Metalama SDK needs to execute compile-time-only code from the main assembly. |
| `MetalamaCompileTimeTargetFrameworks`        | Semicolon-separated list | Specifies the list of target frameworks for which compile-time projects should be built. The default value covers all native frameworks that are known to host the compiler. Override this property if you don't need all of them and can't install the required .NET targeting packs on the machine. `netstandard2.0` is required. |
| `MetalamaRestoreSources`                     | Semicolon-separated list | Specifies the list of NuGet feeds used when restoring the compile-time project. The default value is `https://api.nuget.org/v3/index.json`. |
| `MetalamaCreateLamaDebugConfiguration`       | Boolean                  | Indicates whether the `LamaDebug` build configuration should be automatically defined (see below). The default value is `True`. |
| `MetalamaTemplateLanguageVersion`            | String                   | Specifies the C# language version (e.g., `10.0`) that's used by templates. Any syntax from higher C# versions isn't allowed in template bodies. Such templates can then be used in projects that use this C# version.
| `MetalamaConcurrentBuildEnabled` | Boolean | Specifies whether Metalama can parallelize work across several cores. The default value is `True`. |
| `MetalamaRoslynIsCompileTimeOnly` | Boolean | Indicates whether types from the `Microsoft.CodeAnalysis` namespaces are considered compile-time-only. The default value is `True`. Set it to `False` if your project uses Roslyn in run-time code. |
| `MetalamaRootDirectory` | Path | Specifies the directory to which the path of the project is made relative when Metalama computes the identifier of the project. The default value is `$(SolutionDir)`. A relative path keeps the build reproducible, because the identifier is a part of the compilation and a full path would make the compiled assembly depend on the directory into which the repository is cloned. Set this property when a project is built from several solutions located in different directories and the build must be reproducible in all of them. When neither this property nor `$(SolutionDir)` is defined, which is the case when a project is built without a solution, only the file name of the project is used. |
| `MetalamaAssemblyLocatorHooksDirectory` | Path | Specifies a directory whose `Metalama.AssemblyLocator.Build.props` and `Metalama.AssemblyLocator.Build.targets` files, if they exist, are imported into the internal project that Metalama builds to determine which APIs are available to compile-time code. There is no default value, and no file is imported unless this property is set. See <xref:compile-time-dependencies>. |
| `MetalamaAssemblyLocatorSalt` | String | Specifies an arbitrary value that is included in the cache key of the project that Metalama builds to resolve the compile-time dependencies. There is no default value. Change it to any other value to have that project restored and built again instead of its cached result being reused. See <xref:compile-time-dependencies>. |
| `MetalamaDiagnoseMemoryLeaks` | Boolean | Indicates whether the objects that your compile-time code leaves behind should be analyzed for references that keep a project snapshot in memory. The default value is `False`. Pass this property on the command line of a single build while you're investigating a memory leak: the analysis walks a large object graph and is too slow for an ordinary build. See <xref:diagnosing-memory-leaks>. |
| `MetalamaCheckSupportedPlatform` | Boolean | Indicates whether the build reports a target framework, a .NET SDK or a Visual Studio version outside the configuration matrix that Metalama supports. The default value is `True`. Set it to `False` to turn off every warning of the check. See [The supported platform check](#the-supported-platform-check) below. |

## MSBuild items

| Item                              | Description                                                                                                                                                                                                                                                                              |
| --------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `MetalamaTransformedCodeAnalyzer` | Represents a list of analyzers that must execute on the transformed code instead of the source code. Items can be set to a namespace or a full type name.                                                                                                                                |
| `MetalamaCompileTimePackage`      | Represents a list of packages accessible from the compile-time code. These packages must explicitly target .NET Standard 2.0 and be included in the project as a `ProjectReference`. By default, only the .NET Standard 2.0 API and the Metalama API are available to compile-time code. |
| `MetalamaExtensionAssembly`       | Loads an assembly as a Metalama extension at compile time. The assembly must contain types exported via <xref:Metalama.Framework.Engine.Extensibility.ExportExtensionAttribute>. Supports `TargetFramework` (e.g., `net472`, `net8.0`) and `TargetRoslynVersion` (e.g., `4.8`, `4.12`, `5.0`) metadata to specify which build of the extension to load. See <xref:sdk-extensions>. |
| `MetalamaCompileTimeAssembly`     | Makes an assembly available to compile-time code. Use this when your compile-time code references types from an external assembly that isn't a NuGet package. See <xref:sdk-extensions>. |
| `MetalamaPlatformRequirement`     | Declares the configuration matrix that a package supports. `Metalama.Framework` declares its own. Add one to declare a stricter requirement for your own package or project. See [The supported platform check](#the-supported-platform-check) below. |
| `MetalamaSupportedPlatformExclusion` | Skips the `MetalamaPlatformRequirement` whose name it repeats, and leaves the other requirements in place. See [The supported platform check](#the-supported-platform-check) below. |

## The supported platform check

Metalama supports a defined set of target frameworks, .NET SDK versions and Visual Studio versions. When your project falls outside that set, the build reports a warning instead of letting you discover the problem later through an obscure failure. The build always continues, and a package whose asset can still be resolved still works, but a problem that is specific to an unsupported configuration will not be fixed.

When you cannot move to a supported configuration, use an earlier version of Metalama that supports it, typically a long-term support (LTS) version. The warning says so as well.

### The warning codes

| Code | Dimension | Reported when |
|------|-----------|---------------|
| `LAMA0600` | Target framework | The target framework of the project is older than, newer than, or outside the supported set. |
| `LAMA0601` | .NET SDK | The .NET SDK that drives the build is older or newer than the supported set. |
| `LAMA0602` | Visual Studio | The build runs on `msbuild.exe` from a version of Visual Studio older than the oldest supported one. |

The Visual Studio dimension is evaluated only when MSBuild runs on .NET Framework, which is the case for `msbuild.exe` and therefore for Visual Studio, the Build Tools and the `VSBuild` task of Azure Pipelines. A build started by `dotnet build` never reports `LAMA0602`.

### Turning the warnings off

Four mechanisms are available, from the broadest to the narrowest.

Setting `MetalamaEnabled` to `False` turns off Metalama in the project, and therefore the check as well.

Setting `MetalamaCheckSupportedPlatform` to `False` turns off the whole check and keeps Metalama enabled:

```xml
<PropertyGroup>
    <MetalamaCheckSupportedPlatform>False</MetalamaCheckSupportedPlatform>
</PropertyGroup>
```

Adding a warning code to `NoWarn` turns off one dimension and leaves the others in place. Write the code in its full prefixed form; a bare number does not match a warning that the compiler did not report:

```xml
<PropertyGroup>
    <NoWarn>$(NoWarn);LAMA0600</NoWarn>
</PropertyGroup>
```

Adding a `MetalamaSupportedPlatformExclusion` item turns off the requirement of a single package and leaves the requirements of the other packages in place. This is the mechanism to use when several packages constrain the same dimension, because they all report the same warning code:

```xml
<ItemGroup>
    <MetalamaSupportedPlatformExclusion Include="Metalama.Patterns.Wpf" />
</ItemGroup>
```

### Declaring a requirement of your own

An aspect library that is narrower than `Metalama.Framework`, for example one that ships no `netstandard2.0` asset or that requires the Windows platform, declares its own requirement. Declare it in the `build\<PackageId>.props` file of your package, and in the same file under `buildTransitive`. A project can also declare one directly in the project file.

```xml
<ItemGroup>
    <MetalamaPlatformRequirement Include="Contoso.Aspects">
        <TargetFrameworkIdentifiers>.NETFramework;.NETCoreApp</TargetFrameworkIdentifiers>
        <MinimumNETFrameworkVersion>4.7.2</MinimumNETFrameworkVersion>
        <MinimumNETCoreAppVersion>10.0</MinimumNETCoreAppVersion>
        <RequiredTargetPlatformIdentifier>windows</RequiredTargetPlatformIdentifier>
        <SupportedTargetFrameworksDescription>The supported target frameworks are net472 and net10.0-windows.</SupportedTargetFrameworksDescription>
        <HelpLink>https://docs.contoso.com/aspects/supported-platforms</HelpLink>
    </MetalamaPlatformRequirement>
</ItemGroup>
```

The value of `Include` is the name of the package that the requirement speaks for. It appears in the warning, so the user learns which package is unsatisfied rather than only that something is.

The metadata entries are the following, and all of them are optional. An absent entry means that the package places no constraint on that dimension.

| Metadata | Meaning |
|----------|---------|
| `TargetFrameworkIdentifiers` | The target framework identifiers that the package supports, separated by semicolons, for example `.NETFramework;.NETCoreApp;.NETStandard`. |
| `MinimumNETFrameworkVersion`, `MaximumNETFrameworkVersion` | The oldest and newest version of .NET Framework, for example `4.7.2`. |
| `MinimumNETStandardVersion`, `MaximumNETStandardVersion` | The oldest and newest version of .NET Standard, for example `2.0`. |
| `MinimumNETCoreAppVersion`, `MaximumNETCoreAppVersion` | The oldest and newest version of .NET, for example `10.0`. |
| `RequiredTargetPlatformIdentifier` | The target platform that the package requires, for example `windows`. |
| `MinimumSdkVersion`, `MaximumSdkVersion` | The oldest and newest version of the .NET SDK, for example `10.0`. |
| `MinimumVisualStudioVersion` | The version of MSBuild that the oldest supported version of Visual Studio carries, for example `18.0` for Visual Studio 2026. There is no maximum, because Visual Studio updates its own feature band. |
| `SupportedTargetFrameworksDescription`, `SupportedSdkVersionsDescription`, `SupportedVisualStudioVersionsDescription`, `SdkUpgradeDescription` | Complete sentences that the warning quotes verbatim. |
| `HelpLink` | The address of an article that describes the supported configurations of the package. |

Three rules govern the item.

Each requirement is evaluated on its own, and each unsatisfied requirement reports its own warning naming its own package. There is no intersection of the lists and no merging of the floors, so a requirement can only add a warning and can never remove one that another package reports.

The item must be declared in a `.props` file. NuGet imports the `build\*.props` of every package before it reads any `.targets` file, so the check sees every contribution whatever the order in which NuGet imports the packages, and whether the contributing package is referenced directly or transitively. A contribution declared in a `.targets` file has no such guarantee.

Metadata that the version of `Metalama.Framework` in use does not know is ignored, and an absent entry means no constraint. An older `Metalama.Framework` with a newer contributing package therefore ignores the item, and a newer `Metalama.Framework` with an older contributing package applies only the constraints that the older package declared.

## MSBuild build configurations

When you import `Metalama.Framework`, a new build configuration named `LamaDebug` is defined unless you set the `MetalamaCreateLamaDebugConfiguration` property to `False`.

The `LamaDebug` configuration assigns the following properties:

```xml
    <PropertyGroup Condition="'$(Configuration)'=='LamaDebug'">
        <MetalamaDebugTransformedCode>True</MetalamaDebugTransformedCode>
    </PropertyGroup>
```

> [!NOTE]
> Because `MetalamaDebugTransformedCode` enables code formatting by default, builds in the `LamaDebug` configuration report warning `LAMA0066` ("Formatting of generated code is enabled. Build performance could be significantly affected."). This warning is expected in this configuration and doesn't indicate a problem.

> [!div class="see-also"]
> <xref:configuration>
> <xref:debugging-aspect-oriented-code>
> <xref:reading-msbuild-properties>
