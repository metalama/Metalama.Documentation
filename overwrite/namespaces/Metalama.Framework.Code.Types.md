---
uid: Metalama.Framework.Code.Types
summary: *content
created-date: 2023-01-26
modified-date: 2023-07-11
---

This namespace contains specializations of the <xref:Metalama.Framework.Code.IType> interface.


## Simplified class diagram

```mermaid
classDiagram
      IType <|-- INamedType
      INamedType <|-- ITupleType
      IType <|-- ITypeParameter
      IType <|-- IArrayType
      IType <|-- IPointerType
      IType <|-- IFunctionPointerType
      IType <|-- IDynamicType
```

> [!WARNING]
> <xref:Metalama.Framework.Code.IExtensionBlock>, despite implementing <xref:Metalama.Framework.Code.INamedType>, should never be used as an <xref:Metalama.Framework.Code.IType>.
