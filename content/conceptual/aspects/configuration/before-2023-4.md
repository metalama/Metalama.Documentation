---
uid: exposing-configuration-before-2023-4
level: 300
summary: "Exposing configuration prior to Metalama 2023.4, including constructing a class, overriding methods, and creating an extension method."
keywords: "configuration API, Metalama 2023.4, class inheritance, overriding methods, extension method, IProject, project fabric, aspect code, CompileTime, ProjectExtension"
created-date: 2024-08-04
modified-date: 2025-11-30
---

# Exposing configuration (before 2023.4)

> [!NOTE]
> Starting with Metalama 2023.4, this approach is considered obsolete.

To expose a configuration API prior to Metalama 2023.4:

1. Create a class that inherits from <xref:Metalama.Framework.Project.ProjectExtension> and includes a default constructor.
2. If necessary, override the <xref:Metalama.Framework.Project.ProjectExtension.Initialize*> method, which accepts an <xref:Metalama.Framework.Project.IProject>.
3. In your aspect code, call the <xref:Metalama.Framework.Project.IProject.Extension*?text=IProject.Extension&lt;T&gt;()> method, where `T` represents your configuration class, to get the configuration object.
4. Optionally, create an extension method for the <xref:Metalama.Framework.Project.IProject> type to make your configuration API more discoverable. Annotate the class with `[CompileTime]`.
5. To configure your aspect, users should implement a project fabric and access your configuration API using this extension method.

## Example

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs name="Consuming Property"]

> [!div class="see-also"]
> <xref:aspect-configuration>
> <xref:exposing-options>
> <xref:Metalama.Framework.Project.ProjectExtension>
