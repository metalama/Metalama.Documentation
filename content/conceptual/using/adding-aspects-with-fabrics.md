---
uid: fabrics-adding-aspects
level: 200
summary: "The document provides a guide on how to use fabrics in the Metalama Framework to programmatically add aspects to targets, with examples and recommendations on when to use fabrics versus custom attributes."
keywords: "fabrics, Metalama Framework, add aspects, logging, profiling, ProjectFabric, AmendProject method, AddAspectIfEligible"
created-date: 2023-03-01
modified-date: 2025-12-07
---

# Adding many aspects simultaneously

In <xref:quickstart-adding-aspects>, you learned how to apply aspects individually using custom attributes. While this approach is suitable for aspects like caching or auto-retry, it can be cumbersome for other aspects such as logging or profiling.

In this article, you'll learn how to use _fabrics_ to add aspects to your targets _programmatically_.

## When to use fabrics

Fabrics let you add aspects from a central location. Use fabrics instead of custom attributes when you can express the decision to add an aspect as a _rule_ that depends solely on the metadata of the declaration, such as its name, signature, parent type, implemented interfaces, custom attributes, or any other detail exposed by the [code model](xref:Metalama.Framework.Code).

For instance, to add logging to all public methods of all public types in a namespace, use a fabric.

Conversely, don't use a fabric to add caching to all methods that start with the word _Get_ because you may create more problems than you solve. Caching is typically an aspect you'd carefully select, and custom attributes are a better approach.

## Adding aspects using fabrics

To add aspects using fabrics:

1. Create a fabric class and derive it from <xref:Metalama.Framework.Fabrics.ProjectFabric>.

2. Override the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> abstract method.

3. Call one of the following methods from <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>:

   * To select all types in the project, use the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectTypes*?text=amender.SelectTypes> method.
   * To select type members (methods, fields, nested types, etc.), call the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> method and provide a lambda expression that selects the relevant type members (for example, `SelectMany( t => t.Methods )` to select all methods).
   * To filter types or members, use the <xref:Metalama.Framework.Fabrics.IQuery`1.Where*> method.

4. Call the <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> or <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> method.

> [!NOTE]
> The amender object selects not only members declared in source code but also members introduced by other aspects and therefore unknown when the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method executes. This is why these methods don't directly expose the code model.

### Example 1: Adding aspect to all methods in a project

In the following example, we use a fabric to apply a logging aspect to all methods in the current project.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs]

The key method is `AmendProject`. This method adds aspects to different members of a project, essentially _amending_ the project.

Inside the `AmendProject` method, we get all public methods and add _logging_ and _retrying_ aspects to them.

### AddAspect or AddAspectIfEligible?

The difference between <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> and <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> is that <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> throws an exception when you try adding an aspect to an ineligible target (for instance, a caching aspect to a `void` method), while <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> silently ignores such targets.

* If you use <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*>, you may need to add many conditions to your `AmendProject` method to avoid exceptions. The benefit is you'll be _aware_ of these conditions.
* If you use <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*>, some target declarations may be silently ignored.

In most cases, we recommend using <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*>.

### Example 2: Adding more aspects using the same Fabric

In the following example, we add two aspects: logging and profiling. We add profiling only to public methods of public classes.

For each project, we recommend one project fabric. Multiple project fabrics make it difficult to understand the aspect application order.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric_TwoAspects.cs]

### Example 3: Adding aspects to all methods in a namespace using a NamespaceFabric

To add aspects to all methods within a specific namespace, use a <xref:Metalama.Framework.Fabrics.NamespaceFabric>. Unlike a `ProjectFabric`, which operates at the project level, a `NamespaceFabric` is placed directly within the target namespace and automatically scopes its operations to that namespace.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/NamespaceFabric.cs]

In this example, the `Fabric` class inherits from <xref:Metalama.Framework.Fabrics.NamespaceFabric> and is placed inside `TargetNamespace`. The fabric's `AmendNamespace` method uses `SelectTypes()` to get all types within the namespace, then `SelectMany( t => t.Methods )` to select their methods, and adds the `Log` aspect to all eligible methods.

Notice how `TargetMethod` in `TargetNamespace` gets the logging aspect applied (shown in the transformed code), while `OtherMethod` in `OtherNamespace` remains unchanged because the fabric only affects its containing namespace.

### Performance: Avoid filtering all types by namespace

A common mistake when adding aspects to a specific namespace from a `ProjectFabric` is to enumerate all types and filter by namespace:

```csharp
// DON'T do this - it enumerates ALL types in the project, which is slow
amender.SelectTypes()
    .Where( t => t.Namespace.FullName.StartsWith( "MyNamespace" ) )
    .AddAspectIfEligible<LogAttribute>();
```

This approach is inefficient because it enumerates every type in the entire project.

Instead, use one of these approaches:

**Option 1: Use a NamespaceFabric (recommended)**

Place a <xref:Metalama.Framework.Fabrics.NamespaceFabric> directly in the target namespace, as shown in Example 3 above. This is the most readable approach.

**Option 2: Use GlobalNamespace.GetDescendant**

If you need to target a namespace from a `ProjectFabric`, navigate directly to the namespace using <xref:Metalama.Framework.Code.INamespace.GetDescendant*>:

```csharp
// DO this - navigates directly to the namespace
amender.Select( c => c.GlobalNamespace.GetDescendant( "MyNamespace" )! )
    .SelectTypes()
    .SelectMany( t => t.Methods )
    .AddAspectIfEligible<LogAttribute>();
```

> [!NOTE]
> <xref:Metalama.Framework.Fabrics.IQuery`1.SelectTypes*> selects types in the specified namespace only, not in child namespaces. To include types in child namespaces, use <xref:Metalama.Framework.Code.NamespaceExtensions.DescendantsAndSelf*>:
>
> ```csharp
> amender.Select( c => c.GlobalNamespace.GetDescendant( "MyNamespace" )! )
>     .SelectMany( ns => ns.DescendantsAndSelf() )
>     .SelectTypes()
>     .SelectMany( t => t.Methods )
>     .AddAspectIfEligible<LogAttribute>();
> ```

### Example 4: Adding the `Log` aspect only to derived classes of a given class

Sometimes you may want to add aspects only to a class and its derived types. The following fabric shows how to accomplish this by getting the derived types of a given type and adding aspects to them.

[!metalama-test ~/code/Metalama.Documentation.QuickStart.Fabrics.2/AddLoggingToChildrenFabric.cs tabs="target"]

> [!TIP]
> Use code metrics to filter declarations based on complexity. For example, add logging only to methods exceeding a certain number of syntax nodes. For details, see <xref:metrics>.

> [!div class="see-also"]
>
> **See also**
>
> <xref:using-metalama>
> <xref:fabrics>
> <xref:quickstart-adding-aspects>
> <xref:fabrics-many-projects>
> <xref:metrics>
