---
uid: aspect-composition
level: 400
summary: Aspect composition refers to applying multiple aspects to the same class. Metalama addresses this complex issue through strong ordering of aspects and advice, code model versioning, and ensuring safe composition of advice.
keywords: "aspect composition, multiple aspects, strong ordering, code model versioning, safe composition, Metalama, .NET, advice, aspect layer, depth level"
created-date: 2023-03-02
modified-date: 2025-11-30
---

# Aspect composition

Aspect composition occurs when multiple aspects are applied to the same class. Metalama addresses this complexity through a consistent and deterministic model for aspect composition.

There are three critical points to consider:

1. Strong ordering of aspects and advice
2. Code model versioning
3. Safe composition of advice

## Strong ordering of aspects and advice

Aspects are entities that take a code model as input and produce outputs such as advice, diagnostics, validators, and child aspects. The only output relevant to this discussion is _advice_, as other outputs don't alter the code. While most aspects have a single layer of advice, you can define multiple layers.

To ensure a consistent order of execution for aspects and advice, Metalama employs two ordering criteria:

1. _Aspect layer_: The aspect author or user must specify the order of execution for aspect layers. For more information about aspect layer ordering, see <xref:ordering-aspects>.

2. _Depth level_ of target declarations: Every declaration in the compilation is assigned a _depth level_. Within the same aspect layer, declarations are processed in order of increasing depth. For example, base classes are visited before derived classes, and types are processed before their members.

Aspects and advice in the same layer, applied to declarations of the same depth, are executed in an unspecified order and may be executed concurrently on multiple threads.

## Code model versioning

The code model only represents declarations and doesn't provide access to implementations such as method bodies or initializers. Therefore, the only types of advice that affect the code model are introductions and interface implementations. Overriding an existing method doesn't impact the code model because it only changes the implementation.

For each aspect layer and depth level, Metalama creates a new version of the code model that reflects the changes made by the previous aspect layer or depth level.

If an aspect introduces a member into a type, subsequent aspects will see that new member in the code model and can advise it.

To maintain the consistency of this model, aspects can't provide outputs to previous aspects or to declarations that aren't below the current target.

## Safe composition of advice

When several aspects add advice to the same declaration, Metalama ensures that the resulting code will be correct.

For example, if two aspects override the same method, both aspects are guaranteed to compose correctly. Metalama resolves this complexity automatically.

> [!div class="see-also"]
> <xref:ordering-aspects>
> <xref:implementation>
> <xref:pipeline>
> <xref:aspects>
