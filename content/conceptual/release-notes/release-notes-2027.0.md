---
uid: release-notes-2027.0
level: 200
summary: "Metalama 2027.0 adds C# 15 and .NET 11 support, tools that make Metalama usable by AI coding agents, call-site interceptors, the introduction of structs, records, enums, delegates and unions, type facets in the code model, and analyzers that verify immutable and durable aspect code."
keywords: "Metalama 2027.0, release notes, C# 15, .NET 11, AI coding agents, MCP server, metalama preview, metalama introspect, unions, closed classes, extension indexers, interceptors, type introduction, type facets, immutable aspects"
created-date: 2026-10-13
modified-date: 2026-11-19
---

# Metalama 2027.0

> [!NOTE]
> Metalama 2027.0 is in preview. This article describes the features shipped up to build 2027.0.7-preview, and will be updated as new preview builds ship.

Metalama 2027.0 aligns Metalama with .NET 11 and C# 15, and moves the platform baseline to Visual Studio 2026 and .NET 10. It also introduces call-site interceptors, extends type introduction to every kind of type, and adds analyzers that verify that aspect code follows the rules of the framework.

**Highlights:**

- **AI readiness.** Coding agents can preview the code that aspects generate, query which aspects apply and what they change, explore the code model through an MCP server, and follow each diagnostic to its documentation.
- **C# 15 support**, including unions, closed classes, extension indexers, and collection expression arguments.
- **Call-site interceptors.** The new `Metalama.Extensions.Interceptors` package redirects calls to methods of any assembly to an interceptor that you write or that Metalama generates from a template.
- **Type introduction.** Aspects can now introduce structs, records, enums, delegates, and unions, in addition to classes, interfaces, and extension blocks.
- **Type facets.** The code model exposes the structure specific to delegates, enums, records, and unions.
- **Analyzers for aspect authors.** Warnings when an aspect, fabric, or validator keeps mutable state, or when an object held across compilations pins a compilation.
- **Error reference.** Every `LAMA` error and warning now has its own documentation article.
- **Licensing.** Support for license servers, and no license required to build unmodified source code.

## Requirements

Metalama 2027.0 raises the platform baseline. See <xref:requirements> for the full matrix.

- Visual Studio 2026. Visual Studio 2022 is no longer supported, because version 17.14 leaves mainstream support shortly after the release of Metalama 2027.0 ([#1896](https://github.com/metalama/Metalama/issues/1896)).
- .NET SDK 10.0 or later, including the .NET 11 SDK ([#1927](https://github.com/metalama/Metalama/issues/1927)). Metalama is built on Roslyn 5.12, the C# compiler of the .NET SDK 11.0.100, which Visual Studio 2026 18.11 and 18.12 also use ([#2341](https://github.com/metalama/Metalama/issues/2341)).
- C# up to 15.
- Projects targeting .NET 10, .NET 11, or .NET Framework 4.8 or later. .NET Framework 4.7.2 is no longer a supported runtime, although projects targeting `net472` keep working. Other platforms compatible with .NET Standard 2.0 remain usable but untested.

Metalama packages now ship `net10.0` and `net472` assets, in addition to `netstandard2.0`. The `net8.0` and `net9.0` assets have been removed ([#1876](https://github.com/metalama/Metalama/issues/1876)).

### Warnings for untested configurations

The build now reports a warning when the target framework, the .NET SDK, or the Visual Studio version is outside the matrix that Metalama tests ([#1884](https://github.com/metalama/Metalama/issues/1884)). Each dimension has its own warning code, starting with `LAMA0600`, so you can suppress one dimension with `NoWarn`. To turn the check off entirely, set the `MetalamaCheckSupportedPlatform` MSBuild property to `False`.

Packages built on Metalama can declare stricter requirements with the `MetalamaPlatformRequirement` MSBuild item, and the same check reports them ([#1887](https://github.com/metalama/Metalama/issues/1887)).

A project that targets .NET Framework 4.7.2 builds and runs, but .NET Framework 4.7.2 is no longer supported. The build reports this with the `LAMA0604` warning. The oldest supported version is .NET Framework 4.8, which is an in-place update and does not require you to change the target framework of your project ([#2023](https://github.com/metalama/Metalama/issues/2023)).

## AI readiness

Metalama transforms code during compilation, so the source code that an AI coding agent reads doesn't show what the aspects do. A developer can look at the Metalama Diff and the Aspect Explorer of Visual Studio, but an agent can't. Metalama 2027.0 gives agents command-line and MCP equivalents of these tools, so that they can check their own work.

- **Previewing the transformed code.** The `metalama preview` command prints the transformed and formatted code of one or a few files without building the project. All aspects and fabrics of the project run, so aspects applied from other files are included ([#2313](https://github.com/metalama/Metalama/issues/2313)). See <xref:previewing-transformed-code>.
- **Querying the aspects of the last build.** When the `MetalamaWriteIntrospectionFile` MSBuild property is `True`, the build writes an introspection file (`.mlsym`). The `metalama introspect` command reads it and answers which aspects apply to a declaration, what created them, which transformations they make and in which order, and which diagnostics they reported ([#2300](https://github.com/metalama/Metalama/issues/2300)). See <xref:introspecting-from-command-line>.
- **MCP server.** The new `metalama-mcp` tool, from the `Metalama.Mcp` package, is a local Model Context Protocol server. An agent loads a project or a solution, including `.slnx` solutions, and runs C# queries over the Metalama code model: declarations, references, aspect instances, transformations, diagnostics, metrics, and the transformed code ([#2377](https://github.com/metalama/Metalama/issues/2377)). You can also open the results in your browser. See <xref:mcp-server>.
- **Plugin for Claude Code and OpenAI Codex.** The Metalama plugin gives the agent the whole documentation, the API reference, and the samples, and tells it when to use the tools above. In addition to Claude Code, it's now available for OpenAI Codex. See <xref:ai-agents>.
- **Documentation links in diagnostics.** The IDE shows each Metalama diagnostic ID as a link to its article ([#2319](https://github.com/metalama/Metalama/issues/2319)). Your own diagnostics can link to your documentation with the new <xref:Metalama.Framework.Diagnostics.IDiagnosticDefinition.HelpLinkUri?text=HelpLinkUri> property. See <xref:diagnostics>.

## C# 15 support

Metalama 2027.0 supports the C# 15 features of .NET 11 ([#1926](https://github.com/metalama/Metalama/issues/1926)) in target code, in the code model, and in templates. Compile-time code and templates can also use preview language features when the project enables them ([#1979](https://github.com/metalama/Metalama/issues/1979)).

### Unions

Aspects can be applied to union types and to their members ([#1940](https://github.com/metalama/Metalama/issues/1940)). The new <xref:Metalama.Framework.Code.INamedType.IsUnion?text=INamedType.IsUnion> property and the <xref:Metalama.Framework.Code.Types.IUnionFacet> facet describe the case types of a union. The linker, the advice, and the declaration comparers handle unions, and the pattern libraries treat them correctly: for instance, caching computes a cache key from the case value instead of treating the union as an opaque struct ([#1946](https://github.com/metalama/Metalama/issues/1946)).

Aspects can also introduce a union with the new <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceUnion*> advice ([#1951](https://github.com/metalama/Metalama/issues/1951)).

### Closed classes

The new <xref:Metalama.Framework.Code.INamedType.IsClosed?text=INamedType.IsClosed> property reports whether a class is closed ([#1939](https://github.com/metalama/Metalama/issues/1939)). Aspects can introduce a closed class by setting <xref:Metalama.Framework.Code.DeclarationBuilders.INamedTypeBuilder.IsClosed?text=INamedTypeBuilder.IsClosed> ([#1950](https://github.com/metalama/Metalama/issues/1950)).

### Other C# 15 features

- [#1949](https://github.com/metalama/Metalama/issues/1949): Advise and introduce extension indexers.
- [#1948](https://github.com/metalama/Metalama/issues/1948): Use collection expression arguments in templates.
- [#1938](https://github.com/metalama/Metalama/issues/1938): Introduce non-virtual static members into interfaces in projects targeting .NET Framework.
- [#1947](https://github.com/metalama/Metalama/issues/1947): Labels, including the labels of `break` and `continue` statements, are reported as unsupported in templates.

## Call-site interceptors

Interceptors are a new way to change the behavior of code. Instead of overriding a method, which changes it for every caller and requires its source code, an interceptor replaces the _calls_ to a method in the code you select. The intercepted method can belong to any assembly, including the .NET libraries and NuGet packages ([#2119](https://github.com/metalama/Metalama/issues/2119)).

Interceptors are registered from an aspect or a fabric and require the new `Metalama.Extensions.Interceptors` package, which works with a free Metalama Community license ([#2122](https://github.com/metalama/Metalama/issues/2122)). The first version supports:

- **Existing interceptor methods.** Redirect calls to a static method that you write. Parameters and type parameters are bound automatically by rules, or explicitly when the rules can't bind them ([#2097](https://github.com/metalama/Metalama/issues/2097), [#2126](https://github.com/metalama/Metalama/issues/2126), [#2118](https://github.com/metalama/Metalama/issues/2118)).
- **Interceptors generated from templates.** One T# template generates the interceptors of many methods with different signatures. The template reads the intercepted call through `meta.Interception` and can adjust the signature of the generated method ([#2128](https://github.com/metalama/Metalama/issues/2128), [#2133](https://github.com/metalama/Metalama/issues/2133)). Call sites that need the same interceptor share one generated method ([#2131](https://github.com/metalama/Metalama/issues/2131)).
- **Caller context.** Interceptors can receive caller information, the calling instance, or a parameter of the calling member ([#2129](https://github.com/metalama/Metalama/issues/2129)).
- **A fluent registration API** that selects the intercepted methods, the calling code, and the interceptor ([#2141](https://github.com/metalama/Metalama/issues/2141)).

See <xref:interceptors>.

## Type introduction

Aspects can now introduce every kind of type. In addition to classes, interfaces, and extension blocks, the following advice methods are available on <xref:Metalama.Framework.Aspects.AdviserExtensions>:

- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceStruct*> ([#869](https://github.com/metalama/Metalama/issues/869)).
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceRecord*>, for record classes and record structs ([#867](https://github.com/metalama/Metalama/issues/867)).
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceEnum*> ([#866](https://github.com/metalama/Metalama/issues/866)).
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceDelegate*> ([#865](https://github.com/metalama/Metalama/issues/865)).
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceUnion*> (see above).

See <xref:introducing-types>.

## Code model

### Type facets

A _facet_ is the structure that a named type has because of its kind. The new <xref:Metalama.Framework.Code.INamedType.Facets?text=INamedType.Facets> property exposes it through typed properties, for instance `type.Facets.Delegate?.ReturnType`:

- The delegate facet (<xref:Metalama.Framework.Code.Types.IDelegateFacet>) exposes the signature of a delegate type, so you no longer need to look up its `Invoke` method by name ([#1995](https://github.com/metalama/Metalama/issues/1995)).
- The enum facet (<xref:Metalama.Framework.Code.Types.IEnumFacet>) exposes the underlying type of an enum ([#1996](https://github.com/metalama/Metalama/issues/1996)).
- The record facet (<xref:Metalama.Framework.Code.Types.IRecordFacet>) exposes the members that the compiler synthesizes for a record, such as `EqualityContract`, `PrintMembers`, the copy constructor, and `Deconstruct` ([#1997](https://github.com/metalama/Metalama/issues/1997)).
- The union facet (<xref:Metalama.Framework.Code.Types.IUnionFacet>) exposes the kind and the cases of a union.

For details, see <xref:type-system#type-facets>.

### Builders

- New <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBaseBuilder.AddParameter*?text=AddParameter> and <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBuilder.AddTypeParameter*?text=AddTypeParameter> overloads copy an existing parameter or type parameter into a builder, with its constraints and, optionally, its attributes and default value ([#2140](https://github.com/metalama/Metalama/issues/2140)). This makes it easier to copy a signature without losing information.
- Type parameters added through a builder can now be used in arrays, nullable types, pointer types, and generic instances, so an aspect can introduce a generic method whose parameters use its own type parameters ([#2139](https://github.com/metalama/Metalama/issues/2139)).

### Advising records

- `meta.Proceed()` now works when overriding a compiler-synthesized record member such as `Equals` or `GetHashCode`. Previously, the generated code threw a `NotSupportedException` ([#1343](https://github.com/metalama/Metalama/issues/1343)).
- `AddInitializer` and `Override` can now target the primary constructor of a record introduced by an aspect, as they already could for a record declared in source ([#2020](https://github.com/metalama/Metalama/issues/2020)).

## Analyzers for aspect authors

Two new contracts, verified by analyzers shipped in the `Metalama.Framework` package, catch design errors while you type. Both report warnings only.

- **Immutable style** ([#1872](https://github.com/metalama/Metalama/issues/1872)). An aspect is instantiated once and reused for every target, and at design time across compilations, so state stored on it leaks from one target to the next. Aspects, fabrics, and validators are now verified against the <xref:Metalama.Framework.Utilities.ImmutableTypeAttribute?text=[ImmutableType]> contract, and an assignment to a field outside the constructor is reported.
- **Durability** ([#1829](https://github.com/metalama/Metalama/issues/1829)). The <xref:Metalama.Framework.Utilities.DurableAttribute?text=[Durable]> contract declares that an object can be held across compilations. The analyzer reports a field that could keep a `Compilation`, a syntax tree, or a symbol alive, and so cause memory leaks in the IDE. The diagnostics are `LAMA0870` to `LAMA0879`.

Both attributes are in the <xref:Metalama.Framework.Utilities> namespace.

## Licensing

### License server

Metalama now supports the PostSharp license server ([#946](https://github.com/metalama/Metalama/issues/946)). Instead of a license key, you can register the URL of a license server that your organization hosts, either in your user profile with `metalama license register` or in the `MetalamaLicense` MSBuild property. Metalama then leases a license from the server for a few days and renews it automatically, so one machine contacts the server about once every two days, not at every build. A toast notification warns you when the server stops answering before the lease ends.

Unattended processes, such as build servers, never lease a license from the server, so they don't take seats from the pool.

### No license required for unmodified source code

A repository that uses Metalama Premium components can now be built without a license as long as its source code is unmodified, for instance by a contributor or a user who clones an open-source project to build it ([#957](https://github.com/metalama/Metalama/issues/957)). The check is opt-in and is controlled by the `MetalamaVcsCheck` MSBuild property:

| Value | Unmodified source tree | Modified file |
|-------|------------------------|---------------|
| `Disabled` (default) | A license is required. | A license is required. |
| `AcquireLicenseOnChange` | No license is required. | A license is required. |
| `FailOnChange` | No license is required. | The build fails. |

A file counts as modified when git reports a content modification of a tracked file. Untracked, added, deleted, and renamed files don't count.

### Other licensing improvements

- Metalama Community can be registered outside Visual Studio, from the command line with `metalama license community --reason <reason>` and from the browser-based setup pages ([#2314](https://github.com/metalama/Metalama/issues/2314)). See <xref:register-license>.
- License keys signed with Elliptic Curve DSA are supported. Finite field DSA is no longer available on macOS with .NET 11 ([#1860](https://github.com/metalama/Metalama/issues/1860), [#1864](https://github.com/metalama/Metalama/issues/1864)).
- Registered license keys are grouped by the minimal Metalama version that can read them, so registering a new key no longer causes warnings in older versions of Metalama installed on the same machine ([#1922](https://github.com/metalama/Metalama/issues/1922)).
- The licensing error `LAMA0806` no longer writes the full license keys to the build log ([#2183](https://github.com/metalama/Metalama/issues/2183)).

## Documentation: error and warning reference

Each error and warning reported by Metalama, its extensions, and the Metalama.Patterns libraries now has its own article, explaining the cause of the diagnostic and showing how to resolve it, usually with an example that reports it and a corrected version of the same code. The articles are grouped by the feature that reports the diagnostic.

See <xref:errors>.

## Tooling

- **`metalama shutdown`** stops the compiler server, the MSBuild nodes, and the background processes of Metalama, so that the files they hold are released, for instance before an upgrade ([#2088](https://github.com/metalama/Metalama/issues/2088)). Running compilations finish first. Use `--force` to cancel them, and `--all` to also end IDE processes. See _Breaking changes_ below for the effect on `metalama kill`.
- **`MetalamaAssemblyLocatorBinaryLogDirectory`**: when the nested build that resolves the compile-time reference assemblies fails (`LAMA0082`), its binary log is copied to the directory set by this MSBuild property, so it can be collected from a CI agent ([#1920](https://github.com/metalama/Metalama/issues/1920)).
- **Faster parallel builds.** Usage telemetry now stores one record per project, so parallel builds of large solutions no longer wait on a shared lock ([#2092](https://github.com/metalama/Metalama/issues/2092)).

## Bug fixes

The 2027.0 preview builds also include many bug fixes, most of them in the reporting of diagnostics. For the full, per-build list of fixes, see the [2027.0 releases on GitHub](https://github.com/metalama/Metalama/releases?q=2027.0&expanded=true).

## Breaking changes

- **Platform baseline**: Visual Studio 2022 is no longer supported, the minimum .NET SDK is 10.0, and the `net8.0` and `net9.0` package assets have been replaced by `net10.0` ([#1876](https://github.com/metalama/Metalama/issues/1876), [#1913](https://github.com/metalama/Metalama/issues/1913), [#1896](https://github.com/metalama/Metalama/issues/1896)). Projects targeting .NET 8 or .NET 9 resolve the `netstandard2.0` assets. `Metalama.Patterns.Wpf` has no `netstandard2.0` asset, so WPF applications that use it must target .NET 10 or later.
- **xunit.v3 in the testing frameworks** ([#2075](https://github.com/metalama/Metalama/issues/2075)): `Metalama.Testing.UnitTesting` and `Metalama.Testing.AspectTesting` now depend on xunit.v3 instead of xunit 2.x, so your test projects must move to xunit.v3. `TestContext` and `TestContextOptions` are renamed to <xref:Metalama.Testing.UnitTesting.MetalamaTestContext> and <xref:Metalama.Testing.UnitTesting.MetalamaTestContextOptions>, to avoid a conflict with `Xunit.TestContext`. The `ITestOutputHelper` accepted by the constructor of `UnitTestClass` is now `Xunit.ITestOutputHelper`. See <xref:aspect-testing>.
- **`metalama kill`** is now another name for `metalama shutdown` ([#2088](https://github.com/metalama/Metalama/issues/2088)). It asks the processes to exit instead of ending them immediately, and returns exit code 1 when a process is still running after the timeout. Use `metalama kill --force` to get the previous behavior.
- **Redis caching: `ReadCommandFlags` is nullable** ([#2371](https://github.com/metalama/Metalama/issues/2371)): the type of the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.ReadCommandFlags> property is now `CommandFlags?`. Its default value is `null`, which means `PreferMaster` when an in-memory L1 cache is added in front of Redis, and `PreferReplica` otherwise. In earlier versions, the default value was `PreferReplica` in both cases, and an L1 cache could keep an item that a replica returned just after its removal. Set the property to `PreferReplica` to restore the earlier behavior. See <xref:caching-redis#reading-from-redis-replicas>.

- **Caching: dependency invalidation** ([#2070](https://github.com/metalama/Metalama/issues/2070), [#2368](https://github.com/metalama/Metalama/issues/2368), [#2369](https://github.com/metalama/Metalama/issues/2369), [#2372](https://github.com/metalama/Metalama/issues/2372), [#2374](https://github.com/metalama/Metalama/issues/2374)): the in-memory and Redis backends now remove the items that depend on a removed, expired, or replaced item, and invalidating a cached method also removes the cached values of the methods that called it. `MemoryCachingBackend.Clear` no longer clears a shared `IMemoryCache` that the backend doesn't own, the backend no longer disposes such a cache, and `ItemRemoved` is raised only once per item. Review any code that relies on dependent items surviving an invalidation.
- **Caching: layered backend** ([#2373](https://github.com/metalama/Metalama/issues/2373)): the dependencies of an item are read from the remote layer only when the caller requests them.
- **Interceptors** ([#2250](https://github.com/metalama/Metalama/issues/2250)): registrations made outside `ForEachSite` have their own interfaces, and `RedirectToSynthesizedMethod` takes a <xref:Metalama.Framework.Aspects.DurableTemplateInvocation>.

> [!div class="see-also"]
> <xref:release-notes>
> <xref:requirements>
> <xref:interceptors>
> <xref:introducing-types>
> <xref:aspect-testing>
> <xref:errors>
> <xref:compile-time-rules>
> <xref:mcp-server>
> <xref:ai-agents>
