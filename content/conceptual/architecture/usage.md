---
uid: validating-usage
level: 200
summary: "Verify the usage of classes, members, or namespaces using Metalama to fine-tune accessibility beyond C#'s built-in access modifiers."
keywords: "dependencies, Metalama, custom attributes, C# validation, namespace, internal members, programmatic validation, Metalama.Extensions.Architecture"
created-date: 2023-03-22
modified-date: 2025-11-30
---

# Verifying usage of a class, member, or namespace

Defining dependencies between components—who can call whom—is a critical aspect of software design. In C#, this concept is called _accessibility_. For optimal design, always grant the least necessary accessibility. This minimizes unintended coupling between components and makes future changes easier.

In C#, accessibility is defined across two boundaries: _assemblies_ and _types_. `private` members are only accessible from the current type, `protected` members are accessible from the current and any derived type, `public` members are universally visible, and `internal` members are only accessible from the current assembly unless `InternalsVisibleTo` extends accessibility to other assemblies.

However, large projects often require finer control over accessibility than what C# can provide out of the box.

For instance, you might want to enforce rules such as:

* Requiring a specific method or constructor to be called from unit tests only, based on the caller namespace.
* Forbidding a type from being accessed from outside its home namespace.
* Requiring a whole namespace only to be used by a friend namespace.
* Forbidding internal members of a namespace from being accessed outside of their home namespace.

The traditional approach is to use code comments and rely on manual code reviews to enforce the desired design intent. However, this approach is prone to human error and has a lengthy feedback loop. Another approach is to split the codebase into more fine-grained projects, but this increases build and deployment complexity and negatively affects application startup time.

With Metalama, you can fine-tune the intended accessibility of your namespaces, types, or members using custom attributes or a compile-time API.

## Validating usage with custom attributes

To fine-tune the accessibility of hand-picked types or members, use custom attributes.

Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Apply one of the following custom attributes to the type or member for which you want to limit the accessibility.

    | Attribute | Description |
    |-----------|-------------|
    | <xref:Metalama.Extensions.Architecture.Aspects.CanOnlyBeUsedFromAttribute> | Reports a warning when the target declaration is accessed from outside of the given scope.
    | <xref:Metalama.Extensions.Architecture.Aspects.InternalsCanOnlyBeUsedFromAttribute> |  Reports a warning when any `internal` member of the type is accessed from outside the given scope.
    | <xref:Metalama.Extensions.Architecture.Aspects.CannotBeUsedFromAttribute> | Reports a warning when the target declaration is accessed from the given scope.
    | <xref:Metalama.Extensions.Architecture.Aspects.InternalsCannotBeUsedFromAttribute> | Reports a warning when any `internal` member of the type is accessed from the given scope.

3. Set one or more of the following properties of the custom attribute, which control the scope (which declarations can or can't access the target declaration):

    | Property  | Description  |
    |---------|---------|
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.CurrentNamespace>     |  Includes the current namespace in the scope.       |
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Types> | Includes a list of types in the scope. |
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Namespaces> | Includes a list of namespaces in the scope by identifying them with a string. One asterisk (`*`) matches any namespace component but not the dot (`.`). A double asterisk (`**`) matches any substring including the dot (`.`).
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.NamespaceOfTypes>     |  Includes a list of the namespaces in the scope by identifying them with arbitrary types of these namespaces.

4. Optionally, set the <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Description> property. The value of this property will be appended to the standard error message.

### Example: Test-only constructor

In the following example, the class `Foo` has two constructors, and one of them should only be used in tests. Tests are identified as any code in a namespace ending with the `.Tests` suffix. We define the <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Description> to improve the error message. You can also set the <xref:Metalama.Framework.Code.ReferenceKinds> to limit the kinds of references that are validated.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Attribute_ForTestOnly.cs  tabs="target"]

### Example: Type internals reserved for the current namespace

In the following example, the class `Foo` uses the <xref:Metalama.Extensions.Architecture.Aspects.InternalsCanOnlyBeUsedFromAttribute> constraint to verify that internal members are only accessed from the same namespace. A warning is reported when an internal method of `Foo` is accessed from a different namespace.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Attribute_CurrentNamespace.cs tabs="target"]

## Validating usage programmatically

Custom attributes are adequate when the types or members to validate must be hand-picked. However, when these types or members can be selected by a _rule_, it's more efficient to do it programmatically with compile-time code and [fabrics](xref:fabrics).

Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Create or reuse a fabric type as described in <xref:fabrics>:

    * To concentrate the whole validation logic for the whole project into a single location, create a <xref:Metalama.Framework.Fabrics.ProjectFabric>.
    * To share the validation logic among several projects, see <xref:fabrics-many-projects>.
    * To split the logic on a per-namespace basis, create one <xref:Metalama.Framework.Fabrics.NamespaceFabric> in each namespace that you want to validate.
    * To validate specific types, you can use custom attributes or add a nested <xref:Metalama.Framework.Fabrics.TypeFabric> to this type.

3. Import the <xref:Metalama.Extensions.Architecture> and <xref:Metalama.Extensions.Architecture.Predicates> namespaces to benefit from extension methods.

4. Edit the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*>, or <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

5. Call one of the following methods:

    | Attribute | Description |
    |-----------|-------------|
    | <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.CanOnlyBeUsedFrom*> | Reports a warning when the target declaration is accessed from outside the given scope.
    | <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.InternalsCanOnlyBeUsedFrom*> |  Reports a warning when any `internal` member of the type is accessed from outside of the given scope.
    | <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.CannotBeUsedFrom*> | Reports a warning when the target declaration is accessed from the given scope.
    | <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.InternalsCannotBeUsedFrom*> | Reports a warning when any `internal` member of the type is accessed from the given scope.

6. Pass a delegate like `r => r.ScopeMethod()` where `ScopeMethod` is one of the following methods:

    | Method | Description |
    |--------|-------------|
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.CurrentNamespace*>|  Includes the current namespace in the scope.       |
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.NamespaceOf*> | Includes the parent namespace of a given type in the scope |
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Type*> | Includes a given type in the scope.
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Namespace*> | Includes a given namespace in the scope. One asterisk (`*`) matches any namespace component but not the dot (`.`). A double asterisk (`**`) matches any substring including the dot (`.`).

    For instance:

    ```cs
    amender.CanOnlyBeUsedFrom( r => r.CurrentNamespace() );
    ```

    You can create complex conditions thanks to the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.And*>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Or*>, and <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Not*> methods.

7. Optionally, pass a value for the `description` parameter. This text will be appended to the warning message. You can also supply a <xref:Metalama.Framework.Code.ReferenceKinds> to limit the kinds of references that are validated.

### Example: Namespace internals reserved for the current namespace

In the following example, we use a namespace fabric to restrict the accessibility of internal members to this namespace. A warning is reported when this rule is violated, as in the `ForbiddenInheritor` class.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_InternalNamespace.cs ]

### Example: Forbidding the use of floating-point arithmetic from the Invoicing namespace

Using floating-point arithmetic in operations involving currencies is a common pitfall. Instead, `decimal` numbers should be used. In the following example, we use a project fabric to validate all references to the `float` and `double` types. We report a diagnostic when they're used from the `**.Invoicing` namespaces.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_ForbidFloat.cs]

> [!div class="see-also"]
> <xref:validation>
> <xref:Metalama.Extensions.Architecture.Aspects.CanOnlyBeUsedFromAttribute>
> <xref:Metalama.Extensions.Architecture.Aspects.CannotBeUsedFromAttribute>
> <xref:fabrics>
