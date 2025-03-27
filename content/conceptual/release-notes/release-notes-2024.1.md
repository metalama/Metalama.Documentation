---
uid: release-notes-2024.1
summary: "Metalama 2024.1 improves the user interface, especially through a Visual Studio extension that has been unified with PostSharp. The version also improves performance and the advising of constructors."
keywords: "Metalama 2024.1, release notes"
created-date: 2024-06-19
modified-date: 2024-08-04
---

# Metalama 2024.1

The primary objective of Metalama 2024.1 is to enhance the user interface, with a focus on improving Visual Studio tooling and introducing a new license activation UI. Furthermore, it is now possible to override constructors.

## User Interface

### Unification of Visual Studio Tools for Metalama

Previously, Metalama and PostSharp each had their own Visual Studio extension, each with a unique set of features and user experiences. In Metalama 2024.1, we have merged these two extensions into one, unifying the development experience. Thus, regardless of whether you are using Metalama, PostSharp, or both, you should now install a _single_ extension named _Visual Studio Tools for Metalama_, or, in short, _Metalama + PostSharp_.

#### Aspect Explorer

The Aspect Explorer tool window, previously available only to PostSharp users, now also supports Metalama.

The Aspect Explorer consists of three panels. The first panel displays all aspect classes available in the solution. When you click on an aspect class in this panel, the second panel displays the list of declarations affected by this aspect class. When you click on one of these declarations, the third panel displays the list of transformations applied by this aspect to the selected declaration.

You can double-click on any declaration to open it in the code editor.

![Aspect Explorer](../using/images/aspect-explorer.png)

#### Learning Hub

The Learning Hub displays articles and tutorials, prioritizing them based on your learning objectives. For instance, if you are interested in logging, the Learning Hub will first display tutorials relevant to logging, sorted in increasing order of complexity.

#### ARM64 Support

Visual Studio Tools for Metalama function seamlessly on an ARM64 device.

### Licensing UI

When using Metalama for the first time, a UI will guide you through the process of registering your license key or choosing between the trial and the free edition. You will also have the option to subscribe to a newsletter and the Metalama email course.

We also have added toast notifications for unhandled exceptions.

## Overriding constructors

You can now override instance constructors from the aspect's `BuildAspect` method by calling the <xref:Metalama.Framework.Advising.AdviserExtensions.Override*?text=AdviserExtensions.Override> method and passing an <xref:Metalama.Framework.Code.IConstructor>.

This will work for both standard constructors and primary constructors. If you attempt to override the primary constructor, it will be transparently expanded into a standard constructor.

For details, see <xref:overriding-constructors>.


## Other improvements

* Numeric contracts now generate idiomatic code.
* Metalama 2024.1 now processes aspects approximately 55% faster than 2024.0. However, the fixed cost of using Metalama instead of the vanilla C# compiler has not improved. See [this analysis](https://blog.postsharp.net/metalama-2024-1-performance.html) for details.

## Breaking Changes

* Initializers are now all executed before constructor parameter contracts. Previously, initializers and contracts on constructors could be interleaved.
* The ordering of contracts within the same method has been fixed.
* Contracts are now ineligible on unimplemented partial methods.
* In <xref:Metalama.Framework.Code.TypeFactory>, the generic methods `public static T ToNullableType<T>( this T type ) where T : IType` and `public static T ToNonNullableType<T>( this T type ) where T : IType` have been replaced by a set of non-generic overloads (see <xref:Metalama.Framework.Code.TypeFactory.ToNullableType*> and <xref:Metalama.Framework.Code.TypeFactory.ToNonNullableType*>), taking into account the fact that the nullable type of an <xref:Metalama.Framework.Code.ITypeParameter> is not an <xref:Metalama.Framework.Code.ITypeParameter> if the type parameter has a `struct` constraint.
* The <xref:Metalama.Framework.Code.INamedType.UnderlyingType> property, when the <xref:Metalama.Framework.Code.INamedType> represents a `Nullable<T>` (i.e., a nullable value type), no longer returns `T` but `Nullable<T>`. This behavior is now consistent with other generic types but no longer consistent with nullable reference types.


