---
uid: reading-msbuild-properties
level: 200
summary: "The document provides a guide on how to read MSBuild properties using the IProject.TryGetProperty method in Metalama. It explains how to expose, set, and read MSBuild properties, and how to combine them with the options API."
keywords: "MSBuild properties, IProject.TryGetProperty, Metalama, configuration, command line, Directory.Build.props, compiler, NuGet package, csproj file, options API"
created-date: 2024-08-04
modified-date: 2025-11-30
---

# Reading MSBuild properties

In addition to or as an alternative to a programmatic configuration API, an aspect can accept configuration by reading MSBuild properties using the <xref:Metalama.Framework.Project.IProject.TryGetProperty*?text=IProject.TryGetProperty> method.

This strategy enables the aspect to be configured without modifying the source code. This can be useful when you want the aspect to behave differently according to a property supplied from the command line, for example.

Another advantage of accepting MSBuild properties for configuration is that they can be defined in `Directory.Build.props` and shared among all projects in the repository. For more details, refer to [Customize the build by folder](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory) in the Visual Studio documentation.

## Exposing MSBuild properties

By default, MSBuild properties are not visible to Metalama: you must instruct MSBuild to pass them to the compiler using the `CompilerVisibleProperty` item.

If you're shipping your project as a NuGet package, we recommend the following approach to consume a configuration property:

1. Create a file named `build/YourProject.props`.

    > [!WARNING]
    > The file name must exactly match the name of your package.

    ```xml
    <Project>
        <ItemGroup>
            <CompilerVisibleProperty Include="YourProperty" />
        </ItemGroup>
    </Project>
    ```

2. Create a second file named `buildTransitive/YourProject.props`.

    ```xml
    <Project>
     <Import Project="../build/YourProject.props"/>
    </Project>
    ```

3. Include both `YourProject.props` in your project and mark it for inclusion in your NuGet package, respectively. Your `csproj` file should look like this:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
        <ItemGroup>
            <None Include="build/*">
                <Pack>true</Pack>
                <PackagePath></PackagePath>
            </None>
            <None Include="buildTransitive/*">
                <Pack>true</Pack>
                <PackagePath></PackagePath>
            </None>
        </ItemGroup>
        <!-- ... -->
    </Project>
    ```

This approach will make sure that `YourProject.props` is automatically included in any project that references your project using a `PackageReference`.

However, this won't work for projects referencing your project using a `ProjectReference`. In this case, you need to manually import the `YourProject.props` file using the following code:

```xml
<Import Project="../YourProject/build/YourProject.props"/>
```

## Setting MSBuild properties

To configure the aspect, users should set this property using one of the following approaches:

1. By modifying the `csproj` file, as shown in the following snippet:

   ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
        <PropertyGroup>
            <YourProperty>TheValue</YourProperty>
        </PropertyGroup>
        <!-- ... -->
    </Project>
    ```

     > [!WARNING]
     > Line breaks and semicolons are not allowed in the values of compiler-visible properties as they can cause your aspect to receive an incorrect value.

2. From the command line, using the `-p:PropertyName=PropertyValue` command-line argument to `dotnet` or `msbuild`.

3. By setting an environment variable. See the [MSBuild documentation](https://learn.microsoft.com/en-us/visualstudio/msbuild/how-to-use-environment-variables-in-a-build) for details.

## Reading MSBuild properties from an aspect or fabric

To read an MSBuild property, use the <xref:Metalama.Framework.Project.IProject.TryGetProperty*?text=IProject.TryGetProperty> method. The <xref:Metalama.Framework.Project.IProject> object is available almost everywhere. If you've an <xref:Metalama.Framework.Code.IDeclaration>, use <xref:Metalama.Framework.Code.ICompilation.Project?text=declaration.Compilation.Project>.

### Example: reading MSBuild properties from an aspect

In the example below, the `Log` aspect reads the default category from the MSBuild property.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConsumingProperty.cs name="Consuming Property"]

## Combining MSBuild properties with the options API

Whenever your aspect library relies on both MSBuild properties and a configuration API, it is recommended to integrate the MSBuild properties with your option class instead of reading the properties directly from the aspect classes.

Instead, properties should be read from the <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*> method of your option class. This method receives a parameter of type <xref:Metalama.Framework.Options.OptionsInitializationContext>, which exposes the <xref:Metalama.Framework.Project.IProject> interface, and allows you to read the properties. The object also lets you report errors or warnings if the properties have an invalid value. Thanks to this approach, you can make the default options dependent on MSBuild properties.

### Examples: building default options from MSBuild properties

In the following example, the options class implements the <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*> to read default values from the MSBuild properties. It reports a diagnostic if their value is incorrect.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration_ProjectDefault.Options.cs]

> [!div class="see-also"]
> <xref:aspect-configuration>
> <xref:exposing-options>
> <xref:Metalama.Framework.Project.IProject.TryGetProperty*>
> <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*>
