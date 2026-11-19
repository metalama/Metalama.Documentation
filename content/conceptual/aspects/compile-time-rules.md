---
uid: compile-time-rules
level: 300
summary: "This article explains the two rules that compile-time code must follow, immutability and durability, why Metalama enforces them, and how to fix the warnings that its analyzers report."
keywords: "compile-time code, immutability, durability, ImmutableType, Durable, aspect state, static fields, memory leaks, design time, IDurableRef, DurableLazy, DurableDangerous, LAMA0870, LAMA0880, LAMA0889"
created-date: 2026-11-19
modified-date: 2026-11-19
---

# Compile-time coding rules

Aspects, fabrics, and validators look like ordinary C# classes, but they don't live like ordinary objects. Metalama creates them once and reuses them, for many target declarations and, in the IDE, for many versions of your project. Compile-time code must therefore follow two rules:

* **Immutability**: an object that Metalama reuses must not change after it has been created.
* **Durability**: an object that Metalama keeps across compilations must not reference anything that belongs to one compilation.

Starting with Metalama 2027.0, analyzers verify both rules while you type and report warnings when your code breaks them. This article explains the rules and how to fix the warnings.

## Why compile-time objects need rules

An aspect instance is created once and can be reused for every declaration it applies to. At design time, the IDE produces a new version of the project on essentially every keystroke, and Metalama keeps reusing the same aspect instances, fabrics, and validators, and everything they registered, for as long as the compile-time code doesn't change.

This has two consequences:

* **State leaks between targets.** If an aspect changes a field while it processes one target, the next target sees the changed value. The order in which Metalama processes the targets isn't defined, so the result can differ between builds, and between a build and the IDE.
* **Memory leaks in the IDE.** An object kept across compilations that references a declaration of the code model, a Roslyn symbol, or a syntax tree keeps the whole version of the project it came from in memory. Memory then grows for as long as you edit. See <xref:diagnosing-memory-leaks>.

A static field of compile-time code is the extreme case of both: it's shared by every aspect instance, every target, and every compilation, and in the IDE it keeps its value for as long as the analysis process runs.

## The immutability rule

The <xref:Metalama.Framework.Aspects.IAspect> interface and the <xref:Metalama.Framework.Fabrics.Fabric> class are marked with the <xref:Metalama.Framework.Utilities.ImmutableTypeAttribute?text=[ImmutableType]> attribute, and every type that derives from them or implements them inherits the contract. Validators are checked in the same way. In a type subject to the contract:

* Every instance field must be `readonly`, or private and assigned only in a constructor or an `init` accessor.
* Every automatic property must have no setter, an `init` accessor, or a private setter that is assigned only in a constructor or an `init` accessor.
* The type of every field and property must itself be immutable, all the way down. Intrinsic types, enums, delegates, and the collections of `System.Collections.Immutable` are immutable, and an immutable collection is immutable only when its type arguments are. Any other type is mutable unless it's marked `[ImmutableType]` or declared as immutable in the project file.

The warnings of this rule are <xref:lama0880?text=LAMA0880> to <xref:lama0888?text=LAMA0888>. For the full list, see <xref:errors-compile-time-contracts>.

The following aspect breaks the rule twice. It counts its targets in an instance field, and it caches data in a static dictionary:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeRules_Wrong.cs name="Mutable aspect"]

### Configuring an aspect without breaking the rule

The properties that users set with named arguments of the custom attribute don't need a setter: an `init` accessor is enough, and it makes the aspect immutable once it has been created.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeRules_Immutable.cs name="Immutable aspect"]

### Keeping per-target state

When an aspect needs to compute data for one target and use it later, keep the data in the objects that `BuildAspect` creates for that target, not in the aspect:

* Use local variables of `BuildAspect`, and pass them to the template as _tags_ or as template arguments. See <xref:sharing-state-with-advice>.
* Use the <xref:Metalama.Framework.Aspects.IAspectState> of the aspect instance when child aspects or aspects of derived types need the data.
* Use annotations to attach data to a declaration for other aspects.

## The durability rule

The <xref:Metalama.Framework.Utilities.DurableAttribute?text=[Durable]> attribute declares that an object can be held across compilations. Aspects, fabrics, annotations, and aspect states are bound by this contract, because Metalama keeps them across compilations at design time. A durable object must not hold a strong reference to a compilation, a syntax tree, a semantic model, a Roslyn symbol, a declaration of the code model, or a non-durable reference, directly or through another object.

Intrinsic types are durable, and a system collection is durable when its type arguments are. Any other type isn't durable unless it's marked `[Durable]` or declared as durable in the project file. A delegate type is never durable by its declared type, because a delegate holds everything its closure captured. The analyzer examines what each lambda actually captures where a durable value is required.

The warnings of this rule are <xref:lama0870?text=LAMA0870> to <xref:lama0879?text=LAMA0879>. To fix them:

* **Store an identifier instead of the object.** Replace a declaration with a durable reference: call `ToDurableRef()` on the declaration, store the <xref:Metalama.Framework.Code.IDurableRef`1>, and resolve it with `GetTarget( compilation )` when you need the declaration. When the identifier must cross a process or a project boundary, store a <xref:Metalama.Framework.Code.SerializableDeclarationId>. A reference to an attribute can't be durable, because an attribute has no identifier of its own: store a durable reference to the declaration that carries it, and find the attribute again in its `Attributes` collection.
* **Store a durable expression** instead of an <xref:Metalama.Framework.Code.IExpression>, by calling <xref:Metalama.Framework.Code.ExpressionExtensions.ToDurable*>.
* **Use <xref:Metalama.Framework.Utilities.DurableLazy`1> instead of `Lazy<T>`.** `Lazy<T>` holds its factory delegate, and therefore everything the delegate captured.
* **Mark your own type `[Durable]`** when it holds only durable members. The analyzer then verifies its members.
* **Use <xref:Metalama.Framework.Utilities.DurableDangerous`1>** as a last resort, when a value is durable in fact but the analyzer can't establish it. The wrapper states the assertion in the signature of the member, which is better than suppressing the warning.

For an example of a fabric that keeps a declaration alive and of its fix, see <xref:diagnosing-memory-leaks#store-a-durable-reference-instead>.

## Static fields and properties

A static field or automatic property of a compile-time or run-time-or-compile-time type must be read-only, of an immutable type, and of a durable type. Constants aren't concerned. Metalama reports the first rule that a member breaks:

| Problem | Compile-time type | Run-time-or-compile-time type |
|---------|-------------------|-------------------------------|
| The member isn't read-only. | <xref:lama0889?text=LAMA0889> | <xref:lama0891?text=LAMA0891> |
| The member is read-only, but its type isn't immutable. | <xref:lama0890?text=LAMA0890> | <xref:lama0891?text=LAMA0891> |
| The member is read-only and immutable, but its type isn't durable. | <xref:lama0892?text=LAMA0892> | <xref:lama0892?text=LAMA0892> |

In a run-time-or-compile-time type, such as an aspect class, a static field that only run-time code uses can be marked with <xref:Metalama.Framework.Aspects.RunTimeAttribute?text=[RunTime]>, so that it exists only at run time and isn't checked.

To keep a cache across targets, don't use a static field. Compute the data again for each target, or store it in an annotation or in the aspect state of the declaration it belongs to.

## Marking your own types

When an aspect or a fabric stores an object of your own type, that type must satisfy the same rules. Mark it with `[ImmutableType]` and `[Durable]`: the analyzer then verifies its members, and types that derive from it inherit the contracts.

In the following example, a fabric creates one `LogPolicy` object and passes it to every aspect it adds. Because the aspects store the policy, `LogPolicy` is marked as immutable and durable:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeRules_OwnType.cs name="Own immutable and durable type"]

For types that you don't own, declare the verdict in the project file with MSBuild items. Each item contains the full metadata name of a type, with the arity of generic types, as in ``Contoso.Pair`2``:

| Item | Meaning |
|------|---------|
| `MetalamaImmutableType` | The type is immutable. |
| `MetalamaImmutableContractType` | The types that derive from this type or implement it must be immutable, as if it were marked `[ImmutableType]`. |
| `MetalamaDurableType` | The type is durable. |
| `MetalamaNonDurableType` | The type isn't durable, even if the analyzer would otherwise consider it durable. |

A name that matches no type is reported with <xref:lama0879?text=LAMA0879> or <xref:lama0885?text=LAMA0885>.

## Turning the warnings off

All the diagnostics of these rules are warnings. When a rule doesn't apply to one declaration, suppress the warning with the usual mechanisms: `#pragma warning disable`, `[SuppressMessage]` with a justification, or a severity in an `.editorconfig` file. There is deliberately no attribute that exempts a type.

The `MetalamaEnforceImmutabilityContract` MSBuild property turns off the immutability rule and the checks of static fields for a whole project. It's intended for projects that implement Metalama itself, not for projects that use it.

> [!div class="see-also"]
> <xref:diagnosing-memory-leaks>
> <xref:sharing-state-with-advice>
> <xref:errors-compile-time-contracts>
> <xref:Metalama.Framework.Utilities.ImmutableTypeAttribute>
> <xref:Metalama.Framework.Utilities.DurableAttribute>
