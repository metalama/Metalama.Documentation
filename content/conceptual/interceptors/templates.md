---
uid: generating-interceptors-with-templates
level: 300
summary: "Generate interceptors from T# templates to apply the same logic to calls to many methods with different signatures: reading the call with meta.Interception, async methods, template arguments, and choosing where the interceptors are generated."
keywords: "interceptors, T# template, RedirectToSynthesizedMethod, meta.Proceed, meta.Interception, MethodTemplateSelector, async interceptor, PlaceInCallingType, PlaceInStaticClass, Configure, granularity, tracing, retry"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Generating interceptors with T# templates

When you intercept many methods with different signatures, writing one interceptor per signature is not practical. Instead, write a T# template once, and let Metalama generate an interceptor for each signature.

This article assumes that you know how to write T# templates. See <xref:templates>.

## Writing an interceptor template

To generate interceptors from a template:

1. Write a method with the `[Template]` attribute. Its return type is `dynamic?`, so that the template can implement methods of any return type.
2. In the template, call `meta.Proceed()` to call the intercepted method with the arguments of the call site.
3. In the description of the interception, call <xref:Metalama.Extensions.Interceptors.IMethodResultFactory.RedirectToSynthesizedMethod*> with the name of the template.

Interceptor templates look like the templates of override aspects, with two differences:

* `meta.Target.Method` is the generated interceptor, not the intercepted method.
* `meta.Proceed()` calls the intercepted method, as the call site did.

For instance, the following template measures the duration of the intercepted call:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TimeSdkCalls.Fabric.cs marker="Template"]

By default, the template is a member of the aspect that registers the interception. In a fabric, declare the templates in a separate class that implements <xref:Metalama.Framework.Aspects.ITemplateProvider>, and give an instance of it with <xref:Metalama.Extensions.Interceptors.ITemplateInterception.WithTemplateProvider*>, as the following examples do.

For instance, the following fabric gives its templates with a separate class:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TraceCalls.Fabric.cs marker="Registration"]

## Accessing the interception context

The `meta.Interception` property describes the interception that the template implements. Its <xref:Metalama.Extensions.Interceptors.IInterceptionInfo.Method> property gives:

* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionInfo.InterceptedMethod>: the method that the call site calls.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionInfo.ReceiverParameter>: the parameter of the generated method that receives the receiver of the call, or `null` when the intercepted method is static.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionInfo.ArgumentParameters>: the parameters of the generated method that receive the arguments of the call.
* <xref:Metalama.Extensions.Interceptors.IMethodInterceptionInfo.AddedParameters>: the parameters that you add, described below.

`meta.Interception` is a C# 14 extension property. In a project that uses an earlier version of C#, use `meta.GetExtension<IInterceptionInfo>()`.

For instance, the following template code lists the names and values of the arguments, and the name of the intercepted method:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TraceCalls.Fabric.cs marker="Arguments"]

### Reading the calling code

By default, a generated interceptor is shared by all the call sites that need the same code, even when they are in different types. The template therefore cannot read information about one call site, such as the calling member. To read it, declare which information the template depends on with <xref:Metalama.Extensions.Interceptors.ITemplateInterception.WithGranularity*>:

* With `InterceptorGranularity.Type`, the template can read `meta.Interception.CallingType`.
* With `InterceptorGranularity.Member`, it can also read `meta.Interception.CallingMember`.
* With `InterceptorGranularity.CallSite`, it can also read `meta.Interception.Caller` and `meta.Interception.Source`.

A finer granularity generates more methods. To pass information about the call site without generating more methods, pass it as an added parameter instead. See <xref:interceptor-deduplication-rules>.

For instance, the following registration sets the `Member` granularity:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/DedupPerMember.Fabric.cs marker="Granularity"]

so that its template can read the calling member:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/DedupPerMember.Fabric.cs marker="CallingMember"]

### Example: Timing every call to an SDK

The following project fabric measures the duration of every call to the methods of `PaymentGateway`, a class of another project. <xref:Metalama.Extensions.Interceptors.ITypeSelection.AllMethods*> selects all the methods that the type declares. A <xref:Metalama.Framework.Advising.MethodTemplateSelector> gives a second template for async methods, so that the duration covers the whole asynchronous operation.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/TimeSdkCalls.cs name="Timing SDK calls"]

The interceptors are generated in an internal static class named `MetalamaInterceptors`, which appears in its own tab of the example.

### Example: Tracing calls to investigate a problem

The following fabric traces every call to `PricingClient`: the calling member, the arguments, the result, and the exception. It is a typical way to investigate a problem without a debugger, for instance in a CI build or when an AI agent investigates. To remove the tracing, delete the file. To enable it only in some builds, see <xref:interceptors-selecting-calls>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/TraceCalls.cs name="Tracing calls" diff-files="\.Program\.cs$"]

## Changing arguments, results and exceptions

The template controls the call:

* Assign a parameter before `meta.Proceed()` to change the argument that the intercepted method receives.
* Store the value of `meta.Proceed()`, process it, and return another value.
* Surround `meta.Proceed()` with `try`, `catch` and `finally`, or with a loop.

For instance, the following template retries the call when it times out:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/RetryShippingCalls.Fabric.cs marker="Retry"]

### Async and iterator methods

A template whose return type is `dynamic?` handles async methods as ordinary methods: the generated interceptor returns the task without awaiting it. To run code after the asynchronous operation completes, give a second template with a <xref:Metalama.Framework.Advising.MethodTemplateSelector>, and set `useAsyncTemplateForAnyAwaitable` to `true`. In this template, declared as `async Task<dynamic?>`, call `meta.ProceedAsync()`. The selector works as for override aspects. See <xref:overriding-methods>.

For instance, the following registration gives a template for ordinary methods and a template for async methods:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TimeSdkCalls.Fabric.cs marker="Selector"]

The async template awaits `meta.ProceedAsync()`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TimeSdkCalls.Fabric.cs marker="AsyncTemplate"]

### Passing arguments to the template

<xref:Metalama.Extensions.Interceptors.ITemplateInterception.WithArgs*> gives the arguments of the template, as the properties of an anonymous object:

* A property whose name is a compile-time parameter of the template gives its value. The value is known when the template is expanded.
* A property whose name is a run-time parameter of the template adds a parameter of that name to the generated method. Each call site passes the value.

For instance, the following registration gives the value of the compile-time parameter `maxAttempts` of the retry template:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/RetryShippingCalls.Fabric.cs marker="Registration"]

In the following template, `category` and `caller` are run-time parameters, so they become parameters of the generated method:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TemplateAddedParameter.Aspect.cs marker="Signature"]

<xref:Metalama.Extensions.Interceptors.ITemplateInterception.WithTags*> gives an object that the template reads through `meta.Tags`. For the complete rules, see <xref:interceptor-binding-rules>.

### Example: Retrying calls to a third-party service

The following fabric retries the calls to a third-party shipping client when they time out. `Where` selects only the methods whose name starts with `Get`, because retrying an operation that is not idempotent, such as creating a shipment, could perform it twice. The maximum number of attempts is a compile-time parameter of the template.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/RetryShippingCalls.cs name="Retrying calls" diff-files="\.Program\.cs$"]

## Choosing where the interceptor is generated

By default, the interceptors are internal static methods of the class `MetalamaInterceptors` in the global namespace. You can choose another location:

* <xref:Metalama.Extensions.Interceptors.ITemplateInterception.PlaceInStaticClass*> generates the interceptor in a static class of the given full name.
* <xref:Metalama.Extensions.Interceptors.ITemplateInterception.PlaceInCallingType*> generates the interceptor as a private method of the type that contains the call site. The interceptor can then access the private members of that type. The type does not need to be `partial`.
* <xref:Metalama.Extensions.Interceptors.ITemplateInterception.PlaceInType*> generates the interceptor in a given type of the current project, for instance a base class of the calling types.

A call site in top-level statements has no calling type. Use <xref:Metalama.Extensions.Interceptors.IInterceptionContext.CanPlaceInCallingType> in <xref:Metalama.Extensions.Interceptors.IMethodSelection.ForEachSite*> to choose another location for such a call site.

For instance, the following registration generates the interceptor as an instance method of the calling type:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/CallerLogger.Aspect.cs marker="Placement"]

The template can then access the private fields of the caller through `meta.This`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/CallerLogger.Aspect.cs marker="This"]

### Shaping the generated method

<xref:Metalama.Extensions.Interceptors.ITemplateInterception.Configure*> gives a delegate that receives an <xref:Metalama.Extensions.Interceptors.IInterceptorMethodBuilder>. The delegate can change the name, the accessibility and the `IsStatic` property of the generated method, widen the types of its parameters, and add parameters, whose value it gives with <xref:Metalama.Extensions.Interceptors.IInterceptorMethodBuilder.SetArgument*>. The reference kinds, the positions and the return type cannot change.

For instance, the following registration gives a template argument, and adds a parameter whose value is the name of the calling member:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TemplateAddedParameter.Aspect.cs marker="Configure"]

### Example: Writing to the caller's logger

The following aspect generates the interceptor as an instance method of the calling type, so that the template can write to the private `_log` field of the caller through `meta.This`. Generated interceptors are static by default, so the `Configure` delegate sets `IsStatic` to `false`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/CallerLogger.cs name="Writing to the caller's logger"]

## Intercepting calls to protected methods of a base class

A class that derives from a framework class, such as a Windows Forms or WPF control, often calls the protected methods that it inherits, for instance `this.UpdateStyles()`. Two rules apply:

* The calls are bound to the method of the framework class, so the registration must select the framework class, not the derived class. To limit the interception to the derived classes, use a query that selects them as the scope.
* A protected method can be called only from the class hierarchy. The interceptor cannot be in the default `MetalamaInterceptors` class: generate it in a common base class of your project with <xref:Metalama.Extensions.Interceptors.ITemplateInterception.PlaceInType*>. Because C# allows the call of a protected method only through an instance of the calling class, change the type of the receiver parameter to this base class with a `Configure` delegate.

For instance:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/BaseClassCalls.Fabric.cs marker="Selection"]

### Example: Intercepting calls to a protected method of a framework class

In the following example, `OrderGrid` and `CustomerGrid` derive from `AppControl`, the base class of the controls of the project, which derives from the framework class `Control`. Their calls to the protected method `Control.UpdateStyles` are redirected to one interceptor, generated as a `private protected` method of `AppControl`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/BaseClassCalls.cs name="Intercepting calls to a protected method"]

## Limitations

In this version, interceptors generated from templates intercept only calls. A method group converted to a delegate, such as `items.Select( Normalize )`, can be intercepted only by an existing method. See <xref:interceptors-selecting-calls>.

## In this section

| Article | Description |
|---------|-------------|
| <xref:generic-interceptor-templates> | Reuse one template for methods of the same shape, and intercept generic methods with specialized or shared interceptors. |
| <xref:interceptor-deduplication-rules> | Reference: when call sites share a generated interceptor, and how granularity, names and accessibility are decided. |

> [!div class="see-also"]
> <xref:templates>
> <xref:interceptor-binding-rules>
> <xref:interceptor-deduplication-rules>
