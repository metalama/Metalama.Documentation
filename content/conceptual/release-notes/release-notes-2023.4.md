---
uid: release-notes-2023.4
summary: "Metalama 2023.4 introduces a new configuration framework, the first stable release of Metalama Contracts, and the stabilization of Metalama Caching. It also includes updates for Visual Studio 17.8 readiness and various enhancements and bug fixes."
keywords: "Metalama 2023.4, release notes"
created-date: 2024-01-02
modified-date: 2024-08-04
---

# Metalama 2023.4

## Platform updates

### Visual Studio 17.8 readiness

The new Visual Studio version is based on .NET 8. To avoid incompatibilities with Metalama, update your packages to prevent design-time errors in your code after updating Visual Studio.

Metalama 2023.4 doesn't provide complete support for .NET 8 and C# 12, only issue avoidance. Supporting the next version of the .NET stack is the focus of Metalama 2024.0 and PostSharp 2024.0.

## New features

### Configuration framework

Complex and widely-used aspects often require a centralized, project-wide method for configuring compile-time behavior.

To address this need, we built a new configuration framework within the <xref:Metalama.Framework.Options> namespace. This framework enables aspect authors to design APIs that configure the entire project, specific namespaces, or class families from a single line of code. The framework supports both programmatic configuration using fabric extension methods and declarative configuration via custom attributes. It also supports configuration inheritance from base types, even across project borders.

This new options framework is pivotal to Metalama Contracts, Metalama Caching, and other ready-made aspects currently in development.

For details, see <xref:aspect-configuration>.

### Metalama Contracts

With v2023.4, we're unveiling the first stable release of Metalama Contracts. This framework streamlines the application of Contract-Based Programming principles, a software engineering practice that significantly enhances software reliability and clarity. Within this paradigm, a contract establishes a series of obligations and expectations between a caller and a callee.

Metalama Contracts implements three core tenets of contract-based programming: preconditions, postconditions, and invariants.

For details, see <xref:contract-patterns>.

### Metalama Caching

Another highlight in v2023.4 is the stabilization of Metalama Caching, an open-source, aspect-oriented caching framework that simplifies the caching of method return values as a function of its parameters. Not only does it save a lot of boilerplate code, but it also practically eliminates inconsistencies in cache key generation and significantly reduces the complexities associated with cache dependencies.

Metalama Caching, a port of the battle-tested PostSharp Caching, has been overhauled to align with contemporary coding practices, including dependency injection, an immutability-centric approach for initialization, and the latest C# 11 features.

For details, see <xref:caching>.

### Memoization

Memoization is a simple yet highly efficient form of caching applicable to computed properties and parameterless methods. Unlike the key-value store approach of Metalama Caching, memoization stores values directly within the object itself and has no concept of a caching key whatsoever.

For details, see <xref:memoization>.

### Observability (preview)

We are releasing a preview of our <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect implementing `INotifyPropertyChanged`. It will be finalized and documented in Metalama 2024.1.

## Additional enhancements

* Added MSBuild properties `MetalamaCompileTimeTargetFrameworks` and `MetalamaRestoreSources` to configure the compile-time target frameworks and to specify the package restore sources, respectively.
* Added MSBuild property `MetalamaCreateLamaDebugConfiguration` to disable the creation of the LamaDebug build configuration.
* Added a new member <xref:Metalama.Framework.Code.ICompilation.Cache?text=ICompilation.Cache> to cache often-used declarations across aspect instances.
* Dependency Injection aspect: <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute.IsRequired> default value follows the nullability of the target field.
* Added an environment variable, `METALAMA_TEMP`, to customize the location of the Metalama temp directory.
* Annotations: a facility to add and query custom annotations. See <xref:Metalama.Framework.Code.IAnnotation>, <xref:Metalama.Framework.Aspects.AdviserExtensions.AddAnnotation*?text=AdviserExtensions.AddAnnotation> and <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetAnnotations*?text=declaration.Enhancements().GetAnnotations()>.
* <xref:Metalama.Framework.Code.GenericExtensions.GetBase*?text=IMemberOrNamedType.GetBase> extension method: gets the base type or overridden member.
* <xref:Metalama.Framework.Code.AttributeExtensions.TryConstruct*?text=IAttribute.TryConstruct> extension method: creates a CLR instance of a compile-time custom attribute.
* <xref:Metalama.Framework.Code.ICompilation.GetAllAttributesOfType*?text=ICompilation.GetAllAttributesOfType>: gets all attributes of a given type in a project.
* <xref:Metalama.Framework.Code.IMemberOrNamedType.Definition?text=IMemberOrNamedType.Definition>: navigates to the generic definition.
* <xref:Metalama.Framework.Engine.Diagnostics.LocationExtensions.ToDiagnosticLocation*?text=Location.ToDiagnosticLocation>: converts a Roslyn `Location` into a Metalama <xref:Metalama.Framework.Diagnostics.IDiagnosticLocation>.

## Breaking changes

We continue to introduce low-impact breaking changes in the compile-time API because the platform is too young to enforce a strict forward-compatibility policy.

* The `ContractAspect.Direction` property has become the <xref:Metalama.Framework.Aspects.ContractAspect.GetDefinedDirection*> method.
* The `IConditionallyInheritableAspect.IsInheritable` property has become the <xref:Metalama.Framework.Aspects.IConditionallyInheritableAspect.IsInheritable*> method.
* <xref:Metalama.Extensions.DependencyInjection>
  * The <xref:Metalama.Extensions.DependencyInjection.DependencyProperties> record constructor has changed.
  * The framework is now configured with the new hierarchical options framework. The `DependencyInjectionOptions` is now internal. The new configuration public API is the <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*> extension method.
* For external namespaces, the <xref:Metalama.Framework.Code.IDeclaration.ContainingDeclaration> of the namespace now returns the declaring assembly instead of the current compilation.

> [!div class="see-also"]
> <xref:release-notes>
