---
uid: interceptors-selecting-calls
level: 300
summary: "Choose which calls Metalama intercepts: the intercepted methods, the calling code, the build configurations, and a decision for each call site, including method groups, event handlers and the projects that use your library."
keywords: "interceptors, scope, fabric, namespace fabric, ProjectFabric, TransitiveProjectFabric, Types, AllMethods, Where, IncludingInterfaceImplementations, ExcludingOverrides, ExcludeInterceptors, ForEachSite, Skip, MethodUseKind, method group, event handler, PreprocessorSymbols"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Choosing which calls to intercept

An interception registration selects calls in two dimensions:

* The _scope_ is the code that contains the call sites: a type, a namespace, or the whole project.
* The _method selection_ is the set of methods whose calls are intercepted.

This article describes both dimensions, and how to decide the interception for each call site.

## Choosing the calling code

### From an aspect

When an aspect calls <xref:Metalama.Extensions.Interceptors.InterceptionAdviserExtensions.InterceptMethods*> on its builder, the scope is the target of the aspect. You can also register interceptors for a child declaration with `builder.With( declaration )`, or for several declarations with a query on `builder.Outbound`. In all cases, the scope must be contained in the target of the aspect.

An interceptor registration is recorded during <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*>, and the calls are rewritten after all aspects have run. When the aspect reports an error or is skipped, its interceptors are discarded.

For instance, the following aspect intercepts the calls to `HttpClient.GetStringAsync` in the type to which it is applied:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/LogHttpCalls.Aspect.cs marker="Registration"]

### From a fabric

A fabric calls `InterceptMethods` on its amender or on a query:

* In a <xref:Metalama.Framework.Fabrics.ProjectFabric>, `amender.InterceptMethods( ... )` intercepts the calls in the whole project.
* In a <xref:Metalama.Framework.Fabrics.NamespaceFabric>, the scope is the namespace.
* In a <xref:Metalama.Framework.Fabrics.TypeFabric>, the scope is the type.
* A query such as `amender.SelectTypes().Where( ... ).InterceptMethods( ... )` makes each selected declaration a scope.

For instance, the following project fabric intercepts the calls to all the methods of `PricingClient` in the whole project:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TraceCalls.Fabric.cs marker="Registration"]

With a tagged query, the delegate given to `ForEachSite`, described below, can read the tag of the scope through <xref:Metalama.Extensions.Interceptors.IInterceptionContext.ScopeTag>.

### Example: Switching one module at a time

The following namespace fabric is in the `Billing` namespace. It redirects the calls that this namespace makes to the legacy tax calculator to the new one. The `Reporting` namespace still uses the legacy calculator. To migrate another module, add the same fabric to its namespace. When all the modules are migrated, change the call sites in the source code and delete the fabrics.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/SwitchPerModule.cs name="Switching one module at a time"]

### Nested types and lambdas

* By default, the scope of a type does not include its nested types. Call <xref:Metalama.Extensions.Interceptors.IMethodInterceptionBuilder.IncludingNestedTypes*> to include them.
* By default, the calls in lambdas, anonymous methods and local functions are included. Call <xref:Metalama.Extensions.Interceptors.IMethodInterceptionBuilder.ExcludingLambdas*> to exclude them.

### Excluding code

The calls in a declaration that has the <xref:Metalama.Extensions.Interceptors.ExcludeInterceptorsAttribute> attribute are never intercepted, whatever the registration. The attribute applies to the members and nested types of a type, and to the lambdas and local functions of a member. Use `[ExcludeInterceptors( false )]` to include a declaration again inside an excluded type.

In a fabric, the <xref:Metalama.Extensions.Interceptors.InterceptorConfigurationExtensions.ExcludeInterceptors*> extension methods have the same effect on the declarations that a query selects, for instance `amender.SelectTypes().Where( t => t.Name.EndsWith( "Migration" ) ).ExcludeInterceptors()`.

The exclusion is not inherited by derived types or overriding members.

For instance, the following interceptor calls the method that it intercepts, so its class is excluded from interception:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/AuditFileDelete.Audit.cs marker="Exclude"]

## Choosing the intercepted methods

The description of the interception selects a type, then methods:

* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionBuilder.Type*> selects one type, given as a `Type` or as an <xref:Metalama.Framework.Code.INamedType>. A generic type definition, such as `typeof(List<>)`, selects the methods of all its constructions.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionBuilder.Types*> selects several types with a predicate, for instance all the types of a namespace. The methods must then be given by name, because Metalama finds the call sites by the name of the called method.
* <xref:Metalama.Extensions.Interceptors.ITypeFilterSelection.Methods*> selects the methods of the given names, with all their overloads.
* <xref:Metalama.Extensions.Interceptors.ITypeSelection.AllMethods*> selects all the ordinary methods that the type declares. It does not select inherited methods, operators, accessors or constructors.
* <xref:Metalama.Extensions.Interceptors.IMethodSelection.Where*> keeps only the methods that a predicate accepts, for instance one overload.

A call bound to an override of a selected method is also intercepted. Call <xref:Metalama.Extensions.Interceptors.IMethodSelection.ExcludingOverrides*> to intercept only the calls bound to the selected method itself. A call bound to an implementation of a selected interface method is intercepted only after <xref:Metalama.Extensions.Interceptors.IMethodSelection.IncludingInterfaceImplementations*>.

For instance, the following registration intercepts the calls to `IOrderRepository.Save`, including the calls bound to its implementation `SqlOrderRepository.Save`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/InterfaceCalls.Aspect.cs marker="Selection"]

The complete example:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/InterfaceCalls.cs name="Calls through an interface"]

The predicates and delegates of the description are stored with the registration and can run later, concurrently, and several times. They must not capture a declaration or a compilation.

## Intercepting calls only in some builds

A fabric can register interceptors only when a condition on the project is true. The <xref:Metalama.Framework.Project.IProject> object, available as `amender.Project`, gives:

* <xref:Metalama.Framework.Project.IProject.PreprocessorSymbols>: the symbols defined by `DefineConstants`.
* <xref:Metalama.Framework.Project.IProject.Configuration>: the build configuration, such as `Debug` or `Release`.
* <xref:Metalama.Framework.Project.IProject.TryGetProperty*>: the value of an MSBuild property, which must be declared as a `CompilerVisibleProperty` item. See <xref:reading-msbuild-properties>.

### Example: Tracing only in some builds

The following fabric traces the calls to `File.ReadAllText` only when the `TRACE_CALLS` symbol is defined. For instance, add `<DefineConstants>$(DefineConstants);TRACE_CALLS</DefineConstants>` to a build configuration that is never released.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/TraceBuildOnly.cs name="Tracing only in some builds"]

## Deciding for each call site

<xref:Metalama.Extensions.Interceptors.IMethodSelection.ForEachSite*> gives a delegate that decides the interception of each call site. The delegate receives an <xref:Metalama.Extensions.Interceptors.IMethodInterceptionSite> and returns either `site.Skip()`, which leaves the call site unchanged, or a result that the site creates, such as `site.RedirectToSynthesizedMethod( ... )`.

The site describes the call:

* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.UseKind>: a call, a method group converted to a delegate or to a function pointer, or an event subscription.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.InterceptedMethod> and <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.MatchedMethod>: the called method, and the selected method that it matches.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.Receiver> and <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.Arguments>: the receiver and the arguments written at the call site. Their expressions can be inspected, for instance to read a constant value, but they cannot be copied into generated code.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.IsResultUsed>: whether the calling code uses the return value.
* <xref:Metalama.Extensions.Interceptors.IInterceptionContext.Caller>: the calling member, its parameters, and whether the call is in a lambda, in an async function, or where `this` is available.
* <xref:Metalama.Extensions.Interceptors.IInterceptionContext.NonInterceptableReason>: the reason why the call cannot be intercepted in this version, if any. Skip such call sites to avoid a warning.

The delegate can report diagnostics through <xref:Metalama.Extensions.Interceptors.IInterceptionContext.Diagnostics>. An exception thrown by the delegate is reported as an error, and the call site is left unchanged.

### Example: Skipping the calls from test helpers

The following aspect logs the calls to `PaymentGateway`, except the calls from the types that have the `[TestHelper]` attribute.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/SkipTestHelpers.cs name="Skipping calls from test helpers"]

## Handling calls that cannot be intercepted

Some call sites match a registration but cannot be intercepted in this version, for instance a call to a method that returns by reference, or a call in a lambda converted to an expression tree. For these call sites, you have two choices:

* Let Metalama report them. When the registration returns an interceptor for such a call site, Metalama reports the warning LAMA1012, which gives the reason, and leaves the call unchanged. This is the default behavior.
* Skip them silently. In `ForEachSite`, return `site.Skip()` when <xref:Metalama.Extensions.Interceptors.IInterceptionContext.NonInterceptableReason> is not `None`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/SkipNonInterceptable.Aspect.cs marker="Skip"]

### Example: Skipping calls that cannot be intercepted

In the following example, the call to a method that returns by reference, and the call in an `IQueryable` lambda, are skipped without a warning. The call in the `IEnumerable` lambda is intercepted. For the list of reasons, see <xref:interceptors-troubleshooting>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/SkipNonInterceptable.cs name="Skipping calls that cannot be intercepted"]

## Intercepting delegates and event handlers

A registration intercepts all the uses of the selected methods, not only calls. A method group converted to a delegate, such as `items.Select( Normalize )`, or added to an event, such as `button.Click += OnClick`, is also a use. The <xref:Metalama.Extensions.Interceptors.MethodUseKind> enumeration lists the kinds of use.

The kinds of use have these constraints in this version:

* A method group can be intercepted only by an existing method with exactly the same signature as the intercepted method.
* Interceptors generated from templates support only calls. Skip the other kinds of use, otherwise an error is reported.
* Intercepting a method group changes the identity of the delegate. When you intercept an event subscription, intercept the matching unsubscription in the same way, otherwise `-=` does not remove the handler.

### Example: Intercepting a method group

The following aspect redirects the calls to `TextUtil.Normalize`, including the method group passed to `Select`, to an existing method with the same signature.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/MethodGroups.cs name="Intercepting a method group"]

### Example: Intercepting only calls

The following aspect generates its interceptor from a template, which supports only calls, so it skips the other kinds of use. The method group passed to `Select` is left unchanged.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/CallsOnly.cs name="Intercepting only calls"]

## Intercepting calls in projects that use your library

A <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> in a library executes in every project that references the library. When it registers interceptors, they intercept the calls in the referencing projects. This suits a shared package that applies company-wide policies. See <xref:fabrics-many-projects>.

The referencing projects must build with Metalama, and each of them requires a license.

### Example: A company-wide policy

The following example shows a shared package whose transitive fabric times every call to `HttpClient.GetStringAsync` in the projects that reference it.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/CompanyPolicy.cs name="Company-wide policy"]

## Conflicts between registrations

At most one interceptor applies to a call site. When several registrations select the same call site, each of them is evaluated, and it is an error when more than one returns a result other than `Skip()`. To resolve the conflict, use `ForEachSite` in one of the registrations to skip the call sites that the other one handles.

> [!div class="see-also"]
> <xref:fabrics>
> <xref:fabrics-many-projects>
> <xref:interceptors-troubleshooting>
