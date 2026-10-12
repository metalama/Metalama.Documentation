---
uid: interceptor-deduplication-rules
level: 400
summary: "Reference of the rules that decide when call sites share a method generated from a template: what the call sites must have in common, the granularity, the representative call site, and the names and accessibility of the generated methods."
keywords: "interceptors, deduplication, shared interceptor, granularity, InterceptorGranularity, Owner, representative call site, generated method names, accessibility, MetalamaInterceptors, WithGranularity"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Interceptor deduplication rules

When interceptors are generated from a template, Metalama does not generate one method per call site. The call sites that need the same code share one generated method, and the template is expanded once for this method. This article states when call sites share a method, and the consequences for the template.

These rules apply only to interceptors generated from templates. An existing method is shared by definition.

## What call sites must have in common

Two call sites share a generated method when all of these are equal:

| Element | Details |
|---|---|
| Location | The class or type where the method is generated. |
| Kind of method | Static or instance, and how the receiver is passed: by value or by reference. |
| Requested name | The default name, or the name set by a `Configure` delegate. |
| Intercepted method | Its definition, the type arguments of the call, and how the call is dispatched. Two methods with the same signature never share an interceptor. |
| Final signature | The signature after the `Configure` delegates, including the explicit accessibility, the added parameters with their names and types, the widened types, the attributes and the constraints. |
| Template | The template member, and the selected variant: default, async or iterator. |
| Template provider | The object or type that declares the template. |
| Template arguments and tags | Compared by value. |
| Granularity | The calling type, member or call site, when the granularity includes it. See below. |
| Owner | The aspect instance or fabric that registered the interceptor, only with `InterceptorGranularity.Owner`. |

These elements do not prevent call sites from sharing a method:

* The values of the added parameters given by `SetArgument`, and the type arguments of the added type parameters. They are passed at each call site.
* The `Configure` delegate itself, when it produces the same signature.
* How the receiver is written at the call site: implicit `this`, explicit receiver, or conditional access.
* How the arguments of each call are bound.
* The aspect instance that registered the interceptor, unless the granularity includes `Owner`.

### Example: One method for several types

In the following example, the template does not read the calling code. The four call sites, in two types, share one generated method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/DedupShared.cs name="One method for several types"]

## Granularity

The granularity declares what the template depends on, and therefore which call sites can share a method. Set it with <xref:Metalama.Extensions.Interceptors.ITemplateInterception.WithGranularity*>. Metalama does not detect what the template depends on.

| Granularity | The template can read | Call sites that share a method |
|---|---|---|
| `Global` (default) | Nothing about the calling code. | All the call sites with the same elements, in any type. |
| `Type` | `meta.Interception.CallingType`. | The call sites of the same type. |
| `Member` | Also `meta.Interception.CallingMember`. | The call sites of the same member. |
| `CallSite` | Also `meta.Interception.Caller` and `meta.Interception.Source`. | None: each call site has its own method. |
| `Owner`, combined with any level | The state of the aspect instance that registered the interceptor. | The call sites of the same registration. |

The levels `Type`, `Member` and `CallSite` include the coarser ones. Reading a property that requires a finer level than the granularity throws an exception, whose message names the level to set.

For instance, the following registration declares that its template reads the calling member:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/DedupPerMember.Fabric.cs marker="Granularity"]

### Example: One method per calling member

In the following example, the template reads the calling member, so the granularity is `Member`. Each calling member gets its own method, and the two calls in `PayTwice` share one.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/DedupPerMember.cs name="One method per calling member"]

To pass information about the call site without generating more methods, pass it as an added parameter, as in the example _Added parameters_ of <xref:interceptor-binding-rules>.

For instance, the following registration adds a parameter whose value is the name of the calling member. The call sites still share one generated method:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TemplateAddedParameter.Aspect.cs marker="Configure"]

## The representative call site

The template of a shared method is expanded once, for one of its call sites, called the _representative_. The call sites are ordered by:

1. The path of the file, relative to the project directory, with `/` as separator, in ordinal order.
2. The position of the call site in the file.

The first call site is the representative. Its registration, its aspect instance and its tags are used to expand the template, and the errors reported during the expansion are located at this call site.

As a consequence, a template that reads the fields or the state of its aspect instance must use the `Owner` granularity. Otherwise, the call sites of other aspect instances may share a method that was expanded with the wrong instance. This is the same rule as for several instances of an aspect on one declaration. See <xref:same-type-multiple-instances>.

## Names of the generated methods

* The default name is formed from the name of the declaring type and of the intercepted method, for instance `PaymentGateway_Charge_Interceptor`.
* Generated methods never overload each other. When several methods of the same location have the same requested name, a numeric suffix is added, in the order of their representatives.
* The names of inherited members are avoided.
* The default class is named `MetalamaInterceptors`, in the global namespace. A suffix is added when a type of that name exists. With `PlaceInStaticClass`, the namespace of the full name is created when it does not exist.

The result is the same on every machine and operating system.

## Accessibility of the generated methods

Unless a `Configure` delegate sets the accessibility, it is computed from all the call sites of the method:

* `private`, when every call site is in the type where the method is generated, or in a type nested in it;
* `private protected`, when call sites are in types derived from it;
* `internal`, otherwise.

When the signature of the method mentions a type that is less accessible than the method, the method cannot be generated, and an error is reported. A call site in a conditional access, such as `a?.M()`, is rewritten through a forwarder method, which makes the generated method `internal`.

> [!div class="see-also"]
> <xref:generating-interceptors-with-templates>
> <xref:interceptor-binding-rules>
