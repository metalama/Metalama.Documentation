---
uid: interceptors-use-cases
level: 200
summary: "Decide whether Metalama interceptors fit your problem: the problems they solve well, the problems better solved another way, a comparison with other techniques, and the costs and risks."
keywords: "interceptors, use cases, when to use interceptors, legacy code, seams, telemetry, audit, resilience, test seams, C# interceptors, source generators, AI agents"
created-date: 2026-10-12
modified-date: 2026-10-12
---

# When to use interceptors

Interceptors change the behavior of calls without changing the code that makes them. The change is written once, outside the business code, and every build applies it to the matching calls. This article explains when this is the right approach, and when another technique is better.

## What interceptors are good at

Interceptors combine three properties that other techniques rarely offer together:

* **Interceptors reach any method.** The intercepted method can be static, sealed, non-virtual, an extension method or a struct method, in any assembly. You need neither its source code, nor an interface, nor dependency injection.
* **Interceptors know the caller.** A registration applies only to the code you select, and the interceptor can receive the calling member, its parameters, or the calling object.
* **One registration covers many methods.** A T# template generates the interceptor for any signature, so one registration can cover a whole API.

## Problems that interceptors solve well

### Enforcing a policy on every call

A policy that applies to every call to an API, now and in the future, is a good fit. Examples are telemetry, an audit trail of calls to sensitive APIs, retries around a third-party service, and argument checks before calling an API that you don't own. The policy is defined once, it applies to the code written next year, and it keeps the business code free of repetitive statements.

See the examples _Auditing calls to `File.Delete`_ in <xref:redirecting-calls-to-existing-methods>, and _Timing every call to an SDK_ and _Retrying calls to a third-party service_ in <xref:generating-interceptors-with-templates>.

### Changing legacy code safely

Code without tests is risky to change, and adding tests usually requires changing the code first. An interceptor is a _seam_: a place where you can change the behavior of the program without editing the code at that place. Interceptors create a seam at every call site, which supports a safe workflow:

1. **Understand**: count which calls to an API actually run, and with which arguments.
2. **Pin the behavior**: replace hard-wired dependencies, such as `Thread.Sleep` or the file system, with fakes, and write tests.
3. **Replace**: route the calls to a new implementation, one module at a time.
4. **Verify**: run the old and the new implementation side by side, and compare their results.
5. **Remove**: once the new implementation is proven, change the call sites in the source code, and delete the interceptors.

See the examples _Replacing `Thread.Sleep` in tests_, _Counting the calls to a legacy API_ and _Comparing a new implementation with the legacy one_ in <xref:redirecting-calls-to-existing-methods>, and _Switching one module at a time_ in <xref:interceptors-selecting-calls>.

### Changing behavior temporarily or in some builds only

Interceptors live in one file, and a fabric can register them only in some build configurations. This suits changes that you don't want in the business code:

* Investigating a problem without a debugger, for instance in a CI build, in a container, or when an AI agent investigates a problem. One file traces the calls to a library, and deleting the file removes the tracing. Compare this with log statements added to many files, which must be found and removed afterwards.
* Injecting failures or latency into the calls to a dependency in a test build.
* Disabling external side effects, such as sending emails, in a staging build.

See the examples _Tracing calls to investigate a problem_ in <xref:generating-interceptors-with-templates>, and _Tracing only in some builds_ in <xref:interceptors-selecting-calls>.

### Migrating from PostSharp

PostSharp can apply a `MethodInterceptionAspect` to the methods of another assembly by rewriting the calls. Metalama aspects cannot modify external methods, but interceptors provide the same capability.

### Generated code checked into the source tree

Calls in generated files that are part of the source code, such as OpenAPI clients or Windows Forms designer files, are intercepted like any other call. Regenerating the files does not remove the interception. The output of Roslyn source generators is not intercepted, because source generators run after Metalama.

## Problems better solved another way

| Problem | Better solution |
|---|---|
| Generating optimized code from the values of the arguments, as Dapper.AOT, EF Core precompiled queries and ASP.NET Core minimal APIs do. | A Roslyn source generator with C# interceptors. Metalama can read constant arguments, but it cannot analyze a lambda, a query or a SQL string to generate code from it. |
| A one-time change, such as moving off an obsolete API or forwarding a `CancellationToken`. | A code fix, a refactoring, or an AI agent, so that the change is visible in the source code. |
| Adding behavior to a library that provides a hook, such as `DelegatingHandler` for `HttpClient`, EF Core interceptors, or `DiagnosticSource`. | The hook of the library. |
| Calls that are not in your source code: calls inside libraries, calls made by the framework, calls through reflection or `dynamic`. | Hooks of the library, or run-time instrumentation. |
| Changing a method that you own for every caller. | An override aspect. See <xref:overriding-methods>. |
| Logic that differs at each call site, or a handful of call sites. | Write the logic at the call site. |
| Calls in projects that do not build with Metalama, such as the consumers of a public NuGet package. | A Roslyn source generator. |
| Property accesses, constructor calls, operators and `await` expressions. | Not supported in this version. |

## Comparison with other techniques

| Technique | What it does better | What interceptors can do that it cannot |
|---|---|---|
| Override aspects | The behavior is in the method, for every caller. | Change methods of other assemblies; apply only to some callers. |
| Decorators and dynamic proxies | No build-time tooling. | Intercept static methods and objects not created by a dependency injection container; know the caller; no reflection. |
| Wrapper abstractions, such as `IFileSystem` or `TimeProvider` | The dependency is explicit, which is the best design for new code. | Add a seam without changing every call site, which matters in legacy code. |
| Run-time detours, such as Microsoft Fakes or Harmony | No build-time tooling. | Work with Native AOT; apply only to some callers; no patching at run time. |
| C# interceptors with a source generator | Generate optimized code from the arguments of each call; no Metalama dependency. | No generator to write; one template for many signatures; method groups and event handlers. |
| Editing the call sites, by hand or with an AI agent | The change is visible in the source code. | Apply a rule to every call, including the calls written later; keep the business code free of repetitive statements; remove the change by deleting one file. |

An AI agent changes code once, while an interceptor is a rule that every build applies. When you work on legacy code with an agent, let the agent write the fabric and the interceptor, which are small and easy to review, and leave the business code unchanged until tests and production data support the final change.

## Costs and risks

* **The source code does not contain the interceptor.** A reader of the calling code sees the original call. The rewritten call is visible in the diff preview of Visual Studio Tools for Metalama and in the output of the `LamaDebug` build configuration. Keep interceptors in a few well-named files.
* **Only one interceptor applies to a call site.** Two registrations that intercept the same call cause an error. Decide for each call site which registration handles it.
* **Only calls written in the source code are intercepted.** See <xref:interceptors-troubleshooting>.
* **Tests may run a different binary.** When interceptors replace dependencies only in a test build, the tests do not run the release binary. Keep the difference small, and remove the interceptors once the code has real abstractions.

> [!div class="see-also"]
> <xref:intercepting-calls>
> <xref:overriding-methods>
> <xref:validation>
