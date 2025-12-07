---
uid: dependency-injection-aspect
summary: "Learn how to use the [Dependency] aspect to reduce boilerplate code in dependency injection scenarios with Microsoft.Extensions.DependencyInjection."
level: 200
keywords: "dependency injection, .NET, Microsoft.Extensions.DependencyInjection, Metalama.Extensions.DependencyInjection, DependencyAttribute, dependency, inject, import"
created-date: 2024-06-11
modified-date: 2025-11-30
---

# Injecting dependencies

Dependency injection is one of the most prevalent patterns in .NET. Prior to .NET Core, the community developed several frameworks with varying features and coding patterns. Since then, `Microsoft.Extensions.DependencyInjection` has emerged as the default framework.

Unlike its predecessors, `Microsoft.Extensions.DependencyInjection` doesn't rely on custom attributes on fields and properties. Instead, it requires you to add dependencies to the class constructor and store them as fields. This requirement can lead to boilerplate code, especially if there are numerous dependencies in a complex class hierarchy. Migrating your code from an attribute-based framework to a constructor-based one can also be tedious.

To reduce this boilerplate, use the <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute?text=[Dependency]> aspect from `Metalama.Extensions.DependencyInjection`.

## Benefits

* **Reduces boilerplate code**: Eliminates the need to manually write constructor parameters and field assignments.
* **Simplifies migration**: Eases the transition from attribute-based frameworks to constructor-based ones.
* **Supports multiple frameworks**: Works with various dependency injection frameworks (see <xref:dependency-injection>).

## Properties

The <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute?text=[Dependency]> aspect provides two properties:

* <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute.IsLazy>: Generates code that resolves the dependency lazily, upon the first access.
* <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute.IsRequired>: Determines whether the code can execute if the property is missing. If you're using nullable reference types, the `IsRequired` parameter is inferred from the nullability of the field or property.

## Example: Injecting dependencies

The following example demonstrates the code generation pattern for three types of dependencies: required, optional, and lazy.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection/DependencyInjectionAspect.cs]

## See also

> [!div class="see-also"]
>
> <xref:patterns>
>
> <xref:dependency-injection>
