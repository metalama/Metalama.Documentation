---
uid: Metalama.Framework.Workspaces
summary: *content
created-date: 2023-07-11
modified-date: 2023-07-11
---

## Overview

This namespace enables the loading of a C# project and solution into the <xref:Metalama.Framework.Code> code model from any application, such as LinqPad.

The entry point of this namespace is the <xref:Metalama.Framework.Workspaces.WorkspaceCollection> class.

## Class Diagram

```mermaid
classDiagram
    class ICompilationSet {
        Compilations
        Types
        Methods
        Fields
        Properties
        FieldsAndProperties
        Constructors
        Events
        TargetFrameworks
    }

    class IIntrospectionCompilationDetails {
        Diagnostics
        AspectClasses
        AspectLayers
        AspectInstances
        Advice
        Transformations
        IsMetalamaEnabled
        HasMetalamaSucceeded
    }

    class IProjectSet {
        Projects
        GetSubset()
        GetDeclaration()
    }

    IProjectSet --> ICompilationSet  : SourceCode
    ICompilationSetResult --|> IIntrospectionCompilationDetails
    ICompilationSetResult --> ICompilationSet  : TransformedCode

    IProjectSet --|>  ICompilationSet
    IProjectSet --|> ICompilationSetResult
    Project --|> IProjectSet
    Workspace  --|> IProjectSet
    WorkspaceCollection --* Workspace

    IProjectSet --* Project : Projects
```


