---
uid: generic-interceptor-templates
level: 300
summary: "Write generic T# templates for interceptors: reuse one template for methods of the same shape with different parameter types, and intercept generic methods with specialized or shared interceptors."
keywords: "interceptors, generic templates, methods of the same shape, generic methods, type arguments, run-time type parameter, compile-time type parameter, specialized interceptor, shared interceptor, SetTypeArgument, AddTypeParameter, boxing"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Writing generic interceptor templates

A template can declare type parameters, like any generic method. Generic templates have two main uses:

* Reusing one template for methods of the same shape, whose parameters have different types, such as `SaveOrder( Order order )` and `SaveCustomer( Customer customer )`.
* Intercepting generic methods, such as `Serialize<T>( T value )`, whose call sites have different type arguments.

This article assumes that you know how to generate interceptors from templates. See <xref:generating-interceptors-with-templates>.

## Reusing a template for methods of the same shape

When the template declares a parameter, its type must be compatible with the type of the parameter of each intercepted method. To write one strongly typed template for methods whose parameters have different types, declare a compile-time type parameter. Use it as the type of the template parameter. Give the type of the parameter of each intercepted method in `ForEachSite`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericSameShape.Aspect.cs marker="WithArgs"]

The template uses the type parameter as an ordinary type:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericSameShape.Aspect.cs marker="Template"]

One interceptor is generated for each intercepted method. In each of them, the template parameter takes the name of the parameter of the intercepted method, and `nameof(entity)` is written with this name.

A run-time type parameter cannot be used for this purpose, because Metalama binds run-time type parameters only to the type parameters of the intercepted method.

### Example: Checking the arguments of several methods

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericSameShape.cs name="Methods of the same shape"]

## Intercepting generic methods

When the intercepted method is generic, each call site has its own type arguments, for instance `Serialize<int>` and `Serialize<string>`. You choose how the generated interceptor handles them:

| Goal | Approach |
|---|---|
| Generate code that depends on the type argument, with the type known when the template is expanded. | A template without type parameters, or with a compile-time type parameter. One interceptor is generated for each type argument. |
| Generate one interceptor for all the type arguments. | A template with a run-time type parameter. The interceptor is generic. |

### Specializing the interceptor for each type argument

By default, when the template has no type parameter, Metalama generates one interceptor for each combination of type arguments. The call sites with the same type arguments share it. In the template, `meta.Interception.Method.InterceptedMethod` is constructed with the type arguments of the call sites, so the template can read them at compile time.

For instance, the following template reads the type argument of the call sites:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericSpecialized.Aspect.cs marker="TypeArgument"]

#### Example: One interceptor per type argument

In the following example, the two calls with an `int` share one interceptor, and the call with a `string` gets another one.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericSpecialized.cs name="One interceptor per type argument"]

### Using the type argument in compile-time code

To use the type argument as a type in the template, declare a compile-time type parameter, marked with `[CompileTime]`, and give its value with `WithArgs`. Because the type argument differs for each call site, give it in <xref:Metalama.Extensions.Interceptors.IMethodSelection.ForEachSite*>, from <xref:Metalama.Extensions.Interceptors.IMethodInterceptionContext.InterceptedMethod>. The template can then generate different code for each type, for instance a `null` check only for reference types.

For instance, the following registration gives the type argument of each call site:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericCompileTimeTypeArgument.Aspect.cs marker="WithArgs"]

The template generates a `null` check only for reference types:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericCompileTimeTypeArgument.Aspect.cs marker="Template"]

#### Example: Different code for value types and reference types

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericCompileTimeTypeArgument.cs name="Compile-time type argument"]

### Sharing one generic interceptor

When the template declares a run-time type parameter, Metalama binds it to the type parameter of the intercepted method, and the generated interceptor is generic. All the call sites share it, whatever their type arguments. The template uses the type parameter as an ordinary type, for instance in `typeof(T)`.

For instance, the following template has a run-time type parameter `T`, which binds to the type parameter of `Serializer.Serialize<T>`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericTemplateShared.Aspect.cs marker="Template"]

#### Example: One generic interceptor

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericTemplateShared.cs name="One generic interceptor"]

### Type parameters of the calling type

When a call site uses a type parameter of the calling type, for instance a call to `List<T>.Add` in a class `Repository<T>`, the generated class cannot name `T`. Metalama then adds `T` as a type parameter of the generated interceptor, and the call site passes it.

#### Example: A type parameter of the calling type

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericLiftedTypeParameter.cs name="A type parameter of the calling type"]

## Adding a type parameter

When the intercepted method is not generic, but its parameter has a general type such as `object`, make the interceptor generic yourself:

1. In a <xref:Metalama.Extensions.Interceptors.ITemplateInterception.Configure*> delegate, add a type parameter with `AddTypeParameter`.
2. Use it as the type of the parameter.
3. Give its type argument for each call site with <xref:Metalama.Extensions.Interceptors.IInterceptorMethodBuilder.SetTypeArgument*>, for instance the type of the argument.

For instance:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericAddedTypeParameter.Aspect.cs marker="Configure"]

The call sites then share one generic interceptor, and the arguments of value types are no longer boxed at the call site. The template reads the added type parameter through `meta.Target.Method.TypeParameters`. The type arguments must satisfy the constraints of the type parameter.

### Example: Avoiding boxing at the call site

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/GenericAddedTypeParameter.cs name="Adding a type parameter" diff-files="\.Program\.cs$"]

## Rules for type parameters

* The type parameters that Metalama derives from the intercepted method or from the calling type cannot be renamed, but a `Configure` delegate can add constraints to them.
* The type parameters added by a `Configure` delegate come after the derived ones, and each of them must receive a type argument at every call site.
* The type arguments of added type parameters are not part of the comparison that decides which call sites share an interceptor. See <xref:interceptor-deduplication-rules>.

> [!div class="see-also"]
> <xref:generating-interceptors-with-templates>
> <xref:interceptor-binding-rules>
> <xref:interceptor-deduplication-rules>
