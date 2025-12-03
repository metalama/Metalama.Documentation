---
uid: code-api
summary: "This namespace defines the compile-time representation of the code model."
keywords: "Metalama Framework, API documentation, creating aspects, managing aspects, source code representation, code fixes, diagnostics, eligibility, project model, runtime classes"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Code Model API documentation

These namespaces define the compile-time representation of the code model, providing access to declarations, types, and code structure during aspect execution.

| Namespace                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Code>        | Root namespace representing the code model, containing core interfaces like <xref:Metalama.Framework.Code.IDeclaration>, <xref:Metalama.Framework.Code.IType>, <xref:Metalama.Framework.Code.IMethod>, and <xref:Metalama.Framework.Code.ICompilation>.                                                 |
| <xref:Metalama.Framework.Code.Collections> | Contains specialized collections for the code model, including <xref:Metalama.Framework.Code.Collections.INamedDeclarationCollection`1> and related types.                                                |
| <xref:Metalama.Framework.Code.Types>        | Defines special type representations like <xref:Metalama.Framework.Code.ITypeParameter>, <xref:Metalama.Framework.Code.Types.IArrayType>, and <xref:Metalama.Framework.Code.Types.IPointerType>.                                                 |
| <xref:Metalama.Framework.Code.Comparers>        | Contains equality and conversion comparers for comparing types and declarations.                                                |
| <xref:Metalama.Framework.Code.DeclarationBuilders>  | Defines interfaces for building types and members programmatically. Used when introducing new members through advice.                                                 |
| <xref:Metalama.Framework.Code.Invokers>        | API for generating code that invokes methods, accesses properties, or instantiates types at runtime from templates.                                                 |
| <xref:Metalama.Framework.Code.SyntaxBuilders>        | Syntax builders and factories for dynamically creating expressions and statements using <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> and <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder>.                                                 |

> [!div class="see-also"]
> <xref:type-system>
