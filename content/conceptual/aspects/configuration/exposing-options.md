---
uid: exposing-options
level: 300
summary: "The document provides a detailed guide on how to expose a configuration API using the Metalama.Framework.Options namespace. It explains how to create an options class, read options, configure options from a fabric, expose options directly on your aspect, and create a configuration custom attribute."
keywords: "Metalama.Framework.Options, configuration API, options class, read options, configure options, IHierarchicalOptions, IIncrementalObject, ApplyChanges method, IHierarchicalOptionsProvider, custom attribute"
created-date: 2024-08-04
modified-date: 2024-08-04
---

# Exposing a configuration API

The <xref:Metalama.Framework.Options> namespace is your primary resource when building a configuration API for your aspects. Although it may seem complex at first glance, it allows you to create a user-friendly configuration experience with relative ease.

The <xref:Metalama.Framework.Options> namespace supports the following features:

* Options are automatically inherited from the base type, base member, enclosing type, enclosing namespace, or project. This means you can easily configure options for the entire project, a specific namespace, type, or even a particular method.
* Cross-project inheritance of options is fully supported and transparent.
* You can build a developer-friendly programmatic API to configure your options, which can be used by your aspects' users from any fabric.
* You can also build a custom configuration attribute.

## Creating an option class

When designing a configuration API, the first step involves creating a class to represent and store the options. This option class must implement the <xref:Metalama.Framework.Options.IHierarchicalOptions`1> for as many declaration types as needed. For instance, if you're building a caching aspect, you would naturally implement the `IHierarchicalOption<IMethod>` interface because the aspect is applied to methods. However, to allow bulk configuration of methods, you would also implement `IHierarchicalOption<INamedType>` for types, `IHierarchicalOption<INamespace>` for namespaces, and `IHierarchicalOption<ICompilation>` for the entire project.

It's crucial to understand that option classes represent _changes_, and the classes themselves must be _immutable_. This means all properties must have an `init` accessor instead of a `set` accessor. Furthermore, most properties, including value-typed properties, must be _nullable_. Typically, a `null` value signifies the absence of a modification in this property. This is why we say option classes represent _incremental_ objects and indirectly implement the <xref:Metalama.Framework.Options.IIncrementalObject> interface.

The most important member of option classes is the <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method. This method should merge two immutable incremental instances into one new immutable instance representing the combination of changes. Metalama calls this method to merge the configuration settings provided at different levels by the user.

The <xref:Metalama.Framework.Options.IHierarchicalOptions`1> interface defines a second method, <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*>, which we should ignore at the moment and should just return `null`. This method is useful when getting default options from MSBuild properties. See <xref:reading-msbuild-properties> for details.

In summary, to create an option class, follow these steps:

1. Create a class (records are currently unsupported) that implements as many generic instances of the <xref:Metalama.Framework.Options.IHierarchicalOptions`1> interface as needed, where `T` is any type of declaration on which you want to allow the user to define options.
2. Do not add any constructor to this class. The class must keep its default constructor.
3. Ensure all properties are `init`-only and are of a nullable type, even value-typed ones (exceptions apply for complex types like collections, see below).
4. Skip the implementation of `IHierarchicalOptions.GetDefaultOptions(IProject)` for now. Just return `this`.
5. Implement the <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method so that it returns a new instance of the option class combining the changes of the current instance and the supplied parameter, where the properties of the parameter win over the ones of the current instance.

### Example: option class

The following class demonstrates a typical implementation of <xref:Metalama.Framework.Options.IHierarchicalOptions`1>. The object has no explicit constructor and has only `init`-only properties. The <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method merges the changes and gives priority to the properties of the supplied parameter, if defined.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.Options.cs]

## Reading the options

Reading options that apply to a different context requires some care. There are two APIs. Both are exposed under the expression.

To read the options applying to any declaration, call the <xref:Metalama.Framework.Code.DeclarationExtensions.Enhancements*?text=declaration.Enhancements()> method and then <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetOptions*> method.

> [!WARNING]
> Options provided by _aspects_ through the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider> interface (see below) are applied shortly _before_ the aspect's <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is executed. They will not be available to the <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetOptions*?text=d.Enhancements().GetOptions()> before that moment.

### Example: reading options

The following example demonstrates a template that uses <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetOptions*?text=d.Enhancements().GetOptions()> to read the options applying to the current aspect.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.Aspect.cs]

## Configuring the options from a fabric

If you choose to make the option class public, the users of your aspects can now set the options using the <xref:Metalama.Framework.Options.OptionQueryExtensions.SetOptions*?text=amender.Outgoing.SetOptions> method in any fabric. Users can also use methods like <xref:Metalama.Framework.Fabrics.IQuery`1.Select*>, <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> or <xref:Metalama.Framework.Fabrics.IQuery`1.Where*>.

> [!NOTE]
> This technique not only works from fabrics, but also from any aspect that is applied _before_ the aspect that will consume the option.

### Example: applying options from a fabric

The following example puts all previous code snippets together. It adds a project fabric that configures the logging aspect for the whole project and then specify different options for a child namespace. You can check in the that the logging code generated for the target code refers to different categories, as configured by the fabrics.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs]

### Exposing options directly on your aspect

Often, you will want to offer users of your aspect the possibility to specify options directly when instantiating the aspect, typically as properties of the aspect custom attribute.

To achieve this, your aspect must implement the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider> interface. This interface has a single method <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> that returns a list of options objects, typically an instance of your option class wrapped into an array.

Note that custom attributes cannot have properties of nullable value types. Therefore, you cannot just duplicate the properties of the option class into your aspect. Instead, you have to create field-backed properties where every property is backed by a field of nullable field. With this design, the implementation of <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> becomes a mapping of the backing fields of the aspects to the properties of the option class.

### Example: aspect providing options

In the following example, the aspect code has been updated to support configuration properties. Note how the `Level` property, of a value type, is backed by nullable value type, so we can distinguish between the default value and the unspecified value. The aspect shows a trivial implementation of the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> aspect.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration_Provider.Aspect.cs]

## Creating a configuration custom attribute

In addition to the programmatic API represented by the option class, you may want to provide your users with configuration custom attributes. This would allow users to configure your aspect without resorting to a fabric.

To create a configuration custom attribute, follow these steps:

1. Create a class derived from <xref:System.Attribute?text=System.Attribute>.
2. Add the <xref:System.Attribute?text=[AttributeUsage]> attribute as required. Typically, you will want to allow users to apply this attribute to the assembly and the enclosing type.
3. Implement the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider> interface as described above.



