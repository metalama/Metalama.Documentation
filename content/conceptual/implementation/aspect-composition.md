---
uid: aspect-composition
level: 400
summary: "Aspect composition refers to applying multiple aspects to the same class. Metalama addresses this complex issue through strong ordering of aspects and advice, code model versioning, and ensuring safe composition of advice."
keywords: "aspect composition, multiple aspects, strong ordering, code model versioning, safe composition, Metalama, .NET, advice, aspect layer, depth level"
created-date: 2023-03-02
modified-date: 2025-11-30
---

# Aspect composition

Aspect composition refers to the situation where multiple aspects are applied to the same class. This is a complex issue, and Metalama addresses it by offering a consistent and deterministic model for aspect composition.

There are three critical points to consider:

1. Strong ordering of aspects and advice
2. Code model versioning
3. Safe composition of advice

## 1. Strong ordering of aspects and advice

Aspects are entities that take a code model as input and produce outputs such as advice, diagnostics, validators, and child aspects. The only output relevant to this discussion is _advice_, as other outputs do not alter the code. While most aspects have a single layer of advice, it is possible to define multiple layers.

To ensure a consistent order of execution for aspects and advice, Metalama employs two ordering criteria:

1. _Aspect layer_: The order of execution for aspect layers _must_ be specified by the aspect author or user. To learn more about aspect layer ordering, refer to <xref:ordering-aspects>.

2. _Depth Level_ of target declarations: Every declaration in the compilation is assigned a _depth level_. Within the same aspect layer, declarations are processed in order of increasing depth. For example, base classes are visited before derived classes, and types are processed before their members.

Aspects and advice in the same layer, applied to declarations of the same depth, are executed in an unspecified order and may be executed concurrently on multiple threads.

## 2. Code model versioning

As the code model only represents declarations and does not provide access to implementations such as method bodies or initializers, the only types of advice that affect the code model are introductions and interface implementations. Overriding an existing method does not impact the code model as it merely changes its implementation.

For each aspect layer and depth level, Metalama creates a new version of the code model that reflects the changes made by the previous aspect layer or depth level.

Therefore, if an aspect introduces a member into a type, subsequent aspects will see that new member in the code model and may advise it.

To maintain the consistency of this model, aspects cannot provide outputs to previous aspects or to declarations that are not below the current target.

## 3. Safe composition of advice

When several aspects, unaware of each other, add advice to the same declaration, Metalama ensures that the resulting code will be correct.

For instance, if two aspects override the same method, both aspects are guaranteed to compose correctly. This is a complex problem, but Metalama resolves it, eliminating the need for you to worry about it.

[comment]: # (TODO: example log and cache)

> [!div class="see-also"]
> <xref:ordering-aspects>
> <xref:implementation>
> <xref:pipeline>
> <xref:aspects>



