---
uid: Metalama.Framework.Aspects
summary: *content
created-date: 2023-01-26
modified-date: 2024-06-26
---

This namespace enables you to build aspects. Aspects represent an algorithmic approach to code transformation or validation.

For instance, tasks such as adding logging to a method or implementing `INotifyPropertyChanged` can largely be expressed as an algorithm and, therefore, implemented as an aspect.

## Conceptual Documentation

Refer to <xref:aspects> for more information.

## Overview

To create an aspect, you need to create a class that derives from <xref:System.Attribute> and implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface. Alternatively, you can use one of the following classes, which already derive from <xref:System.Attribute>, have the appropriate <xref:System.AttributeUsageAttribute>, and implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface:

* <xref:Metalama.Framework.Aspects.CompilationAspect>
* <xref:Metalama.Framework.Aspects.ConstructorAspect>
* <xref:Metalama.Framework.Aspects.EventAspect>
* <xref:Metalama.Framework.Aspects.FieldAspect>
* <xref:Metalama.Framework.Aspects.FieldOrPropertyAspect>
* <xref:Metalama.Framework.Aspects.MethodAspect>
* <xref:Metalama.Framework.Aspects.ParameterAspect>
* <xref:Metalama.Framework.Aspects.PropertyAspect>
* <xref:Metalama.Framework.Aspects.TypeAspect>
* <xref:Metalama.Framework.Aspects.TypeParameterAspect>

## Class Diagrams

### Aspect builders


```mermaid
classDiagram

    class IAspect {
        BuildAspect(IAspectBuilder)
        BuildEligibility(IEligibilityBuilder)
    }

    class IAspectBuilder {
        SkipAspect()
        TargetDeclaration
    }

    class IAdviser {
        Target
        With(declaration)
    }


    class ScopedDiagnosticSink {
        Report(...)
        Suppress(...)
        Suggest(...)
    }

    class AdviserExtensions {
        <<static>>
        Override(...)
        Introduce*(...)
        ImplementInterface(...)
        AddContract(...)
        AddInitializer(...)
    }

    class IAspectReceiver {
        Select(...)
        SelectMany(...)
        Where(...)
        AddAspect(...)
        AddAspectIfEligible(...)
        Validate(...)
        ValidateInboundReferences(...)
        ReportDiagnostic(...)
        SuppressDiagnostic(...)
        SuggestCodeFix(...)
    }

    IAspect --> IAspectBuilder : BuildAspect() receives
    IAspectBuilder --|> IAdviser : inherits 
    IAspectBuilder --> ScopedDiagnosticSink : exposes
    IAspectBuilder --> IAspectReceiver : exposes
    AdviserExtensions --> IAdviser : provides extension\nmethods

```

### Scope custom attributes

```mermaid
classDiagram

ScopeAttribute <|-- CompileTimeAttribute  : derives from
ScopeAttribute <|-- RunTimeOrCompileTimeAttribute  : derives from


```

### Advice and template attributes

```mermaid
classDiagram

IAdviceAttribute <|-- ITemplateAttribute : derives from
IAdviceAttribute <| -- DeclarativeAdviceAttribute : derives from
ITemplateAttribute <|-- TemplateAttribute  : derives from
DeclarativeAdviceAttribute <|-- IntroduceAttribute  : derives from
DeclarativeAdviceAttribute <|-- IntroduceDependencyAttribute  : derives from
ITemplateAttribute <|-- InterfaceMemberAttribute  : derives from
```

### IAspectInstance, IAspectPredecessor

The <xref:Metalama.Framework.Aspects.IAspectPredecessor> facility allows aspects to access their parent (i.e. the artifact that created them): a parent aspect, a fabric, or a custom attribute.

```mermaid
classDiagram

   class AspectPredecessorKind {
       <<enum>>
       Attribute
       ChildAspect
       RequiredAspect
       Inherited
       Fabric
   }

    class IAspect {
        BuildAspect(IAspectBuilder)
    }

    class IAspectBuilder {
        AspectInstance
    }

    class  IAspectState {

    }

    class IAttribute {
        ContainingDeclaration
        Type
        Constructor
        ConstructorArguments
        NamedArguments
    }

    IAspectInstance <-- IAspectBuilder : exposes

    IAspectBuilder <-- IAspect : receives

    IAspectPredecessor <|-- IAttribute : derives from

    IAspectPredecessor <|-- IFabricInstance : derives from

    class IAspectPredecessor {

    }

    class IFabricInstance {
        Fabric
        TargetDeclaration
    }

   class IAspectInstance {
       Aspect
       AspectClass
       IsSkipped
       Procecessors
       SecondaryInstances
       State
       TargetDeclaration
   }

    IAspectState <-- IAspectInstance : exposes
    IAspect <-- IAspectInstance : exposes
    IAspectPredecessor <|-- IAspectInstance : derives from
    IAspectState <-- IAspect : reads & writes

   IAspectInstance *-- AspectPredecessor : has

   class AspectPredecessor {
       Kind
       Instance
   }

   IAspectPredecessor <-- AspectPredecessor : exposes
   AspectPredecessorKind <-- AspectPredecessor : has

```

## Namespace members

