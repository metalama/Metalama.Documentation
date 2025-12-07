---
uid: contract-patterns
summary: "Metalama Contracts facilitate contract-based programming, enhancing software reliability and clarity. They enforce preconditions, postconditions, and type invariants, aiding in error detection and promoting modular design. They are more readable, inheritable, and customizable than hand-written precondition checks. "
level: 100
keywords: "contract-based programming, preconditions, postconditions, type invariants, error detection,  Metalama Contracts"
created-date: 2024-06-11
modified-date: 2025-11-30
---

# Metalama.Patterns.Contracts

Metalama Contracts facilitate the implementation of Contract-Based Programming principles, a software engineering practice that significantly enhances software reliability and clarity. In contract-based programming, a _contract_ defines a set of obligations and expectations between two parties: a caller and a callee. Metalama Contracts embody three fundamental concepts of contract-based programming: preconditions, postconditions, and type invariants.

_Preconditions_ ensure that necessary conditions are met before a method executes, while _postconditions_ verify the results after execution. This enforcement aids in early error detection, preventing a defect in one component from manifesting in another. For example, when preconditions are properly enforced, a <xref:System.NullReferenceException> in a component can be attributed to the component itself, whereas an <xref:System.ArgumentException> thrown by a method shifts the blame to its _caller_. Similarly, Metalama Contracts introduce a new exception type, <xref:Metalama.Patterns.Contracts.PostconditionViolationException>, indicating that the method throwing it is responsible for the defect.

_Type invariants_ are conditions that remain true throughout the entire lifetime of an instance of a type. Type invariants are useful in diagnosing defects in types with mutable states and complex logic.

Adhering to these contracts promotes modular and decoupled design, fostering system extensibility and adaptability. Overall, enforcing preconditions and postconditions is a pragmatic practice crucial for creating maintainable, reliable, and scalable software systems.

Compared to hand-written precondition checks, Metalama Contracts offer the following benefits:

- **More readable**: Metalama Contracts are represented as custom attributes, resulting in less code to read and understand.

- **Inherited**: You can add a Metalama contract attribute to an interface method parameter, and it's automatically enforced in all implementations of this method.

- **Customizable**: It's easy to change the code generation pattern that throws the exception. For example, you can localize your exception messages as an afterthought, without any impact on your business code.

## In this chapter

This chapter includes the following articles:

| Article | Description |
|--|--|
| <xref:value-contracts> | Describes how to add preconditions and postconditions to your fields, properties, and parameters. |
| <xref:invariants> | Describes how to add type invariants and execute verification logic after the completion of all public methods. |
| <xref:configuring-contracts> | Describes how to configure Metalama Contracts. |
| <xref:contract-types> | Lists all available contract attributes. |
| <xref:enforcing-non-nullability> | Explains how to automatically enforce non-nullable declarations. |

> [!div class="see-also"]
> <xref:patterns>
