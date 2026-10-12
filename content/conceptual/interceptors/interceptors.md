---
uid: intercepting-calls
level: 200
summary: "Intercept calls to methods of any assembly, including the .NET libraries and NuGet packages, and change their behavior in the code you choose, without editing the calling code."
keywords: "interceptors, call-site interception, Metalama.Extensions.Interceptors, intercept calls, code you don't own, third-party API, InterceptMethods, RedirectToExistingMethod, RedirectToSynthesizedMethod"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# Intercepting calls to code you don't own

> [!NOTE]
> This feature requires the `Metalama.Extensions.Interceptors` package and a Metalama Community license or higher.

An _interceptor_ is a method that a call site calls instead of the method written in the source code. When you intercept the calls to a method, Metalama rewrites each matching call site at build time so that it calls your interceptor. The interceptor can run code before and after the call, change the arguments, process the result, handle exceptions, or replace the call entirely.

Interception works on the _calls_, not on the called method. This makes it different from overriding a method with an aspect:

* Overriding a method changes it for every caller, and requires the source code of the method. See <xref:overriding-methods>.
* Intercepting the calls to a method changes only the calls that the code you choose makes, and works for methods of any assembly: the .NET libraries, NuGet packages, or the projects of another team. The called method itself does not change.

## Benefits

* **Reach any method**: static, sealed, non-virtual, extension and struct methods, from any assembly.
* **Choose where the change applies**: a type, a namespace, the whole project, or some build configurations only.
* **Know the caller**: an interceptor can receive the calling member, its parameters, or the calling object.
* **Keep the business code clean**: the behavior is defined once, outside the code that makes the calls, and applies to calls written in the future.
* **No run-time overhead of reflection or proxies**: the calls are rewritten at build time.
* **Synthesize the interceptor methods from T# templates**: one template generates the interceptors of many methods with different signatures.

Before you choose interceptors, read <xref:interceptors-use-cases>, which also describes the problems that interceptors do not solve well.

## How it works

You register interceptors from an aspect or a fabric. A registration selects the methods whose calls are intercepted, for instance `HttpClient.GetStringAsync`, and gives the interceptor.

After all aspects have run, Metalama finds the matching call sites in the source code of the current project and rewrites them. The source code does not change. To see the rewritten calls, use the diff preview of Visual Studio Tools for Metalama, or build with the `LamaDebug` configuration. See <xref:debugging-aspects>.

Metalama interceptors are not related to the C# `[InterceptsLocation]` interceptors that source generators produce, although both rewrite calls at build time.

## Two ways to write an interceptor

You can write the interceptor yourself, as an ordinary static method, or let Metalama generate it from a T# template.

| | Existing method | T# template |
|---|---|---|
| You write | A plain C# static method. | A T# template. |
| Signatures | One method per signature of the intercepted methods, or a generic method. | One template for any signature. |
| Debugging and testing | Like any C# code. | Through the transformed code. |
| Reading the call | Through its arguments and caller information attributes. | Through `meta.Interception`. |
| Accessing the state of the calling object | Only values passed as arguments. | Private fields and members, when the interceptor is generated in the calling type. |
| Typical uses | Fakes, routers, checks, audit. | Tracing, timing, retry. |

## Installing the package

Add a reference to the `Metalama.Extensions.Interceptors` package to the project that registers the interceptors. The package requires a registered Metalama license. A free Metalama Community license is enough.

## In this chapter

| Article | Description |
|---------|-------------|
| <xref:interceptors-use-cases> | When to use interceptors, when not to, and how they compare with other techniques. |
| <xref:redirecting-calls-to-existing-methods> | Redirect calls to a static method that you write. |
| <xref:generating-interceptors-with-templates> | Generate interceptors from T# templates, for many methods with different signatures, write generic templates, and understand when call sites share a generated interceptor. |
| <xref:interceptors-selecting-calls> | Choose the methods, the calling code and the build configurations where calls are intercepted, and decide for each call site. |
| <xref:interceptor-binding-rules> | Reference: how the arguments of a call reach the parameters of the interceptor. |
| <xref:interceptors-troubleshooting> | Calls that are not intercepted, limitations, and diagnostics. |

> [!div class="see-also"]
> <xref:Metalama.Extensions.Interceptors>
> <xref:overriding-methods>
> <xref:fabrics>
