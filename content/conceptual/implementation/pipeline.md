---
uid: pipeline
level: 400
summary: This document outlines the steps involved in a compilation pipeline - initialization, applying aspects, and transforming the compilation. It differentiates between compile time and design time processes.
keywords: "compilation pipeline, compile-time processes, design time processes, observable transformations, non-observable transformations, code model version"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Compilation pipeline

## Step 1: Initialization

1. Generate the compile-time compilation:
    1. Identify and load referenced compile-time projects.
    2. Identify compile-time code in the current compilation and create a separate compile-time compilation.
        1. Transform templates into code generating Roslyn syntax trees.
        2. Transform `nameof` and `typeof` expressions to make them independent from run-time references.
2. Initialize aspect classes:
    1. Create a prototype instance of each aspect class using `FormatterServices.GetUninitializedObject`.
    2. Execute the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method and discover the aspect layers.
    3. Discover aspect ordering relationships in the current project and all referenced assemblies.
    4. Order aspect layers.

## Step 2: Applying aspects

For each aspect layer, in order of application (inverse order of execution, see <xref:ordering-aspects>):

* For the default aspect layer:
  * Evaluate aspect sources for this aspect type, resulting in a set of _target declarations_.
  * Visit target declarations in [breadth-first order](https://en.wikipedia.org/wiki/Breadth-first_search) of _depth level_. For each target declaration:
    * Instantiate the aspect.
    * Invoke the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method.
    * Add advice to the subsequent steps of the pipeline.

* For all aspect layers and for all target declarations visited in breadth-first order:
  * Execute advice. Advice can provide observable or non-observable transformations (or both):
    * _Observable transformations_ are transformations observable from outside, for instance, adding a new type member or adding a method parameter.
    * _Non-observable transformations_ are transformations that only affect the _implementation_ of declarations (for example, overriding a method).

* Before executing the next aspect layer or the next visiting depth, create a new code model version incrementally from the previous version, which includes all observable transformations added by advice.

## Step 3: Transforming the compilation

Before this step, the algorithm collected transformations, but the compilation was never modified.

The subsequent actions depend on whether the pipeline runs at design time or at compile time.

### Compile time

1. Introduce all transformations (observable and non-observable) into a new compilation. Expand templates during this step.
2. Link the code together and inline where possible.

### Design time

At design time, Metalama ignores non-observable transformations and creates partial files for observable transformations.
Templates are never executed at design time.

> [!div class="see-also"]
> <xref:implementation>
> <xref:aspect-composition>
> <xref:ordering-aspects>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.IAspect`1>
> <xref:Metalama.Framework.Eligibility.IEligible`1>
