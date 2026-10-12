---
uid: interceptors-troubleshooting
level: 300
summary: "Understand why a call is not intercepted: calls that are not in the source code, calls outside the scope, calls that cannot be intercepted in this version, and the diagnostics that Metalama reports."
keywords: "interceptors, troubleshooting, limitations, call not intercepted, NonInterceptableReason, LAMA1012, LAMA1014, expression tree, ref return, source generators, transformed code"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Interceptor limitations and troubleshooting

This article helps you understand why a call is not intercepted, and lists the limitations of this version.

## Seeing what was intercepted

The editor shows the source code, not the rewritten calls, and the generated interceptors are not visible in the code model of aspects. To see which calls were rewritten, and the code of the generated interceptors, look at the transformed code. See <xref:debugging-aspects>.

## Calls that are never intercepted

Only the calls written in the source code of the current project are intercepted. These calls are never intercepted:

* Calls that the compiler, the runtime or a library makes on behalf of the code, because there is no call at the call site. For instance, an interpolated string or `Console.WriteLine( object )` calls `ToString` inside the runtime, a `foreach` statement calls `GetEnumerator`, a `using` statement calls `Dispose`, and collections call `Equals` and `GetHashCode`.
* Calls in code that aspects introduce.
* Calls in the output of Roslyn source generators, because source generators run after Metalama.
* Calls inside other assemblies, including the intercepted library itself.
* Calls through reflection, `dynamic`, or a delegate.
* Method groups in a `nameof` expression.

## Calls outside the scope

A call is intercepted only when it is in the scope of a registration. Check that:

* the call is in the target of the aspect, or in the declarations selected by the fabric;
* the call is not in a nested type, unless the registration calls `IncludingNestedTypes()`;
* the call is not in a lambda or a local function when the registration calls `ExcludingLambdas()`;
* the call is not in a declaration that has the `[ExcludeInterceptors]` attribute;
* the called method is selected. A method that a type inherits is not selected by a selection of the derived type: select the base type. A call bound to an interface method is not a call to its implementation: select the interface, or use `IncludingInterfaceImplementations()`.

See <xref:interceptors-selecting-calls>.

## Calls that cannot be intercepted

Some calls match a registration but cannot be intercepted in this version. When the registration returns an interceptor for such a call, Metalama reports the warning LAMA1012 and leaves the call unchanged. In `ForEachSite`, read <xref:Metalama.Extensions.Interceptors.IInterceptionContext.NonInterceptableReason> and skip these call sites to avoid the warning.

| Reason | Description |
|---|---|
| `RefReturn` | The intercepted method returns by reference. |
| `ScopedParameter` | The signature requires `scoped` or `[UnscopedRef]`. |
| `PointerType` | A pointer or function pointer type is in the signature, in a type argument, or in the type of the receiver. |
| `VariableArguments` | The intercepted method uses `__arglist`. |
| `UnsupportedDefaultValue` | An omitted optional argument has a default value that cannot be written in C#, and the interceptor receives it. |
| `ConditionalAccessMutableReceiver` | The call is in a conditional access, and the receiver is a type parameter that would have to be passed by reference. |
| `PromotedFieldInitializer` | The call is in the initializer of a field that an aspect promotes to a property. |
| `MethodReferenceReceiverNotSupported` | The receiver of a method group can be kept neither in a method group nor in a wrapper. |
| `ExpressionTree` | The call is in a lambda converted to an expression tree, for instance in a query on `IQueryable`. The compiler describes the call instead of making it. |

Later versions can add values to this enumeration.

### Example: Calls that cannot be intercepted

In the following example, the call to a method that returns by reference, and the call in a lambda converted to an expression tree, cannot be intercepted. Metalama reports LAMA1012 for each of them. The same call in a lambda converted to a delegate is intercepted.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Interceptors/UnsupportedCalls.cs name="Calls that cannot be intercepted"]

## Uses that are not supported in this version

* Property and indexer accesses, event accessors, constructor calls, operators and `await` expressions cannot be intercepted.
* C# 14 extension members cannot be intercepted.
* Interceptors generated from templates intercept only calls, not method groups.
* An existing method cannot receive a receiver whose type is a type parameter, and cannot intercept a `base` call to a virtual method.

## Diagnostics

| Diagnostic | Severity | Cause |
|---|---|---|
| LAMA1012 | Warning | The call cannot be intercepted in this version. The reason is part of the message. The call is left unchanged. |
| LAMA1013 | Error | The parameters of an existing method cannot be bound to the call. The message gives the parameter and the reason. |
| LAMA1014 | Error | The interceptor or its `Configure` delegate is invalid for the call, for instance a template for a method group, or an added parameter without a value. |
| LAMA1016 | Error | The template cannot be found or cannot implement the interceptor. |
| LAMA1024 | Error | The location of the generated interceptor cannot be used for the call, for instance `PlaceInCallingType` in top-level statements. |
| LAMA0661 | Warning | A variable that hides a parameter of the calling member passed to the interceptor was renamed. |

## Other tools that rewrite calls

* Metalama interceptors and the C# `[InterceptsLocation]` interceptors that source generators produce are independent mechanisms. Do not intercept the same call with both.
* Aspects that run after a low-level aspect weaver cannot register interceptors.

> [!div class="see-also"]
> <xref:interceptors-selecting-calls>
> <xref:interceptor-binding-rules>
> <xref:debugging-aspects>
