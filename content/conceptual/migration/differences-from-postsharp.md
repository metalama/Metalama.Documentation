---
uid: differences-from-postsharp
summary: "The document compares the architectural differences between Metalama and PostSharp, focusing on their operation, aspect execution, and aspect inheritance. It also discusses the implications of these differences."
keywords: "Metalama vs PostSharp, PostSharp vs Metalama, comparison, differences"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Differences between Metalama and PostSharp

This article outlines the major architectural differences between Metalama and PostSharp. The content is presented in a theoretical style and may not be essential for first-time readers.

## Metalama is a compiler add-in

A key distinction between Metalama and PostSharp lies in their operation. PostSharp functions as a post-compiler, a process that runs after the compiler to post-process the compiler's output. Metalama works as a compiler add-in and operates both at design and compile time.

Metalama executes aspects by creating a sub-project from your main project, which contains only compile-time code such as aspects, fabrics, and their dependencies. This sub-project is compiled and executed at design or compile time.

PostSharp loads the entire project (compiled as an assembly) in the .NET runtime. Metalama loads only the sub-project that includes compile-time code.

### Illustrations

#### PostSharp Architecture

```mermaid

flowchart LR

  IDE --> Compiler --> input_dll>binary] --> PostSharp --> output_dll>Binary With Aspects] --> Execute

  subgraph design-time
    IDE
  end

  subgraph compile-time
    Compiler
    PostSharp
    input_dll
  end

  subgraph run-time
    Execute
  end

```

#### Metalama Architecture

```mermaid

flowchart LR

  IDE --> Compiler --> output_dll>Binary With Aspects] --> Execute
  IDE <--> Metalama2
  Metalama2 <--> compiledAspects
  Compiler <--> Metalama
  Metalama <--> compiledAspects>Compiled Aspects]

  subgraph compile-time
    Compiler
    Metalama[Compile-Time\nMetalama]
  end

  subgraph design-time
    IDE
    Metalama2[Design-Time\nMetalama]
  end

  subgraph run-time
    Execute
  end

```

## Metalama aspects are compile-time-only

In PostSharp, aspect classes are instantiated at compile time, serialized, stored as a managed resource in the assembly being built, then deserialized at run time and executed. Therefore, in PostSharp, some aspect code is executed at compile time and some at run time.

In contrast, Metalama aspects are never executed at run time. Aspects provide code templates, and these templates are expanded at compile time. The templates generate C# code when the advice is applied—only this generated code is executed at run time.

### Illustration

#### Aspect lifetime in PostSharp

```mermaid
flowchart LR

   instantiated[aspect instantiated] --> executed[aspect executed] --> serialized[aspect serialized] --> deserialized[aspect deserialized] --> runTimeExecuted[aspect executed]

   executed -->|generates| code>code advised\nwith aspect bindings]
   code --> deserialized


   subgraph compile-time
      instantiated
      executed[executed]
      serialized
    end

   subgraph run-time
      code
      deserialized
      runTimeExecuted
    end

```

#### Aspect lifetime in Metalama

```mermaid
flowchart LR

    instantiated --> executed  -->|generates| code>code advised\nwith inlined templates]

    subgraph compile-time
      instantiated
      executed
    end
    subgraph run-time
        code
    end

```

### Implications

The difference in aspect lifetime has significant implications for how aspects are designed.

* **Metalama templates should generate succinct code.** In PostSharp, advice methods could be long and complex because they were independent C# methods, compiled and JIT-compiled just once, and executed at run time. In Metalama, advice methods are templates. The template implementation can be long, but the code they generate should be short. This code must be compiled and JIT-compiled as often as the aspect is applied, potentially thousands of times. Any logic that may repeat itself should be moved into run-time helper classes.

* **Aspects can no longer hold run-time state.** In PostSharp, aspect fields could hold any run-time state required by the aspect. In Metalama, if an aspect needs run-time state, it must _introduce_ a field into the target class (see <xref:introducing-members>).

## Aspect instances in Metalama can be shared by several declarations

Some aspects are applied to a declaration in a project but affect other projects that reference the main project (as a project or as a package). For instance, an aspect may be applied to a base class in a project. If this aspect is inheritable, it's automatically applied to all classes derived from this base class. For details, see <xref:aspect-inheritance>.

The implementation of inheritance differs between Metalama and PostSharp.

In PostSharp, each inherited aspect instance is instantiated again from the custom attribute from which it originates (to be exact, it's deserialized from the custom attribute). This mechanism is used for intra-project inheritance and cross-project inheritance.

In Metalama, the mechanism differs inside a project and across projects.

Inside a project, the same aspect instance is _shared_ among all declarations that inherit this aspect. This is why aspect classes should be written in an immutable style.

For cross-project inheritance or validators, inheritable aspect instances are _serialized_ and stored as a managed resource using the <xref:Metalama.Framework.Serialization> namespace into the assembly being built. In child projects, one new aspect instance is created by deserializing the serialized aspect of the base declaration. This deserialized instance is then shared by all derived declarations inheriting the aspect.

### Illustrations

#### Cross-project aspects in PostSharp

```mermaid
flowchart BT

  subgraph BaseAssembly

    subgraph BaseClass
    BaseAspect
    CustomAttribute[Aspect Custom Attribute]
     BaseAspect -->|instantiated from| CustomAttribute
    end

    subgraph DerivedClass1
    DerivedAspect1
    end


    DerivedClass1 -->|inherited from| BaseClass
    DerivedAspect1 -->|instantiated from| CustomAttribute

  end

  subgraph DependentAssembly

    subgraph DerivedClass2
    DerivedAspect2
    end

    subgraph DerivedClass3
    DerivedAspect3
    end

   DerivedAspect2 ----> |instantiated from| CustomAttribute
   DerivedAspect3 --> |instantiated from| CustomAttribute
  end

  DependentAssembly --> BaseAssembly

```

#### Cross-project aspects in Metalama

```mermaid
flowchart BT

  subgraph BaseAssembly

    subgraph BaseClass
    BaseAspect
    CustomAttribute[Custom Attribute]
     BaseAspect -->|instantiated from| CustomAttribute
    end

    subgraph DerivedClass1
    DerivedAspect1
    end

    subgraph ManagedResource
    SerializedAspect(Serialized\nAspect) -->|serialized from| BaseAspect
    end


    DerivedAspect1 -->|reuses| BaseAspect


  end

  subgraph DependentAssembly

    subgraph Shared
      DeserializedAspect[Deserialized\nAspect] -->|deserialized from| SerializedAspect
    end

    subgraph DerivedClass2
    DerivedAspect2 --> |reuses| DeserializedAspect
    end

    subgraph DerivedClass3
    DerivedAspect3 --> |reuses| DeserializedAspect
    end


  end

  DependentAssembly --> BaseAssembly

```

### Implications

* **In Metalama, aspect classes must be written in an immutable style.** Since aspect instances may be reused among several declarations, they can't store state that's specific to a target declaration. For details, see <xref:sharing-state-with-advice>.

> [!div class="see-also"]
>
> * <xref:benefits-over-postsharp>
> * <xref:migration-feature-status>
> * <xref:migrating-aspects>
> * <xref:templates>
> * <xref:aspect-inheritance>
> * <xref:sharing-state-with-advice>
> * <xref:introducing-members>
