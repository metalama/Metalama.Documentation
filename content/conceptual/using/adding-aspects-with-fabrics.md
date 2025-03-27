---
uid: fabrics-adding-aspects
level: 200
summary: "The document provides a guide on how to use fabrics in the Metalama Framework to programmatically add aspects to targets, with examples and recommendations on when to use fabrics versus custom attributes."
keywords: "fabrics, Metalama Framework, add aspects, logging, profiling, ProjectFabric, AmendProject method, AddAspectIfEligible"
created-date: 2023-03-01
modified-date: 2024-11-06
---

# Adding many aspects simultaneously

In <xref:quickstart-adding-aspects>, you learned how to apply aspects individually using custom attributes. While this approach is suitable for aspects like caching or auto-retry, it can be cumbersome for other aspects such as logging or profiling.

In this article, you will learn how to use _fabrics_ to add aspects to your targets _programmatically_.

## When to use fabrics

Fabrics allow you to add all aspects from a central location. Consider using fabrics instead of custom attributes when the decision to add an aspect to a declaration can be easily expressed as a _rule_, and when this rule depends solely on the metadata of the declaration, such as its name, signature, parent type, implemented interfaces, custom attributes, or any other detail exposed by the [code model](xref:Metalama.Framework.Code).

For instance, if you want to add logging to all public methods of all public types of a namespace, it is more efficient to do it using a fabric.

Conversely, it may not be advisable to use a fabric to add caching to all methods that start with the word _Get_ because you may end up creating more problems than you solve. Caching is typically an aspect you would carefully select, and custom attributes are a better approach.

## Adding aspects using fabrics

To add aspects using fabrics:

1. Create a fabric class and derive it from <xref:Metalama.Framework.Fabrics.ProjectFabric>.

2. Override the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> abstract method.

3. Call one of the following methods from <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>:

   * To select all types in the project, use the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectTypes*?text=amender.SelectTypes> method.
   * To select type members (methods, fields, nested types, etc.), call the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> method and provide a lambda expression that selects the relevant type members, e.g., `SelectMany( t => t.Methods )` to select all methods.
   * To filter types or members, use the <xref:Metalama.Framework.Fabrics.IQuery`1.Where*> method.

4. Call the <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> or <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> method.

> [!NOTE]
> The amender object will not only select members declared in source code, but also members introduced by other aspects and therefore unknown when the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method is executed. This is why these methods do not directly expose the code model.

### Example 1: Adding aspect to all methods in a project

In the following example, we use a fabric to apply a logging aspect to all methods in the current project.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs]

There are a few things to note in this example. The first point to consider is the `AmendProject` method. We aim to add aspects to different members of a project. Essentially, we are trying to _amend_ the project, hence the name.

Inside the `AmendProject` method, we get all the public methods and add _logging_ and _retrying_ aspects to these methods.

### AddAspect or AddAspectIfEligible?

The difference between <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> and <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> is that <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> will throw an exception if you try adding an aspect to an ineligible target (for instance, a caching aspect to a `void` method), while <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> will silently ignore such targets.

* If you choose <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*>, you may be annoyed by exceptions and may have to add a lot of conditions to your `AmendProject` method. The benefit of this approach is that you will be _aware_ of these conditions.
* If you choose <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*>, you may be surprised that some target declarations were silently ignored.

As is often the case, life does not give you a choice to be completely happy, but you can often choose which pain you want to suffer. In most cases, we recommend using <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*>.

### Example 2: Adding more aspects using the same Fabric

In the following example, we add two aspects: logging and profiling. We add profiling only to public methods of public classes.

For each project, it is recommended to have only one project fabric. Having several project fabrics makes it difficult to understand the aspect application order.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric_TwoAspects.cs]

### Example 3: Adding aspects to all methods in a given namespace

To add the Logging aspect (`LogAttribute`) to all the methods that appear in types within namespaces that start with the prefix `Outer.Inner` and all the child types located in any descendant namespace, use the following fabric.

[!metalama-test  ~/code/DebugDemo2/Fabric2.cs tabs="target"]

In this fabric, we use the `GlobalNamespace.GetDescendant` method to retrieve all child namespaces of the given namespace (in this case, `Outer.Inner`). The first `SelectMany` call retrieves all the types in these namespaces, and the subsequent `SelectMany` call retrieves all the methods in these types. This results in an `IQuery<IMethod>`. The final call to <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> adds the `Log` aspect to all eligible methods.

### Example 4: Adding the `Log` aspect only to derived classes of a given class

Sometimes you may not need or want to add aspects to all types, but only to a class and its derived types. The following fabric shows how you can accomplish this. In this example fabric, you will see how to get the derived types of a given type and how to add aspects to them.

[!metalama-test ~/code/Metalama.Documentation.QuickStart.Fabrics.2/AddLoggingToChildrenFabric.cs tabs="target"]

> [!div class="see-also"]
> <xref:video-fabrics-and-inheritance>
> <xref:fabrics-adding-aspects>

