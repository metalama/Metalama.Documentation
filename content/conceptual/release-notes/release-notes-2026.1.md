---
uid: release-notes-2026.1
level: 200
summary: "Metalama 2026.1 completes C# 14 support, extends initialization advice, brings major Redis caching improvements, reduces third-party dependencies, and closes most of the bug backlog."
keywords: "Metalama 2026.1, release notes, C# 14, extension blocks, operator introduction, initialization, dependency injection, Redis caching, System.Text.Json"
created-date: 2026-04-13
modified-date: 2026-04-14
---

# Metalama 2026.1

Metalama 2026.1 is a consolidation release, and the first long-term support (LTS) release of Metalama. It completes the C# 14 support started in 2026.0, extends the initialization and parameter-introduction advice, ports a major redesign of PostSharp.Patterns.Caching into the Redis caching backend, trims third-party dependencies, improves design-time performance, and closes most of the bug backlog.

**Highlights:**

- **C# 14 completion.** Every feature listed as a limitation in <xref:release-notes-2026.0> is now implemented, including extension block introductions, contracts on extension block parameters, and introduction of user-defined compound assignment operators.
- **Initialization advice.** New `AfterObjectInitializer` and `AfterLastInstanceConstructor` kinds, records supported by `BeforeInstanceConstructor`, forwarding overloads for constructor parameter introduction, and parameter reuse for dependency injection.
- **Redis caching backend.** Retry and exception-handling policies, key compression, overload detection, and many new configuration options.
- **Reduced third-party dependencies.** `System.Text.Json` replaces `Newtonsoft.Json`, `System.IO.Hashing` replaces `K4os.Hash`, and optional HTML/diff-tool components have been extracted into opt-in extension packages.
- **Performance enhancements.** Source-generated JSON serializers, MessagePack for design-time RPC, and pattern-matching optimizations.
- **Over 100 bug fixes**, i.e. almost the entire backlog.

## Requirements

Metalama 2026.1 has the same requirements as 2026.0. See <xref:requirements> for the full matrix.

- Visual Studio 2022 LTSC 17.12, 2022 17.14, or 2026 18.0 (latest build).
- .NET SDK 8.0, 9.0, or 10.0.
- C# 12, 13, or 14.

## C# 14 completion

All C# 14 features listed as limitations in <xref:release-notes-2026.0> are now implemented in 2026.1. 

### Introducing extension blocks

The new <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceExtensionBlock*> advice lets aspects introduce C# 14 extension blocks into a static class. The advice takes the receiver type and an optional receiver parameter name to choose between instance and static extension blocks, and supports the usual `IntroduceMethod`, `IntroduceProperty`, and so on for adding members.

See <xref:introducing-types> for a full example.

### Contracts on extension-block receiver parameters

Contracts such as `[NotNull]` or attributes from <xref:contract-patterns?text=Metalama.Patterns.Contracts> can now be applied to the receiver parameter of an extension block ([#1127](https://github.com/metalama/Metalama/issues/1127)). Metalama propagates the contract to every extension member in the block, so a single declaration validates `this`-equivalent receiver access across the entire extension block, including members introduced by other aspects.

### Introducing user-defined compound assignment operators

C# 14 lets types define their own compound assignment operators (`+=`, `-=`, `*=`, etc.). Aspects can now introduce these user-defined overloads ([#1131](https://github.com/metalama/Metalama/issues/1131)). Operator introduction goes through the standard <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceMethod*> advice by setting <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBuilder.OperatorKind>.

For details, see the _Introducing operators_ section of <xref:introducing-members>.

### Other C# 14 completions

- [#1109](https://github.com/metalama/Metalama/issues/1109): Use null-conditional assignments in templates.
- [#1114](https://github.com/metalama/Metalama/issues/1114): Use the `field` keyword in templates.
- [#1036](https://github.com/metalama/Metalama/issues/1036): Generate run-time code for extension members using invoker interfaces.
- [#1143](https://github.com/metalama/Metalama/issues/1143): Introduce parameters into partial constructors.

## Initialization advice

Metalama 2026.1 extends the initialization advice with two new initializer kinds: `AfterObjectInitializer` and `AfterObjectInitializer`. It improves `BeforeInstanceConstructor` on records. It also adds features to the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> advice.

### AfterLastInstanceConstructor

`InitializerKind.AfterLastInstanceConstructor` injects your initialization logic into a `protected virtual OnConstructed` method and ensures it runs after all instance constructors in the chain have completed. An <xref:Metalama.Framework.RunTime.Initialization.InitializationContext> parameter threaded through the constructor chain coordinates this across inheritance hierarchies.

### AfterObjectInitializer

`InitializerKind.AfterObjectInitializer` runs after the constructor _and_ any object initializer or `with` expression have completed. Metalama makes the target type implement <xref:Metalama.Framework.RunTime.Initialization.IInitializable> and rewrites _call sites_ to invoke `Initialize` automatically once construction and object initialization are done. Use this kind to validate or compute derived state after all fields and properties have been set in the object initializer.

### BeforeInstanceConstructor on records

`InitializerKind.BeforeInstanceConstructor` now supports records, including positional records. The initializer code is injected into the primary constructor.

See <xref:initializers> for examples.

### Constructor parameter introduction with forwarding overloads

The <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> advice now supports two mechanisms, both source-compatible:

- **Adding an optional parameter**: the overload taking a compile-time-constant `defaultValue` adds the parameter directly to the existing constructor. The IL signature changes, so binary compatibility is not preserved. This was the only available strategy prior to 2026.1, and remains the right choice for dependency-injection scenarios.
- **Adding a required parameter, pulled from a forwarding constructor**: the overload without `defaultValue` adds the new parameter as required, and generates a forwarding constructor that retains the pre-mutation signature and chains to the mutated constructor via `: this(...)`, preserving binary compatibility. The value supplied by the forwarding constructor comes from an <xref:Metalama.Framework.Advising.IPullStrategy>: for instance, <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> for a non-constant expression such as `DateTime.Now`.

The new <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardSourceConstructors> and <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardDefaultConstructor> strategies control when forwarding constructors are generated. Both expose a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy.WithObsoleteAttribute*> method so generated constructors can be deprecated.

See <xref:introducing-constructor-parameters>.

### Dependency injection

<xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*> now accepts a `reuseExistingParameterOfCompatibleType` argument ([#1552](https://github.com/metalama/Metalama/issues/1552)). When a chained constructor already accepts a parameter of the same or a more specific type, the existing parameter is forwarded instead of duplicated. The default .NET DI adapter relies on this: an aspect pulling `ILogger<T>` no longer adds a second `ILogger<T>` parameter if the target constructor already accepts one.

See <xref:dependency-injection>.

## Redis caching backend

The refactoring performed in the late summer 2025 to `PostSharp.Patterns.Caching.Redis` is now integrated into Metalama 2026.1. The motivation and full background are described in the [PostSharp blog post](https://blog.postsharp.net/redis-caching-update). For full documentation, see <xref:caching-redis>.

### New data schema

The data schema has been redesigned so that cache items are read with a single, observationally-consistent operation. The previous implementation reconciled the cache item and its dependencies, stored under separate Redis keys, through a client-side retry loop that could fail under sustained load in master/replica deployments. Dependencies are no longer flattened into the cache item; transitive dependencies are walked recursively at invalidation time instead. Cache keys are now versioned, so future schema changes can be rolled out alongside the running application.

A cache purge (`FLUSHDB`) is required after upgrading. See _Breaking changes_ below.

### Cluster and multi-node support

The backend now supports sharded Redis clusters through hash tags on cache keys, ensuring that related keys map to the same hash slot. <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.ReadCommandFlags> (default: `PreferReplica`) and <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.WriteCommandFlags> (default: `PreferMaster`) let you steer reads and writes between primary and replica nodes.

### Retry and exception-handling policies

Configurable retry and exception-handling policies are now exposed directly on <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration>, replacing the previous `ExceptionHandlingCachingBackendEnhancer`.

- **Retry policies** (<xref:Metalama.Patterns.Caching.Resilience.IRetryPolicy>): configurable retry logic with exponential backoff and jitter for transactions, background tasks, and recovery actions.
- **Exception handling policies** (<xref:Metalama.Patterns.Caching.Resilience.IExceptionHandlingPolicy>): control how exceptions are handled after retries are exhausted. The <xref:Metalama.Patterns.Caching.Resilience.DefaultExceptionHandlingPolicy> logs exceptions and attempts to recover from failed write operations.

### Key compression

Cache keys that exceed a configurable threshold (default: 128 characters) can now be automatically hashed using the <xref:Metalama.Patterns.Caching.Formatters.CacheKeyHashingAlgorithm> enum (`XxHash64` or `XxHash128`), avoiding Redis key length limits and improving performance with long keys.

### Overload detection

<xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend> now monitors its background task queue and exposes <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend.IsBackgroundTaskQueueOverloaded> and <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend.IsBackgroundTaskQueueOverloadedChanged>. The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> automatically pauses real-time notification processing when the backend is overloaded.

### New configuration options

New properties on <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration>:

- `TransactionRetryPolicy`, `BackgroundTasksRetryPolicy`, `BackgroundRecoveryRetryPolicy` for resilience.
- `BackgroundTasksMaxConcurrency` and `BackgroundTasksOverloadedThreshold` for overload management.
- `InvalidationMaxConcurrency` for throttling large graph invalidations.
- `KeyCompressingThreshold` for key compression.
- `DisposeTimeout`, `SupportsEvents`, `ReadCommandFlags`, `WriteCommandFlags`.

The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollectorOptions> class now provides configuration for the periodic cleanup process, including `CacheCleanupDelay` and <xref:Metalama.Patterns.Caching.Implementation.CacheCleanupOptions> (with `RemediationDelay` and `MaxConcurrency`).

## Supply chain security

Several transitive dependencies have been replaced with platform-provided APIs or extracted into optional extension packages, so projects that don't use a given component are no longer exposed to it.

- **`System.Text.Json` replaces `Newtonsoft.Json`** ([#741](https://github.com/metalama/Metalama/issues/741)).
- **`System.IO.Hashing` replaces `K4os.Hash`** ([#742](https://github.com/metalama/Metalama/issues/742)). Non-cryptographic hashes now use xxHash from the .NET runtime libraries.
- **Diff-tool integration extracted to `Metalama.Extensions.DiffEngine`** ([#446](https://github.com/metalama/Metalama/issues/446)). Add the package to have the diff tool launched automatically when an aspect test fails; without it, tests run normally and the diff tool is silently disabled. See <xref:aspect-testing> and <xref:diff-tool>.
- **HTML test output extracted to `Metalama.Extensions.HtmlWriter`** ([#447](https://github.com/metalama/Metalama/issues/447)). The only typical use of this package is the generation of Metalama's own documentation.
- MD5 is no longer used internally for non-cryptographic hashing ([#525](https://github.com/metalama/Metalama/issues/525)), so Metalama assemblies no longer trip security scanners that flag MD5 usage regardless of intent.

## Performance enhancements

- **Source-generated JSON serializers.** Metalama now uses `System.Text.Json` source generators for its configuration files.
- **MessagePack for design-time RPC** ([#1299](https://github.com/metalama/Metalama/issues/1299)). The protocol between the Metalama analyzer and the design-time services has been migrated from JSON to MessagePack.
- **Pattern-matching optimization.** Polymorphic matching on interface types has been restructured into matching by `DeclarationKind` or `TypeKind`.
- **Improved WPF compatibility** ([#1590](https://github.com/metalama/Metalama/issues/1590)). XAML files can now reference declarations introduced by Metalama aspects. The WPF temporary compilation phase (`MarkupCompilePass1`) runs a reduced pipeline that emits member signatures only, skipping the linker step since the temporary assembly is discarded after XAML type resolution.

## Other enhancements

### Contracts

All numeric contracts in <xref:Metalama.Patterns.Contracts> now accept types implementing `INumber<T>` (including generic type parameters), in addition to built-in numeric types ([#1543](https://github.com/metalama/Metalama/issues/1543)).

See the _Generic math support_ section of <xref:contract-types>.

### Code model

- **<xref:Metalama.Framework.Code.ICompilation.EntryPoint?text=ICompilation.EntryPoint>** returns the `Program.Main` method of the compilation, or `null` for library projects.
- **<xref:Metalama.Framework.Code.IEvent.RaiseMethod?text=IEvent.RaiseMethod>** returns `null` for events that cannot be raised (non-field-like, interface, and abstract events). The new <xref:Metalama.Framework.Code.Invokers.IEventInvoker.CanRaise?text=IEventInvoker.CanRaise> property lets you check before attempting to raise ([#771](https://github.com/metalama/Metalama/issues/771)).
- **`OfExactSignature`** overloads on <xref:Metalama.Framework.Code.Collections.IMethodCollection> and <xref:Metalama.Framework.Code.Collections.IConstructorCollection> ([#846](https://github.com/metalama/Metalama/issues/846)) locate methods or constructors by exact signature.
- **Generic parameter constraints** are now honored when matching signatures on <xref:Metalama.Framework.Code.Collections.IMethodCollection> ([#842](https://github.com/metalama/Metalama/issues/842)).
- **<xref:Metalama.Framework.Code.TypeFactory.TryGetType*?text=TypeFactory.TryGetType>** returns `false` instead of throwing when a type cannot be resolved.
- **<xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBaseBuilder.InsertParameter*?text=IMethodBaseBuilder.InsertParameter>** adds a parameter at an arbitrary position in an introduced method or constructor, not just at the end ([#657](https://github.com/metalama/Metalama/issues/657)).
- **<xref:Metalama.Framework.Project.IProject.Features?text=IProject.Features>** exposes a <xref:Metalama.Framework.Project.ProjectFeatures> object describing compiler and target-framework capabilities (for instance, `SupportsCovariantReturnTypes`), so aspects can adapt their output to the target project ([#1532](https://github.com/metalama/Metalama/issues/1532)).

### Templates and compile-time code

- **<xref:Metalama.Framework.Code.TypedConstant.NamedConstant*?text=TypedConstant.NamedConstant>** creates a `TypedConstant` that references an enum member or a `const` field by name, for instance run-time-only enum value.
- **<xref:Metalama.Framework.Code.Invokers.IMethodInvoker.CreateDelegateExpression*?text=IMethodInvoker.CreateDelegateExpression>** generates an expression that references a method as a delegate. Useful when you need to pass a method reference (e.g., to `EventHandler` or `Func<…>`) rather than invoke it. See <xref:invokers>.
- **Compile-time serialization** now supports value tuples (`ValueTuple` through `ValueTuple<T1,…,T7,TRest>`) and <xref:System.Index>/<xref:System.Range>. See <xref:aspect-serialization>.
- **<xref:Metalama.Framework.Code.AttributeExtensions.TryConstruct*?text=IAttribute.TryConstruct>** instantiates a custom attribute from a template, returning `false` if the attribute cannot be constructed at compile time ([#723](https://github.com/metalama/Metalama/issues/723)).

### Fabrics

<xref:Metalama.Framework.Fabrics.ITypeAmender> now implements <xref:Metalama.Framework.Aspects.IAdviser`1> ([#1487](https://github.com/metalama/Metalama/issues/1487)), so you can call `IntroduceMethod`, `Override`, `ImplementInterface`, and every other extension method from <xref:Metalama.Framework.Aspects.AdviserExtensions> directly on the `amender` parameter; no more `amender.Advice.*` boilerplate. Use <xref:Metalama.Framework.Aspects.IAdviser.With*> to target a specific member.

A new <xref:Metalama.Framework.Fabrics.ITypeAmender.Diagnostics> property of type <xref:Metalama.Framework.Diagnostics.ScopedDiagnosticSink> ([#724](https://github.com/metalama/Metalama/issues/724)) lets you report or suppress diagnostics scoped to the target type, just as you would from an aspect's `BuildAspect` method.

See <xref:fabrics-advising>.

### IDE and tooling

- You can now hide an aspect from **Aspect Explorer** via the <xref:Metalama.Framework.Aspects.EditorExperienceAttribute.HideFromAspectExplorer> property of the <xref:Metalama.Framework.Aspects.EditorExperienceAttribute> attribute ([#697](https://github.com/metalama/Metalama/issues/697)).
- **Read-only generated files.** The transformed files emitted under `obj/<Config>/<TFM>/metalama/` are now marked read-only, so accidental edits during a debugging session no longer go unnoticed (the edits would be overwritten on the next build) ([#745](https://github.com/metalama/Metalama/issues/745)).

## Bug backlog

More than 100 bug fixes shipped across the 2026.1 preview builds, covering the template engine, linker, code model, design-time services, dependency injection, and diagnostics.

For the full, per-build list of fixes, see the [2026.1 releases on GitHub](https://github.com/metalama/Metalama/releases?q=2026.1&expanded=true).

## Breaking changes

- **`AddInitializer` ordering** ([#1529](https://github.com/metalama/Metalama/issues/1529)): initializer order now respects `AspectOrderDirection.RunTime`. If you define an ordering with `[assembly: AspectOrder(AspectOrderDirection.RunTime, typeof(FirstAspect), typeof(SecondAspect))]`, the initializer from `FirstAspect` runs before the one from `SecondAspect`.
- **`IntroduceParameter` on record primary constructors** ([#1555](https://github.com/metalama/Metalama/issues/1555)): the introduced parameter is no longer materialized as part of the record's value shape by default: it does not generate an auto-property, does not appear in `Deconstruct`, and does not participate in `Equals`, `GetHashCode`, or `ToString`. Opt in explicitly with `PullStrategy.IntroduceParameterAndPull(materializeOnRecord: true)`.
- **<xref:Metalama.Framework.Code.IConstructor.InitializerKind?text=IConstructor.InitializerKind>** now returns `Base` (instead of `None`) for implicit `base()` calls.
- **<xref:Metalama.Framework.Aspects.TemplateAttribute.IsVirtual?text=TemplateAttribute.IsVirtual>** is disregarded when the target type is `sealed`.
- **<xref:Metalama.Framework.Code.IEvent.RaiseMethod?text=IEvent.RaiseMethod>** returns `null` for events that cannot be raised. Use <xref:Metalama.Framework.Code.Invokers.IEventInvoker.CanRaise?text=IEventInvoker.CanRaise> to check before raising.
- **Removed test base classes**: the `AspectTestClass`, `DefaultAspectTestClass`, `CurrentDirectoryAttribute`, and `CurrentProjectAttribute` classes have been removed from `Metalama.Testing.AspectTesting`. Tests are now discovered automatically; no test-runner code is needed.

### Redis caching

- **Data schema redesign**: the Redis data schema has been redesigned. A cache purge (`FLUSHDB`) is required after upgrading to 2026.1.
- **Removed `ExceptionHandlingCachingBackendEnhancer`**: use the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.ExceptionHandlingPolicy> property instead.
- **`TransactionMaxRetries` is obsolete**: use <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.TransactionRetryPolicy> instead.
- **`CacheCleanupOptions.Sequential` is obsolete**: use <xref:Metalama.Patterns.Caching.Implementation.CacheCleanupOptions.MaxConcurrency> instead.
- **`CacheItem.Dependencies`**: now exposes direct dependencies only. Transitive dependencies are resolved at invalidation time rather than flattened into the item.

> [!div class="see-also"]
> <xref:release-notes>
> <xref:requirements>
> <xref:introducing-members>
> <xref:introducing-types>
> <xref:introducing-constructor-parameters>
> <xref:initializers>
> <xref:dependency-injection>
> <xref:fabrics-advising>
> <xref:contract-types>
> <xref:caching-redis>
