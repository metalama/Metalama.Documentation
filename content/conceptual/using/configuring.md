---
uid: fabrics-configuration
level: 200
summary: "The document provides a guide on how to configure aspects with fabrics in the Metalama framework, detailing the process steps, example configurations, and information about inheritance of configuration options."
keywords: "configuration, Metalama framework, IHierarchicalOptions, SetOptions, compile-time configuration"
created-date: 2024-04-22
modified-date: 2024-08-04
---

# Configuring aspects with fabrics

Certain aspect libraries may expose a compile-time, imperative configuration API that influences the behavior of aspects.

For instance, the `Metalama.Patterns.Caching` library has a <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> method that allows changing various settings such as the expiration of cache items. Similarly, `Metalama.Extensions.DependencyInjection` provides a <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*> method for registering new frameworks.

Alternatively, an aspect library may expose its configuration API as a class implementing the <xref:Metalama.Framework.Options.IHierarchicalOptions> interface.

The process to access these configuration APIs is similar to adding aspects in bulk using fabrics:

1. Create a fabric class and derive it from one of the following: <xref:Metalama.Framework.Fabrics.ProjectFabric>, <xref:Metalama.Framework.Fabrics.NamespaceFabric>, <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric>, or, less commonly, <xref:Metalama.Framework.Fabrics.TypeFabric>.

2. Override the appropriate method: <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*>, or <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>.

3. Call the following methods:

   * To select all types in the project or the namespace, use the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectTypes*?text=amender.SelectTypes> method.
   * To select type members (methods, fields, nested types, etc.), call the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> method and provide a lambda expression that selects the relevant type members, e.g. `SelectMany( t => t.Methods )` to select all methods.
   * To filter types or members, use the <xref:Metalama.Framework.Fabrics.IQuery`1.Where*> method.

4. The next step depends on the kind of configuration API exposed by the aspect library:

    * If the aspect library exposes a `Configure` method, such as <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> or <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*>, call this method.
    * If the aspect library exposes a configuration object that implements the <xref:Metalama.Framework.Options.IHierarchicalOptions> interface, call the <xref:Metalama.Framework.Options.OptionQueryExtensions.SetOptions*> and pass an instance of the desired configuration.


## Example: configuring caching

The following example demonstrates how to configure caching. It sets the absolute expiration for the `MyProduct.MyNamespace` namespace.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AbsoluteExpiration_Fabric.cs]


## About the incremental nature of compile-time configuration

All compile-time configuration APIs are _incremental_. In other words, any call to a `Configure` or <xref:Metalama.Framework.Options.OptionQueryExtensions.SetOptions*> method represents a _change_ in the configuration. These changes are merged on a per-property basis.

For instance, let's say you configure caching at the project level, setting an absolute expiration of 30 minutes and a sliding expiration of 10 minutes. However, for a sub-namespace named `ColdNs`, you increase the absolute expiration to 60 minutes and leave the sliding expiration unchanged. As expected, the sliding expiration in `ColdNs` remains 10 minutes.

Interestingly, the order of configuration operations does _not_ matter when they affect different declarations. Whether you first configure `ColdNs` and then the project, or perform these operations in the reverse order, the result is the same.

Therefore, the two following code snippets are equivalent:

```cs
amender
  .ConfigureCaching( caching => {
        caching.SlidingExpiration = TimeSpan.FromMinutes( 10 );
        caching.AbsoluteExpiration = TimeSpan.FromMinutes( 30 );
    });

amender
  .Select( x => x.GlobalNamespace.GetDescendant( "ColdNs" )! )
  .ConfigureCaching( caching => caching.AbsoluteExpiration = TimeSpan.FromMinutes( 60 ) );

```

And in reverse order:

```cs
amender
  .Select( x => x.GlobalNamespace.GetDescendant( "ColdNs" )! )
  .ConfigureCaching( caching => caching.AbsoluteExpiration = TimeSpan.FromMinutes( 60 ) );

amender
  .ConfigureCaching( caching => {
        caching.SlidingExpiration = TimeSpan.FromMinutes( 10 );
        caching.AbsoluteExpiration = TimeSpan.FromMinutes( 30 );
    });

```

The order of operations only matters when they are applied to the same declaration. In this case, the last operation always wins on a per-property basis.

## Configuration with custom attributes

Some libraries may provide configuration custom attributes in addition to an imperative one.

It's important to note that both mechanisms &mdash; fabrics and custom attributes &mdash; are equivalent. Configuration custom attributes, when they exist, are simply easier ways to call the configuration API. The configuration properties they provide are merged with the ones provided by the fabrics.


## Inheritance of configuration options

Unless specified otherwise by the aspect library, all configuration options are _inherited_ from the base type or from the overridden member. However, implemented interfaces are not taken into account.

So, when you want to set some options for an entire class family, it's sufficient to set these options for the base class, and they will automatically apply to child classes.

Options inherited from the base class take precedence over the options that come from the enclosing type (including that of nested types), the enclosing namespace, or the project.



