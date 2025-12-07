---
uid: distributing
summary: "The document provides instructions on how to manage aspect usage in projects, either by allowing it to flow to other projects or preventing its transitive use with the help of Metalama.Framework and Metalama.Framework.Redist packages."
level: 200
keywords: "aspect usage, transitive use, Metalama.Framework, Metalama.Framework.Redist, project reference, flowing aspects, preventing aspects, .NET projects, package reference, inheritable aspects"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Distributing projects that use aspects

When your project uses aspects, consider whether the projects that _reference_ your project will also need to use aspects due to this reference.

## Flowing the use of aspects

Your project may _flow_ the need to use aspects and the aspect framework to consumers of your project due to one of the following reasons:

* Your project exposes public aspects that other projects can use.
* Your project has non-sealed, public classes with _inheritable_ aspects.
* Your project has public classes that have _reference validators_.
* Your project contains a _transitive project fabric_ or references a project that includes one.

If this is the case, you don't need to take any action. Your package reference to [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) will flow to consumers of your project.

## Preventing the transitive use of aspects

If consumers of your project will _not_ need to use aspects due to your project, you can prevent [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) from flowing to its consumers by setting the `PrivateAssets="all"` property of the `PackageReference`.

You also need to include the [Metalama.Framework.Redist](https://www.nuget.org/packages/Metalama.Framework.Redist) package, which is the only package that needs to flow to consumers.

Add the following code snippet to your `.csproj` file:

```xml
<ItemGroup>
  <PackageReference Include="Metalama.Framework" PrivateAssets="all"/>
  <PackageReference Include="Metalama.Framework.Redist" />
</ItemGroup>
```

> [!div class="see-also"]
> <xref:aspect-inheritance>
> <xref:aspect-validating>
> <xref:fabrics>
