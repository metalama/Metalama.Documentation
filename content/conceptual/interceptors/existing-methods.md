---
uid: redirecting-calls-to-existing-methods
level: 200
summary: "Redirect the calls to a method of any assembly to a static method that you write, from an aspect or a fabric, and pass additional values such as the caller's name or the calling object."
keywords: "interceptors, RedirectToExistingMethod, InterceptMethods, existing method, static interceptor, WithArgs, caller information, ExcludeInterceptors, fabric, test seam, audit"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Redirecting calls to existing methods

The simplest interceptor is an ordinary static method that you write. Metalama rewrites the matching call sites so that they call your method instead of the original one. Your method is plain C#: you can debug it and unit-test it like any other code.

Use an existing method when you intercept a few methods, when the interceptor contains logic that you want to test on its own, or when you route calls to another implementation. When you need the same logic for many methods with different signatures, generate the interceptors from a template instead. See <xref:generating-interceptors-with-templates>.

## Redirecting calls from an aspect

To intercept the calls made by the members of one type, register the interceptor from an aspect:

1. Add a reference to the `Metalama.Extensions.Interceptors` package to the project that contains the aspect.
2. Write a static method whose parameters match the intercepted method. When the intercepted method is an instance method, the first parameter receives the object on which the method is called, called the _receiver_.
3. In the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of the aspect, call the <xref:Metalama.Extensions.Interceptors.InterceptionAdviserExtensions.InterceptMethods*> extension method. Its argument is a delegate that describes the interception:
   * <xref:Metalama.Extensions.Interceptors.IMethodInterceptionBuilder.Type*> selects the type that declares the intercepted methods.
   * <xref:Metalama.Extensions.Interceptors.ITypeFilterSelection.Methods*> selects the methods by name. A name selects every overload of that name, and <xref:Metalama.Extensions.Interceptors.IMethodSelection.Where*> keeps only some of them.
   * <xref:Metalama.Extensions.Interceptors.IMethodResultFactory.RedirectToExistingMethod*> gives your method.

The scope of the registration is the target of the aspect: only the calls written in the members of this type are intercepted.

### Example: Logging calls to HttpClient

The following aspect logs the calls that `OrderService` makes to `HttpClient.GetStringAsync( string )`. The `Where` predicate keeps the overload that takes a `string`, because the interceptor has a `string` parameter.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/LogHttpCalls.cs name="Logging calls to HttpClient"]

## Redirecting calls from a project or namespace fabric

To intercept the calls made by many types, register the interceptor from a fabric, and call `InterceptMethods` on the amender:

* In a <xref:Metalama.Framework.Fabrics.ProjectFabric>, the scope is the whole project.
* In a <xref:Metalama.Framework.Fabrics.NamespaceFabric>, the scope is the namespace of the fabric.

To select other scopes, such as the types that a query selects, see <xref:interceptors-selecting-calls>.

### Example: Replacing Thread.Sleep in tests

Code that calls `Thread.Sleep` directly makes tests slow. The following project fabric redirects the calls to `Thread.Sleep( int )` to a virtual clock, which records the time instead of waiting. The program waits 6 seconds in virtual time, and completes immediately.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/FakeSleep.cs name="Replacing Thread.Sleep" diff-files="\.Program\.cs$"]

In a real project, register such an interceptor only in a test build, as described in <xref:interceptors-selecting-calls>. The tests then run a binary that differs from the release build, so keep the difference small, and introduce a real abstraction, such as `TimeProvider`, once the tests are in place.

### Example: Intercepting calls in one namespace

The following namespace fabric is in the `Billing` namespace. It redirects the calls to `File.ReadAllText` in this namespace only. The same call in the `Reporting` namespace is not intercepted.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/NamespaceScope.cs name="Intercepting calls in one namespace"]

### Avoiding infinite recursion

When your method calls the intercepted method, and your method is in the scope of the registration, the call in your method is intercepted too, and your method calls itself.

To prevent this, add the <xref:Metalama.Extensions.Interceptors.ExcludeInterceptorsAttribute> attribute to the class of your interceptor. The calls in a class with this attribute are never intercepted. Some of the following examples use this attribute. In the previous example, the interceptor is outside the scope of the namespace fabric, so it does not need the attribute.

## Matching the parameters of the intercepted method

Metalama binds each parameter of your method to a value of the call site:

* When the intercepted method has a receiver, the first parameter of your method receives it.
* The other parameters are matched with the parameters of the intercepted method _by name_, not by position.
* A parameter of your method that has no match can receive a value given by `WithArgs`, the caller information requested by an attribute such as `[CallerMemberName]`, or its own default value.
* An argument of the call that your method does not receive is dropped. It is still evaluated when its evaluation can have a side effect.

The types do not have to be identical: each argument must convert implicitly to the type of your parameter, and the return value of your method must convert implicitly to the return type of the intercepted method. Parameters passed by `ref`, `out` or `in` must keep their reference kind and their type.

These rules have many details, for instance for omitted optional arguments, `params` arrays, generic methods and struct receivers. See <xref:interceptor-binding-rules>.

## Passing additional parameter values

Your method can receive values that the intercepted method does not have.

### Passing a constant value

<xref:Metalama.Extensions.Interceptors.IExistingMethodInterception.WithArgs*> gives values by name. Pass an anonymous object, another object whose public properties and fields give the values, or an `IReadOnlyDictionary<string, object?>`. A value can be a literal, an enumeration value, a `Type`, which is written as `typeof`, or a <xref:Metalama.Framework.Code.TypedConstant>. See <xref:interceptor-binding-rules>.

### Passing the location of the call

A parameter with a caller information attribute, such as `[CallerMemberName]`, `[CallerFilePath]` or `[CallerLineNumber]`, receives the location of the intercepted call site, not of the call inside your method.

#### Example: Auditing calls to File.Delete

The following project fabric redirects every call to `File.Delete` to an audit method. `WithArgs` passes a constant category, and the caller information attributes give the name of the calling member and the line of the call.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/AuditFileDelete.cs name="Auditing calls to File.Delete" diff-files="\.Program\.cs$"]

#### Example: Counting the calls to a legacy API

Before replacing a legacy API, you may want to know which of its call sites actually run in production. The following interceptor counts the calls for each calling member and line, and then calls the legacy implementation.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/CountCalls.cs name="Counting calls to a legacy API" diff-files="\.Program\.cs$"]

### Passing a parameter of the calling method

To pass a value that depends on the call site, decide the interception for each call site with <xref:Metalama.Extensions.Interceptors.IMethodSelection.ForEachSite*>. The delegate receives the call site, which describes the calling code through its <xref:Metalama.Extensions.Interceptors.IInterceptionContext.Caller> property:

* <xref:Metalama.Extensions.Interceptors.ICaller.Parameters> gives the parameters of the calling member, which you can pass to your method.
* <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.This*> gives the calling object, when <xref:Metalama.Extensions.Interceptors.ICaller.CanAccessThis> is `true`.

The values passed this way are written at the call site.

#### Example: Passing a parameter of the calling method

The following aspect passes the `requestId` parameter of the calling method to the interceptor. It skips the call sites where the calling method has no such parameter, and the call sites in lambdas that cannot read it.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/PassCallerParameter.cs name="Passing a parameter of the calling method" diff-files="\.Program\.cs$"]

#### Example: Passing the calling object

The following fabric redirects the calls to `Telemetry.Track` to a method that also receives the calling object. In a static member, there is no calling object, so `null` is passed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/PassCallerInstance.cs name="Passing the calling object" diff-files="\.Program\.cs$"]

## Calling the original method

Your method can call the method that it intercepts, before or after its own logic, or not at all. It can also change the arguments, process the result, or handle exceptions.

### Example: Comparing a new implementation with the legacy one

The following interceptor runs a new tax calculator next to the legacy one, reports the differences, and returns the result of the legacy calculator. The callers don't change, and the behavior of the program does not change either. When the new calculator has proven itself, you can switch the calls to it. See the example _Switching one module at a time_ in <xref:interceptors-selecting-calls>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/CompareTaxCalculators.cs name="Comparing tax calculators" diff-files="\.Program\.cs$"]

> [!div class="see-also"]
> <xref:interceptor-binding-rules>
> <xref:interceptors-selecting-calls>
> <xref:generating-interceptors-with-templates>
