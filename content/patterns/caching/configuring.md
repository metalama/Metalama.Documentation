---
uid: caching-configuration
summary: "The document provides instructions on configuring the Metalama caching service, detailing three methods: using custom attributes, using a fabric, and at runtime with caching profiles."
keywords: "caching configuration, Metalama caching, caching profiles, caching options, configure caching"
created-date: 2024-04-25
modified-date: 2025-11-30
---

# Configuring the caching service

The behavior of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> aspect can be configured in several ways, including:

- Expiration (absolute and sliding)
- Priority
- Auto-reload
- Enabled/disabled

These options can be set in three ways:

- At compile time using the properties of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> or <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute?text=[CachingConfiguration]> attributes
- At compile time using the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> fabric method
- At run time using the <xref:Metalama.Patterns.Caching.CachingProfile> class

This article describes these three approaches.

## Configuring caching with custom attributes

Configure the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> aspect by setting the properties of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> custom attribute. The downside of this approach is that you must repeat the configuration for each cached method.

To configure several methods with a single line of code, add the <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute> custom attribute to the declaring type, the base type of the declaring type, the enclosing type of the declaring type (if it is nested), or the declaring assembly.

The configuration of Metalama Caching is based on the configuration framework of Metalama Framework. For more details, see <xref:fabrics-configuration>.

### Example: Configuring expiration with custom attributes

In the following example, the absolute expiration of cache items is set to 60 minutes for methods of the `PricingService` class, but to 20 minutes for the `GetProducts` method.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AbsoluteExpiration_Attribute.cs diff]

## Configuring caching with fabrics

Instead of using custom attributes, you can use a fabric to configure the caching aspect at any level of granularity, thanks to the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> fabric extension method.

### Example: Configuring expiration with a fabric

The following example sets the absolute expiration to 20 minutes for the namespace `MyProduct.MyNamespace`.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AbsoluteExpiration_Fabric.cs]

## Configuring caching at run time with profiles

All compile-time approaches described above have the same drawback: by definition, they can't be modified at run time. This can be a problem if you want caching options to be loaded from a configuration file that you can deploy with your application.

Metalama Caching allows you to change caching options at run time with a concept called _caching profile_. These are sets of options that can be modified at run time and are represented by the <xref:Metalama.Patterns.Caching.CachingProfile> class.

To modify run-time caching options with a caching profile:

1. Set the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.ProfileName> property of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect, the <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute?text=[CachingConfiguration]> attribute, or the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingOptionsBuilder> parameter of the  <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> fabric extension method.

    > [!NOTE]
    > A default profile is assigned to any cached method even if the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.ProfileName> property is not assigned. This profile can be customized at run time like any other profile.

2. Go back to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>, and supply a delegate that calls <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.AddProfile*>.

> [!WARNING]
> Any configuration property specified through a compile-time mechanism takes precedence over the configuration of the <xref:Metalama.Patterns.Caching.CachingProfile>.

### Example: Configuring expiration with a profile

In this example, the `ProductCatalogue` class uses two caching profiles: the default one, and the _hot_ one, which should refresh content more frequently. However, we don't want to hard-code these expiration values in the source code. The following code in `Program.cs` configures the caching profiles. While the values are hard-coded here, they could easily be read from a configuration file.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Profiles/Profiles.cs]

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-getting-started>
> <xref:fabrics-configuration>
