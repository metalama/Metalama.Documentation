---
uid: validation-extending
level: 300
summary: "The document provides a guide on how to create custom validation rules in Metalama, including extending usage verification with custom predicates and creating new verification rules."
keywords: "custom validation rules, Metalama, usage verification, custom predicates, Metalama.Extensions.Architecture, ReferencePredicate, validation rules, codebase validation"
created-date: 2023-03-22
modified-date: 2024-08-04
---

# Creating your own validation rules

Metalama's true strength lies not in its pre-made features but in its ability to let you create custom rules for validating the codebase against your architecture.

In this article, we will demonstrate how to extend the [Metalama.Extensions.Architecture](https://www.nuget.org/packages/Metalama.Extensions.Architecture) package. This package is open source. For a better understanding of the instructions provided in this article, you can study its [source code](https://github.com/postsharp/Metalama.Extensions/tree/master/src/Metalama.Extensions.Architecture).

## Extending usage verification with custom predicates

Before creating rules from scratch, it's worth noting that some of the existing rules can be extended. In <xref:validating-usage>, you learned how to use methods like <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.CanOnlyBeUsedFrom*> or <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.CannotBeUsedFrom*>. These methods require a predicate parameter, which determines from which scope the declaration can or cannot be referenced. Examples of predicates are <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.CurrentNamespace*>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.NamespaceOf*> of the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions> class. The role of predicates is to determine whether a given code reference should report a warning.

To implement a new predicate, follow these steps:

1. Create a new class and derive it from <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate>. We recommend making this class `internal`.
2. Add fields for all predicate parameters, and initialize these fields from the constructor.

    > [!NOTE]
    > Predicate objects are serialized. Therefore, all fields must be serializable. Notably, objects of <xref:Metalama.Framework.Code.IDeclaration> type are not serializable. To serialize a declaration, call the <xref:Metalama.Framework.Code.IDeclaration.ToRef*> method and store the returned <xref:Metalama.Framework.Code.IRef`1>. For details, see <xref:aspect-serialization>.

3. Implement the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate.IsMatch*> method. This method receives a <xref:Metalama.Extensions.Validation.ReferenceValidationContext>. It must return `true` if the predicate matches the given context (i.e., the code reference); otherwise `false`.
4. Create an extension method for the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateBuilder> type and return a new instance of your predicate class.

### Example: restricting usage based on calling method name

In the following example, we create a custom predicate, `MethodNameEndsWith`, which verifies that the code reference occurs within a method whose name ends with a given suffix.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_CustomPredicate.cs tabs="target"]

## Creating new verification rules

Before you build custom validation rules, you should have a basic understanding of the following topics:

* <xref:aspect-design> (it is not necessary to learn about advising the code);
* <xref:diagnostics>;
* <xref:eligibility>;
* <xref:aspect-inheritance>;
* <xref:fabrics>.

### Designing the rule

When you want to create your own validation rule, the first decision is whether it will be available as a custom attribute, as a compile-time method invoked from a fabric, or as both a custom attribute and a compile-time method. As a rule of thumb, use attributes when rules need to be applied one by one by the developer and use fabrics when rules apply to a large set of declarations according to a code query that can be expressed programmatically. Rules that affect namespaces must be implemented as fabric-based rules because adding a custom attribute to a namespace is impossible. For most ready-made rules of the `Metalama.Extensions.Architecture` namespace, we expose both a custom attribute and a compile-time method.

The second question is whether the rule affects the target declaration of the rule or the _references_ to the target declaration, i.e., how the target declaration is being _used_. For instance, if you want to forbid an interface to be implemented by a `struct`, you must verify references. However, if you want to verify that no method has more than five parameters, you need to validate the type itself and not its references.

A third question relates to rules that verify classes: should the rule be _inherited_ from the base type to derived types? For instance, if you want all implementations of the `IFactory` interface to have a parameterless constructor, you may implement it as an inheritable aspect. However, with inheritable rules, the design process may be more complex. We will detail this below.

### Creating a custom attribute rule

If it is exposed as a custom attribute, it must be implemented as an aspect, but an aspect that does _not_ transform the code, i.e., does not provide any advice.

Follow these steps.

1. Create a new class from one of the following classes: <xref:Metalama.Framework.Aspects.ConstructorAspect>, <xref:Metalama.Framework.Aspects.EventAspect>, <xref:Metalama.Framework.Aspects.FieldAspect>, <xref:Metalama.Framework.Aspects.FieldOrPropertyAspect>, <xref:Metalama.Framework.Aspects.MethodAspect>, <xref:Metalama.Framework.Aspects.ParameterAspect>, <xref:Metalama.Framework.Aspects.PropertyAspect>, <xref:Metalama.Framework.Aspects.TypeAspect>, <xref:Metalama.Framework.Aspects.TypeParameterAspect>

   All of these classes derive from the <xref:System.Attribute> system class.

2. If your rule must be inherited, add the <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> attribute to the class. See <xref:aspect-inheritance> for details.

3. For each error or warning you plan to report, add a static field of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> to your aspect class, as described in  <xref:diagnostics>.

3. Implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method. You have several options:

    * If you need to validate the target declaration itself, or its members, you can inspect the code model under `builder.Target` and report diagnostics using `builder.Diagnostics.Report`.
    * If you need to validate the _references_ to the target declarations, see <xref:aspect-validating>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/RequireDefaultConstructorAspect.cs tabs="aspect,target"]

### Creating a programmatic rule

Follow this procedure:

1. Create a `static` class containing your extension methods. Name it, for instance, `ArchitectureExtensions`.
2. Add the [<xref:Metalama.Framework.Aspects.CompileTimeAttribute?text=CompileTime>] custom attribute to the class.
3. For each error or warning you plan to report, add a static field of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> to your fabric class, as described in <xref:diagnostics>.
4. Create a `public static` extension method with a `this` parameter of type <xref:Metalama.Framework.Fabrics.IQuery`1> where `T` is the type of declarations you want to validate. Name it for instance `verifier`.
5. If you need to apply the rule to _contained_ declarations, select them using the  <xref:Metalama.Framework.Fabrics.IQuery`1.Select*>,  <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> and  <xref:Metalama.Framework.Fabrics.IQuery`1.Where*> methods.
6. From here, you have several options:
 * If you already know, based on the <xref:Metalama.Framework.Fabrics.IQuery`1.Select*>,  <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> and  <xref:Metalama.Framework.Fabrics.IQuery`1.Where*> methods, that the declaration violates the rule, you can immediately report a warning or error using the <xref:Metalama.Framework.Diagnostics.DiagnosticsQueryExtensions.ReportDiagnostic*> method.
 * To validate references (i.e. dependencies), use <xref:Metalama.Extensions.Validation.ReferenceValidationQueryExtensions.ValidateInboundReferences*>.
 * To validate the declaration itself, use <xref:Metalama.Extensions.Validation.ValidationQueryExtensions.Validate*>.

> [!div class="see-also"]
> <xref:video-custom-architecture-rules>


