---
uid: msbuild-properties
level: 300
summary: "This article lists MSBuild properties and environment variables, including their types, descriptions, and default values, related to the Metalama compiler."
keywords: "MSBuild properties, Metalama, environment variables, temporary directory, execution order, transformers, debug transformed code, transformed code files, output path"
created-date: 2023-03-03
modified-date: 2026-08-02
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
| `MetalamaCompileTimeTargetFrameworks`        | Semicolon-separated list | Specifies the list of target frameworks for which compile-time projects should be built. The default value covers all native frameworks that are known to host the compiler. Override this property if you don't need all of them and cannot install the required .NET targeting packs on the machine. `netstandard2.0` is required. |
| `MetalamaRestoreSources`                     | Semicolon-separated list | Specifies the list of NuGet feeds used when restoring the compile-time project. The default value is `https://api.nuget.org/v3/index.json`. |
| `MetalamaCreateLamaDebugConfiguration`       | Boolean                  | Indicates whether the `LamaDebug` build configuration should be automatically defined (see below). The default value is `True`. |
| `MetalamaTemplateLanguageVersion`            | String                   | Specifies the C# language version (e.g., `10.0`) that's used by templates. Any syntax from higher C# versions isn't allowed in template bodies. Such templates can then be used in projects that use this C# version.
| `MetalamaConcurrentBuildEnabled` | Boolean | Specifies whether Metalama can parallelize work across several cores. The default value is `True`. |
| `MetalamaRoslynIsCompileTimeOnly` | Boolean | Indicates whether types from the `Microsoft.CodeAnalysis` namespaces are considered compile-time-only. The default value is `True`. Set it to `False` if your project uses Roslyn in run-time code. |
| `MetalamaRootDirectory` | Path | Specifies the directory to which the path of the project is made relative when Metalama computes the identifier of the project. The default value is `$(SolutionDir)`. A relative path keeps the build reproducible, because the identifier is a part of the compilation and a full path would make the compiled assembly depend on the directory into which the repository is cloned. Set this property when a project is built from several solutions located in different directories and the build must be reproducible in all of them. When neither this property nor `$(SolutionDir)` is defined, which is the case when a project is built without a solution, only the file name of the project is used. |
| `MetalamaAssemblyLocatorHooksDirectory` | Path | Specifies a directory whose `Metalama.AssemblyLocator.Build.props` and `Metalama.AssemblyLocator.Build.targets` files, if they exist, are imported into the internal project that Metalama builds to determine which APIs are available to compile-time code. There is no default value, and no file is imported unless this property is set. See below. |
| `MetalamaAssemblyLocatorSalt` | String | Specifies an arbitrary value that is included in the cache key of the internal project described below. There is no default value. Change it to any other value to have that project restored and built again instead of its cached result being reused. |

## MSBuild items

| Item                              | Description                                                                                                                                                                                                                                                                              |
| --------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `MetalamaTransformedCodeAnalyzer` | Represents a list of analyzers that must execute on the transformed code instead of the source code. Items can be set to a namespace or a full type name.                                                                                                                                |
| `MetalamaCompileTimePackage`      | Represents a list of packages accessible from the compile-time code. These packages must explicitly target .NET Standard 2.0 and be included in the project as a `ProjectReference`. By default, only the .NET Standard 2.0 API and the Metalama API are available to compile-time code. |
| `MetalamaExtensionAssembly`       | Loads an assembly as a Metalama extension at compile time. The assembly must contain types exported via <xref:Metalama.Framework.Engine.Extensibility.ExportExtensionAttribute>. Supports `TargetFramework` (e.g., `net472`, `net8.0`) and `TargetRoslynVersion` (e.g., `4.8`, `4.12`, `5.0`) metadata to specify which build of the extension to load. See <xref:sdk-extensions>. |
| `MetalamaCompileTimeAssembly`     | Makes an assembly available to compile-time code. Use this when your compile-time code references types from an external assembly that isn't a NuGet package. See <xref:sdk-extensions>. |

## Customizing the internal reference-assembly project

To determine which APIs are available to compile-time code, Metalama generates a project under its temporary
directory, restores it and builds it. That project references `Microsoft.CodeAnalysis.CSharp` and targets the
frameworks given by `MetalamaCompileTimeTargetFrameworks`.

The generated project is deliberately isolated from the build customizations of your repository. It inherits your
`nuget.config`, but it sets `ImportDirectoryBuildProps`, `ImportDirectoryBuildTargets` and
`ImportDirectoryPackagesProps` to `False`, so your `Directory.Build.props`, `Directory.Build.targets` and
`Directory.Packages.props` do not apply to it and cannot break it.

That isolation is occasionally too strict, typically in an environment where the restore of this project cannot
succeed without additional configuration. The `MetalamaAssemblyLocatorHooksDirectory` property names a directory from
which the generated project imports two optional files:

| File | Where it is imported |
|------|----------------------|
| `Metalama.AssemblyLocator.Build.props` | Before the `Microsoft.NET.Sdk` props, so that it can set properties that the SDK reads. |
| `Metalama.AssemblyLocator.Build.targets` | After the `Microsoft.NET.Sdk` targets, so that it can define targets or hook into the targets of the SDK. |

Each file is imported only if it exists, and neither is required. The two positions are the same as those of
`Directory.Build.props` and `Directory.Build.targets` in an ordinary SDK-style project.

The following example gives the internal project a NuGet packages folder of its own:

```xml
<PropertyGroup>
    <MetalamaAssemblyLocatorHooksDirectory>$(MSBuildThisFileDirectory)metalama-hooks</MetalamaAssemblyLocatorHooksDirectory>
</PropertyGroup>
```

`metalama-hooks/Metalama.AssemblyLocator.Build.props`:

```xml
<Project>
    <PropertyGroup>
        <RestorePackagesPath>C:\packages\metalama-compile-time</RestorePackagesPath>
    </PropertyGroup>
</Project>
```

Give the property an absolute path, as in the example above. A relative path is interpreted relative to the generated
project, which lives under Metalama's temporary directory and not in your repository.

Note that the generated project assigns some properties itself, after the props file is imported and therefore
overriding it. `RestoreAdditionalProjectSources`, which is set from `MetalamaRestoreSources`, and
`RestoreIgnoreFailedSources` are among them, as are `TargetFrameworks` and `LangVersion`. Set the NuGet sources of the
internal project through `MetalamaRestoreSources` or through your `nuget.config` rather than through a hook.

When the property is set, the generated project reports a warning for each file that it imported. Those warnings
belong to the build of the internal project, whose output Metalama captures, so you see them only when that build
fails and Metalama reports what it said.

> [!WARNING]
> The result of this build is cached, and `MetalamaAssemblyLocatorHooksDirectory` is not a part of the cache key.
> After you change a hook file, the cached result is still used. Assign a new value to the
> `MetalamaAssemblyLocatorSalt` property, which is a part of the cache key, to have the internal project built again.

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
