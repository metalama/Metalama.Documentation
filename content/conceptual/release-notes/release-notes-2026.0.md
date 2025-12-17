---
uid: release-notes-2026.0
level: 200
summary: "Metalama 2026.0 provides full C# 14 and .NET 10 SDK support, introduces first-class tuple types, and enables event handler invocation overriding."
keywords: "Metalama 2026.0, release notes"
created-date: 2025-11-01
modified-date: 2025-12-09
---

# Metalama 2026.0

Metalama 2026.0 brings full support for C# 14, the most significant evolution of the C# language in many years.

**Highlights:**

- **C# 14 and .NET 10 SDK support**, including extension blocks, partial constructors, partial events, and compound assignment operators
- **First-class tuple types** with direct access to element names and types
- **Event handler invocation overriding** for implementing patterns like safe events
- **Faster Visual Studio experience** with significant performance improvements

Metalama 2026.0 ensures you're ready to take full advantage of the latest C# features while keeping your aspects clean, powerful, and maintainable.

## Requirements

### Development environment

Metalama 2026.0 supports the following development environments and SDKs:

- Visual Studio:
  - 2022 LTSC 17.12 (latest build)
  - 2022 17.14 (latest build)
  - 2026 18.0 (latest build)
- .NET SDK 8.0, 9.0, or 10.0.
- C# 12, 13, or 14.

> [!WARNING]
> .NET 6 SDK has been deprecated in this release ([#1092](https://github.com/metalama/Metalama/issues/1092)).

### Target frameworks (runtimes)

Metalama 2026.0 supports the following target frameworks:

- **Metalama.Framework** and **Metalama.Extensions**: any framework implementing the .NET Standard 2.0 API (language polyfills might be required for some frameworks, see for instance [PolySharp](https://github.com/Sergio0694/PolySharp)).
- **Metalama.Patterns**: .NET Framework 4.7.2 (tested), .NET 8.0 (tested), or any framework implementing the .NET Standard 2.0 API (untested).

> [!WARNING]
> .NET 6 has been deprecated as a tested runtime.

## C# 14 support

Metalama 2026.0 provides extensive support for C# 14 language features. While most features are fully implemented, some remain on the roadmap for future releases.

### Implemented in 2026.0

Metalama 2026.0 supports the following C# 14 features:

- [#1108](https://github.com/metalama/Metalama/issues/1108): Use null-conditional assignments when generating syntax from an <xref:Metalama.Framework.Code.IFieldOrPropertyOrIndexer> (when assigning their `Value` property). Use the <xref:Metalama.Framework.Code.Invokers.IFieldOrPropertyInvoker.WithOptions*> and specify `NullConditional`.
- [#1094](https://github.com/metalama/Metalama/issues/1094): Override a property that uses the `field` keyword.
- [#1110](https://github.com/metalama/Metalama/issues/1110): Override or introduce to a partial constructor.
- [#1111](https://github.com/metalama/Metalama/issues/1111): Add an instance initializer to a partial constructor.
- [#1112](https://github.com/metalama/Metalama/issues/1112): Introduce partial events.
- [#1113](https://github.com/metalama/Metalama/issues/1113): Override partial events.
- [#1034](https://github.com/metalama/Metalama/issues/1034): Query extension blocks and extension members from the code model (see below).
- [#1035](https://github.com/metalama/Metalama/issues/1035): Override members of extension blocks.
- [#1115](https://github.com/metalama/Metalama/issues/1115): Query compound assignment operators in the code model.
- [#1116](https://github.com/metalama/Metalama/issues/1116): Override compound assignment operators.
- [#1160](https://github.com/metalama/Metalama/issues/1160): Introduce new extension members into existing extension blocks.
- [#1041](https://github.com/metalama/Metalama/issues/1041): Use simple lambda parameters with modifiers both in compile-time and run-time code.
- [#1105](https://github.com/metalama/Metalama/issues/1105): When an unsupported feature is used in a template, an understandable error message is reported.

### Limitations

The following C# 14 features haven't been implemented in Metalama 2026.0:

- [#1109](https://github.com/metalama/Metalama/issues/1109): Use null-conditional assignments in templates.
- [#1114](https://github.com/metalama/Metalama/issues/1114): Use the `field` keyword in templates.
- [#1036](https://github.com/metalama/Metalama/issues/1036): Generate run-time code for extension members using invoker interfaces.
- [#1127](https://github.com/metalama/Metalama/issues/1127): Add a contract to the receiver parameter of extension blocks.
- [#1131](https://github.com/metalama/Metalama/issues/1131): Introducing new compound assignment operators.
- [#1143](https://github.com/metalama/Metalama/issues/1143): Introducing parameters into partial constructors.
- [#1159](https://github.com/metalama/Metalama/issues/1159): Introducing new extension blocks.

## Extension blocks

Extension blocks represent the flagship feature of C# 14, enabling the extension of any type with new members. Metalama 2026.0 provides comprehensive support for extension blocks, including the ability to override extension members.

Extension blocks are modeled using the <xref:Metalama.Framework.Code.IExtensionBlock> interface, which derives from <xref:Metalama.Framework.Code.INamedType> with the following characteristics:

- <xref:Metalama.Framework.Code.ICompilationElement.DeclarationKind> is `Extension` and <xref:Metalama.Framework.Code.IType.TypeKind> is `Extension`.
- Adds <xref:Metalama.Framework.Code.IExtensionBlock.ReceiverParameter> and <xref:Metalama.Framework.Code.IExtensionBlock.ReceiverType> properties.
- For extension members (excluding classic extension methods), <xref:Metalama.Framework.Code.IMember.DeclaringType?text=IMember.DeclaringType> references the <xref:Metalama.Framework.Code.IExtensionBlock>.

> [!WARNING]
> Although <xref:Metalama.Framework.Code.IExtensionBlock> implements <xref:Metalama.Framework.Code.INamedType>, an extension block _cannot_ be used as an <xref:Metalama.Framework.Code.IType>. This behavior breaks the Liskov Substitution Principle, but it is much simpler than changing the type of <xref:Metalama.Framework.Code.IMember.DeclaringType?text=IMember.DeclaringType> property.

Extension blocks are accessible through <xref:Metalama.Framework.Code.INamedType.ExtensionBlocks?text=INamedType.ExtensionBlocks>, _not_ as nested types in <xref:Metalama.Framework.Code.INamespaceOrNamedType.Types?text=INamedType.Types>.

Extension methods and property accessors are available in <xref:Metalama.Framework.Code.INamedType.Methods?text=INamedType.Methods> as implicitly-implemented methods. These methods exist in IL and are addressable in C#, but cannot be overridden with Metalama.

This implementation aligns closely with Roslyn's model, providing a natural experience for C# developers.

## First-class support for tuple types

Metalama 2026.0 introduces first-class support for named tuples through the new <xref:Metalama.Framework.Code.ITupleType> interface. Previously, tuples were treated as plain <xref:Metalama.Framework.Code.INamedType> objects without access to element types or names, making scenarios such as argument packing for interceptors cumbersome and inefficient.

The new implementation provides direct access to element names and types through <xref:Metalama.Framework.Code.TypeFactory.CreateTupleType*?text=TypeFactory.CreateTupleType> for creation, and <xref:Metalama.Framework.Code.ITupleType.CreateCreateInstanceExpression*?text=ITupleType.CreateCreateInstanceExpression> for instantiation.

The implementation supports tuples with any number of elements:

- For tuples with two or more elements: native tuple syntax
- For degenerate cases (zero or one element): automatic fallback to `ValueTuple.Create(...)`

For details, see <xref:type-system>.

## Event handler invocation overriding

Metalama 2026.0 introduces the capability to override event handler invocations ([#549](https://github.com/metalama/Metalama/issues/549)). This extends the existing functionality that allowed overriding only the add and remove operations of events.

This advice kind allows you to implement aspects such as "safe events", where event handlers are isolated from each other by an exception handler.

For comprehensive documentation, see <xref:overriding-events>.

## Metrics

Metalama 2026.0 enhances code metrics capabilities with new built-in metrics and improved API support for consuming metrics from standalone applications.

### Lines of Code metric

The `Metalama.Extensions.Metrics` package now includes a comprehensive <xref:Metalama.Extensions.Metrics.LinesOfCode> metric that provides three distinct measurements ([#1216](https://github.com/metalama/Metalama/issues/1216)):

- **Logical**: Counts lines of substantive code, excluding braces, blank lines, and comments
- **NonBlank**: Counts any line containing non-whitespace content
- **Total**: Measures the total line span from the first to the last line of a declaration

This metric complements the existing <xref:Metalama.Extensions.Metrics.StatementsCount> and <xref:Metalama.Extensions.Metrics.SyntaxNodesCount> metrics, enabling more nuanced code complexity analysis.

### Workspaces API metrics support

The <xref:Metalama.Framework.Workspaces> API now officially supports metrics consumption without requiring internal APIs ([#1209](https://github.com/metalama/Metalama/issues/1209)). The <xref:Metalama.Framework.Workspaces.ServiceProviderBuilder`1> class is now part of the public SDK namespace, enabling extension packages to provide clean integration methods:

```csharp
WorkspaceCollection.Default.ServiceBuilder.AddMetrics();
```

This eliminates previous workarounds involving internal API warnings and compile-time/run-time boundary violations, making it straightforward to consume metrics from standalone applications and build tools.

For details on using metrics, see <xref:metrics>. For creating custom metrics, see <xref:custom-metrics>.

## Visual Studio Tools for Metalama: performance improvements

Visual Studio Tools for Metalama includes refactored components that dramatically improve performance. It's now more stable, consumes less CPU, and makes better use of your cores.

## Additional improvements

- **Toast notifications for product news** ([#1161](https://github.com/metalama/Metalama/issues/1161)). Visual Studio Tools for Metalama can now display toast notifications when new blog posts or product briefs are published.

- **User-defined checked operators** ([#1133](https://github.com/metalama/Metalama/issues/1133)). Metalama 2026.0 adds support for introducing user-defined `checked` operators.

- **Cross-project dependency injection** ([#568](https://github.com/metalama/Metalama/issues/568)). Enhanced dependency injection capabilities now allow pulling constructor parameters across project boundaries. See <xref:dependency-injection>.

- **Compile-time assembly resolver** ([#1088](https://github.com/metalama/Metalama/issues/1088)). The component that downloads compile-time assembly now properly respects the project's `nuget.config` file for package resolution.

- **Single-file dotnet run support** ([#1107](https://github.com/metalama/Metalama/issues/1107)). Metalama now supports Microsoft's single-file `dotnet run` functionality, allowing aspects to work with modern .NET single-file deployment scenarios.

- **ExcludeAspect enhancement** ([#1176](https://github.com/metalama/Metalama/issues/1176)). The `[ExcludeAspect]` attribute, when applied to a field or property, now implicitly applies to its accessors.

## Documentation updates

- New article: <xref:type-system>.
- Improved the chapter: <xref:templates>.
- Improved the API documentation by adding elements from the conceptual documentation where relevant.
- **Claude Code plugin**: A new plugin is available to enhance [Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview), Anthropic's AI-powered coding assistant, with comprehensive Metalama knowledge. The plugin provides access to conceptual documentation, API references, sample code, and pattern libraries, enabling Claude to assist with aspect development, templates, fabrics, and the Metalama code model. Install from the [Metalama.AI.Skills marketplace](https://github.com/metalama/Metalama.AI.Skills). See <xref:ide-claude-code> for complete installation and usage instructions.

## New APIs

### Type System - Nullability Handling

- **`IType.StripNullabilityAnnotation()`**: Strips the nullability annotation from a type, returning an unannotated version (where `IsNullable` is null). Type-specific overrides return `INamedType`, `IArrayType`, `IDynamicType`, and `ITypeParameter` respectively ([#1232](https://github.com/metalama/Metalama/issues/1232)).
- **`ExpressionFactory.WithNullForgivingOperator(IExpression, bool force)`**: Adds the null-forgiving operator (!) to an expression ([#1232](https://github.com/metalama/Metalama/issues/1232)).

### Extensibility APIs

- **`IProjectServiceFactory`**: Interface allowing extensions to create project services loaded from external assemblies ([#1223](https://github.com/metalama/Metalama/pull/1223)).
- **`ExtensionKinds.ServiceFactory`**: New flag enabling service factory extensions ([#1223](https://github.com/metalama/Metalama/pull/1223)).
- **`TemplateAttribute.Id`**: Property for assigning stable identifiers to templates, enabling template lookup by ID in addition to member name ([#1223](https://github.com/metalama/Metalama/pull/1223)).

### Code Model - Pseudo Members

- **`IDeclaration.ImplementationKind`**: Property (moved from `IMemberOrNamedType` to the broader `IDeclaration` interface) that indicates whether a declaration represents a real symbol or a pseudo member ([#828](https://github.com/metalama/Metalama/issues/828)).
- **`EligibilityExtensions.MustNotBePseudoMember()`**: Extension method for `IDeclaration` that filters out pseudo members from eligibility checks, preventing aspects from incorrectly flowing between real members and their pseudo counterparts ([#828](https://github.com/metalama/Metalama/issues/828)).

## Breaking changes

- <xref:Metalama.Framework.Code.IType.TypeKind?text=INamedType.TypeKind> now returns `Tuple` instead of `NamedType` for tuples.
- <xref:Metalama.Framework.Aspects.IAspectBuilder.Advice?text=IAspectBuilder.Advice> is now obsolete. Use <xref:Metalama.Framework.Aspects.IAdviser> instead.
- The <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.With*> method has been split into <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithObject*> and <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithOptions*> with additional overloads. This change applies to all kinds of members.
- <xref:Metalama.Framework.Aspects.IAdviser> and <xref:Metalama.Framework.Aspects.AdviserExtensions> have been moved to the `Metalama.Framework.Aspects` namespace.
- `TypeKind.RecordClass` and `TypeKind.RecordStruct` have been removed and replaced by <xref:Metalama.Framework.Code.INamedType.IsRecord?text=INamedType.IsRecord>.
- The `IntroduceConversionOperator` method now has an additional optional parameter to support `checked` operators.
- **ServiceLocator self-registration removed**: The <xref:Metalama.Extensions.DependencyInjection.ServiceLocator> adapter no longer automatically registers itself via a <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric>. Projects using this adapter must now explicitly configure the framework by creating a <xref:Metalama.Framework.Fabrics.ProjectFabric> class that calls `RegisterFramework<ServiceLocatorDependencyInjectionFramework>()` within the `ConfigureDependencyInjection` method.
- **Elvis operator behavior change in templates**: The null-conditional operator (`?.`) is no longer simplified for non-nullable reference types in templates. It is still simplified for non-nullable value types. This may result in different generated code compared to previous versions.
- **Test framework changes**: The `@AcceptInvalidInput` and `@ReportOutputWarnings` directives have been removed; both behaviors are now enabled by default. A new `@SkipDiffTool` annotation has been introduced to replace the previous mechanism for suppressing diff tool execution during tests.
- **Field pseudo-accessor behavior change** ([#828](https://github.com/metalama/Metalama/issues/828)): The <xref:Metalama.Framework.Code.IDeclaration.IsImplicitlyDeclared?text=IsImplicitlyDeclared> property for field pseudo-accessors now returns `false` instead of `true`, since they represent explicitly declared fields. This may affect aspects that rely on `IsImplicitlyDeclared` for eligibility rules. Use the new <xref:Metalama.Framework.Code.IDeclaration.ImplementationKind> property or <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustNotBePseudoMember*> method to explicitly filter pseudo members.

> [!div class="see-also"]
> <xref:release-notes>
> <xref:requirements>
> <xref:overriding-events>
> <xref:type-system>
> <xref:dependency-injection>
