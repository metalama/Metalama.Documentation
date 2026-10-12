---
uid: interceptor-binding-rules
level: 400
summary: "Reference of the rules that bind the receiver, the arguments and the type arguments of an intercepted call to the parameters and type parameters of an existing interceptor method or of a method generated from a template."
keywords: "interceptors, binding rules, parameter binding, type parameter binding, receiver, WithArgs, caller information, optional parameters, params, ref, out, in, struct receiver, generic interceptor, added parameters, SetArgument"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Interceptor binding rules

This article states how Metalama passes the values of an intercepted call site to the interceptor: the receiver, the arguments, and the type arguments. The rules differ for an existing method, which you write, and for a method generated from a template.

In this article:

* The _intercepted method_ is the method that the call site calls in the source code.
* The _interceptor_ is the method that the rewritten call site calls.
* The _receiver_ is the object on which an instance method is called.
* An _added value_ is a value that the intercepted method does not have: a value given by `WithArgs` or `SetArgument`.

## Parameters of an existing method

Each parameter of the interceptor receives the first of these values that exists. A required parameter that receives no value is an error, and the call site is not intercepted.

| Order | Value |
|---|---|
| 1 | The property of the same name of the object given to <xref:Metalama.Extensions.Interceptors.IExistingMethodInterception.WithArgs*>. |
| 2 | The receiver of the call, for the first parameter, when the call has a receiver. |
| 3 | The argument of the call whose parameter in the intercepted method has the same name. |
| 4 | The caller information that a `[CallerMemberName]`, `[CallerFilePath]` or `[CallerLineNumber]` attribute requests, for the intercepted call site. |
| 5 | The default value of the parameter. |

Consequences:

* Parameters are bound by name, not by position. When the interceptor declares its parameters in another order, the rewritten call uses named arguments.
* `WithArgs` can pass the receiver or an argument of the call to a parameter of another name, through <xref:Metalama.Extensions.Interceptors.IInvocationReceiver.Expression> or <xref:Metalama.Extensions.Interceptors.InvocationArgument.Expression>.
* An extension method called with the instance syntax, such as `text.Normalize()`, is bound in its static form: its receiver is the first argument.

For instance, the following interceptor of `Mailer.Send( string recipient, string subject, int priority = 1 )` declares its parameters in another order, and has no parameter for `priority`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingByName.Aspect.cs marker="Interceptor"]

### Example: Parameters bound by name

In the following example, the interceptor declares `subject` and `recipient` in another order than the intercepted method, and it has no parameter for `priority`. The argument for `priority` is dropped, but it is still evaluated, in the order of the source code, because it can have a side effect.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingByName.cs name="Parameters bound by name"]

## Values accepted by WithArgs

The argument of `WithArgs` gives values by name. It can be:

* an anonymous object, such as `new { category = "Sales" }`;
* any other object: its public instance properties and fields are bound by name;
* an `IReadOnlyDictionary<string, object?>`, such as a `Dictionary<string, object?>`: its entries are bound by key, which is useful when the names are computed;
* an <xref:Metalama.Framework.Aspects.IObjectReader>.

Each value can be:

* A literal: `null`, a Boolean, a character, a number, a string, or a value of an enumeration that compile-time code can use.
* A `Type`, which is written as `typeof`.
* A <xref:Metalama.Framework.Code.TypedConstant>, for instance an array, or a value of an enumeration that only run-time code can use.
* An expression of the call site: the receiver or an argument. It is evaluated once, in the order of the source code.
* Another <xref:Metalama.Framework.Code.IExpression>, written at the call site, for instance a parameter of the calling member given by <xref:Metalama.Extensions.Interceptors.ICaller.Parameters>, or the calling object given by <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.This*> when <xref:Metalama.Extensions.Interceptors.ICaller.CanAccessThis> is `true`.

A name that matches no parameter and no type parameter is an error. A value that does not convert to the type of the parameter is an error. An argument of the call can be passed to two parameters only when it is passed by value and its evaluation has no side effect.

### Example: Values given by a dictionary

The following aspect gives the values of the additional parameters of the interceptor with a dictionary: a string, a number, a type and `null`.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingWithArgs.Aspect.cs marker="WithArgs"]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingWithArgs.cs name="Values given by a dictionary"]

### Passing a parameter of the calling member

When a lambda or a local function that contains the call site declares a variable with the name of a parameter of the calling member that you pass, Metalama renames the variable and reports the warning LAMA0661. A parameter cannot be passed from a static lambda, and a parameter passed by reference cannot be passed from a lambda: check <xref:Metalama.Extensions.Interceptors.ICaller.IsInNestedFunction> and <xref:Metalama.Extensions.Interceptors.ICaller.IsInStaticFunction> first, as in the following snippet:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/PassCallerParameter.Aspect.cs marker="Guard"]

For the complete example, see _Passing a parameter of the calling method_ in <xref:redirecting-calls-to-existing-methods>.

## Evaluation of the arguments

* The arguments of the call are always evaluated in the order of the source code, even when the interceptor receives them in another order.
* An argument that the interceptor does not receive is dropped. When its evaluation can have a side effect, it is still evaluated.

## Omitted optional arguments and params arrays

When the call omits an optional argument, and the interceptor receives it, Metalama writes the default value of the intercepted method explicitly, for instance an enumeration member by its name. When the default value cannot be written in C#, the call site is not intercepted, unless the interceptor has no parameter for this argument. The optional parameters of the interceptor that have no match receive their own default value.

When the call passes the elements of a `params` collection:

* If the interceptor has a `params` parameter of the same type, the elements are kept as written.
* Otherwise, the elements are packed into a collection expression.
* A collection passed in normal form, or `null`, is passed as it is.

For instance, the following interceptor of `Log.Write( string format, Severity severity = Severity.Info, params object[] args )` has no default value and no `params` modifier:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingDefaultsAndParams.Aspect.cs marker="Interceptor"]

### Example: Default values and params arrays

In the following example, the interceptor has no default value for `severity`, and its `args` parameter is a plain array. The omitted `severity` argument is written as `Severity.Info`, and the elements of the `params` array are packed into a collection expression.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingDefaultsAndParams.cs name="Default values and params arrays"]

## Conversions and return values

* Each argument must convert implicitly to the type of the parameter that receives it. When the call site converts an argument to the parameter type of the intercepted method, the argument is cast to this type before it is passed.
* A lambda argument is cast to the delegate type of the intercepted method, which keeps the meaning of the source code.
* The return value of the interceptor must convert implicitly to the return type of the intercepted method. It is cast when the calling code uses the value. In a conditional access, such as `a?.M()`, the return types must be identical.
* An interceptor that returns a value can intercept a method that returns `void`.

## Reference kinds

* A `ref`, `out` or `in` parameter must receive an argument of the call with the same reference kind and an identical type.
* Combinations that would cause a compiler warning are refused, for instance a `ref readonly` parameter for an argument passed by value.

## Struct receivers

When the intercepted method is an instance method of a struct, the interceptor must receive the receiver in a way that keeps the behavior of the original call:

For instance, `Counter.Increment()` modifies the struct, so the interceptor receives the counter by reference:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingStructReceiver.Aspect.cs marker="Interceptor"]

| Receiver | Interceptor parameter |
|---|---|
| A variable, and a method that can modify the struct | `ref`. Receiving it by value would lose the modification, so it is an error. |
| A `readonly` method, or a member of a `readonly` struct | By value or `in`. |
| A read-only variable, such as a `readonly` field or an `in` parameter, on which C# calls the method on a copy | By value. `ref` would modify the variable, so it is an error. |

### Example: A mutating struct method

In the following example, `Counter.Increment` modifies the struct, so the interceptor receives the counter with `ref`. The rewritten call passes the field by reference, and the modification is kept.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingStructReceiver.cs name="A mutating struct method"]

## Requirements on an existing method

* The method must be static and accessible from the call site.
* It must not be a `[Conditional]` method, and it must not be the intercepted method itself.
* A receiver whose type is a type parameter cannot be passed to an existing method in this version.
* A `base` call to a virtual method cannot be intercepted by an existing method.
* For a method group, the existing method must have exactly the signature of the intercepted method, and `WithArgs` can bind only type parameters.

## Type parameters of an existing method

Each type parameter of the interceptor, and of the types that declare it, receives the first of these types that exists. A type parameter that receives no type is an error.

| Order | Type |
|---|---|
| 1 | The property of the same name of the object given to `WithArgs`, as a `Type` or an <xref:Metalama.Framework.Code.IType>. |
| 2 | The type argument of the type parameter of the same name of the intercepted method or of the types that declare it. |

Pass the definition of a generic method or of a method of a generic type: its type parameters are resolved for each call site. When a type argument cannot be written, such as an anonymous type, the rewritten call omits the type arguments, and the compiler infers them.

For instance, the type parameter `T` of the following interceptor binds to the type parameter `T` of `Cache.Get<T>`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingGeneric.Aspect.cs marker="Interceptor"]

### Example: A generic interceptor

In the following example, the type parameter `T` of the interceptor is bound to the type parameter `T` of `Cache.Get<T>`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/BindingGeneric.cs name="A generic interceptor"]

## Parameters of a generated method

A method generated from a template has this signature, in this order:

1. A receiver parameter, when the intercepted method has a receiver.
2. One parameter for each parameter of the intercepted method, with the same name and reference kind, without default value and without `params`.
3. The added parameters.

The run-time parameters of the template bind to the parameters of the generated method by name, or else by position after the receiver parameter. A run-time parameter that binds to no parameter receives its default value. Without a default value, it is an error.

<xref:Metalama.Extensions.Interceptors.ITemplateInterception.WithArgs*> gives:

* the values of the compile-time parameters and type parameters of the template;
* for a run-time parameter of the template that has no match, a new added parameter, with its value at each call site;
* the values of the parameters that a <xref:Metalama.Extensions.Interceptors.ITemplateInterception.Configure*> delegate adds.

A `Configure` delegate can also add a parameter and give its value at each call site with <xref:Metalama.Extensions.Interceptors.IInterceptorMethodBuilder.SetArgument*>, which can depend on the call site. A parameter cannot receive a value from both `WithArgs` and `SetArgument`.

For instance, the following template has two run-time parameters that the intercepted method does not have:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TemplateAddedParameter.Aspect.cs marker="Signature"]

`category` receives its value from `WithArgs`, and `caller` from the `Configure` delegate:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Interceptors/TemplateAddedParameter.Aspect.cs marker="Configure"]

When the delegate widens the type of a parameter, `meta.Proceed()` casts it back to the type of the intercepted method.

### Example: Added parameters

In the following example, `category` is a run-time parameter of the template whose value is given by `WithArgs`, and `caller` is a parameter added by the `Configure` delegate. Both become parameters of the generated method, and each call site passes its own value. The call sites still share one generated method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/TemplateAddedParameter.cs name="Added parameters"]

## Type parameters of a generated method

* When the template has run-time type parameters, they bind to the type parameters of the intercepted method, and the generated method is generic.
* Otherwise, the generated method is specialized for the type arguments of the call site, which a compile-time type parameter of the template can receive through `WithArgs`.
* A type parameter of the calling type that the generated method cannot name becomes a type parameter of the generated method.
* A `Configure` delegate can add type parameters, and give their type arguments with <xref:Metalama.Extensions.Interceptors.IInterceptorMethodBuilder.SetTypeArgument*>.

For the use cases and examples, see <xref:generic-interceptor-templates>.

## What a Configure delegate can change

| Can change | Cannot change |
|---|---|
| The name of the method and of its parameters. The template binds to the new names. | The reference kinds of the parameters. |
| The accessibility, within the limits of the call sites. | The positions of the receiver and argument parameters. |
| `IsStatic`. An instance method requires `this` at every call site. | The return type. |
| The type of a parameter, to a type to which the argument converts implicitly. A parameter passed by reference keeps its type. | The names of the derived type parameters. |
| The type of the receiver parameter, for instance to an interface of the receiver. | |
| Attributes, added parameters and added type parameters. | |

An attempt to change a locked part throws an exception, which is reported as an error.

> [!div class="see-also"]
> <xref:redirecting-calls-to-existing-methods>
> <xref:generating-interceptors-with-templates>
> <xref:interceptor-deduplication-rules>
