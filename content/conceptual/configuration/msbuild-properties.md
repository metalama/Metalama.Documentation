---
uid: msbuild-properties
level: 300
---

# MSBuild configuration properties and items

## Properties

| Property                               | Type                      | Description                                                                                                                                         |
|----------------------------------------|---------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| `MetalamaCompilerTransformerOrder`     | Semicolon-separated list | Specifies the execution order of transformers in the current project. Transformers are identified by their namespace-qualified type name but without the assembly name. This property is generally unimportant because the only transformer is typically _Metalama.Framework_. |
| `MetalamaDebugTransformedCode`         | Boolean                   | Indicates that you want to debug the _transformed_ code instead of the _source_ code. The default value is `False`.                                 |
| `MetalamaEmitCompilerTransformedFiles` | Boolean                   | Indicates that `Metalama.Compiler` should write the transformed code files to disk. The default is `True` if `MetalamaDebugTransformedCode` is enabled and `False` otherwise. |
| `MetalamaCompilerTransformedFilesOutputPath` | Path                  | Specifies the directory path where the transformed code files are written. The default is `obj/$(Configuration)/metalama`.                           |
| `MetalamaDebugCompiler`                | Boolean                   | Specifies that you want to attach a debugger to the compiler process. The default value is `False`.                                                 |
| `MetalamaLicense`                      | String                    | Represents a Metalama license key or license server URL. Any license key or license server URL provided this way takes precedence over the license registered via the `metalama` global tool. |
| `MetalamaEnabled`                      | Boolean                   | When set to `False`, specifies that _Metalama.Framework_ should not execute in this project, although the _Metalama.Framework_ package is referenced in the project. It does not affect the _Metalama.Compiler_ package. |
| `MetalamaFormatOutput`                 | Boolean                   | Indicates that the transformed code should be nicely formatted. The default value is `False`. Formatting the transformed code has a performance overhead and should only be performed when the code is troubleshot or exported. |
| `MetalamaFormatCompileTimeCode`        | Boolean                   | Indicates that the compile-time code should be nicely formatted. The default value is `False`. Formatting the compile-time code has a performance overhead and should only be performed when the code is troubleshot or exported. |
| `MetalamaCompileTimeProject`           | Boolean                   | Indicates that the complete project is compile-time code. This property is set to `True` by the _Metalama.Framework.Sdk_ package. Otherwise, the default value is `False`. |
| `MetalamaDesignTimeEnabled`            | Boolean                   | Indicates that the real-time design-time experience is enabled. The default value is `True`, and it can be set to `False` to work around performance issues. When this property is set to `False`, refreshing the IntelliSense cache requires you to rebuild the project. |
| `MetalamaRemoveCompileTimeOnlyCode`    | Boolean                   | Indicates that Metalama should replace compile-time-only code with `throw NotSupportedException()` in produced assemblies. The default value is `True` because Metalama normally executes compile-time-only code from the compile-time sub-project embedded as a managed resource in the assembly. This property should be set to `False` in public assemblies referenced by a weaver-style project (using Metalama SDK) because Metalama SDK needs to execute compile-time-only code from the main assembly. |

## Items

| Item                           | Description                                                                                                                                        |
|--------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| `MetalamaTransformedCodeAnalyzer` | Represents a list of analyzers that must execute on the transformed code instead of the source code. Items can be set to a namespace or a full type name. |
| `MetalamaCompileTimePackage`   | Represents a list of packages accessible from the compile-time code. These packages must explicitly target .NET Standard 2.0 and be included in the project as a `ProjectReference`. By default, only the .NET Standard 2.0 API and the Metalama API are available to compile-time code. |

## Build configurations

When you import the `Metalama.Framework`, a new build configuration named `LamaDebug` is defined. It assigns the following properties:

```xml
    <PropertyGroup Condition="'$(Configuration)'=='LamaDebug'">
        <MetalamaFormatOutput>True</MetalamaFormatOutput>
        <MetalamaDebugTransformedCode>True</MetalamaDebugTransformedCode>
        <MetalamaEmitCompilerTransformedFiles>True</MetalamaEmitCompilerTransformedFiles>
    </PropertyGroup>
```


