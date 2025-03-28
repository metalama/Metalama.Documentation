---
uid: aspect-inheritance
level: 300
summary: "The document explains aspect inheritance in the context of programming. It details how aspects can be inherited from base to derived classes, and how to implement conditional inheritance. It also discusses cross-project inheritance, eligibility of inherited aspects, and provides examples. "
keywords: "aspect inheritance, derived classes, conditional inheritance, cross-project inheritance, eligibility of inherited aspects, inheritable attribute, Metalama, IConditionallyInheritableAspect, BuildAspect method, serialization"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Applying aspects to derived types

Many aspects, such as the `INotifyPropertyChanged` implementation or thread synchronization aspects, must be _inherited_ from the base class to which the aspect is applied, extending to all derived classes. This means that if a base class has a `[NotifyPropertyChanged]` aspect that adds calls to `OnPropertyChanged` to all property setters, it is logical for the aspect to also affect the property setters of the _derived_ classes.

This feature is referred to as _aspect inheritance_. It is activated by adding the <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> custom attribute to the aspect class. When an aspect is marked as _inheritable_, its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is invoked not only for the direct target declaration of the aspect but also for all derived declarations.

An aspect can be inherited in the following scenarios:

* From a base class to derived classes;
* From a base interface to derived interfaces;
* From an interface to all types implementing that interface;
* From a `virtual` or `abstract` member to its `override` members;
* From an interface member to its implementations;
* From a parameter of a `virtual` or `abstract` method to the corresponding parameter of all `override` methods;
* From a parameter of an interface member to the corresponding parameter of all its implementations.

## Example

The following type-level aspect is applied to a base class and is implicitly inherited by all derived classes.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/InheritedTypeLevel.cs name="Type-level inherited aspect"]

## Conditional inheritance

The <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> custom attribute causes _all_ instances of the aspect class to be inheritable, irrespective of their fields or properties. If you wish to base the inheritance decision on fields or properties of the aspect, your aspect must implement the <xref:Metalama.Framework.Aspects.IConditionallyInheritableAspect>.

Note that when the <xref:Metalama.Framework.Aspects.IConditionallyInheritableAspect> interface is implemented, the refactoring menu will always suggest adding the aspect to a declaration, even if the aspect is eligible for inheritance only on the target declaration.

## Cross-project inheritance

Aspect inheritance also operates across project boundaries, even when the base class is in a different project than the derived class.

To facilitate this, the aspect instance in the project containing the base class is _serialized_ into a binary buffer and stored as a managed resource in the assembly. When compiling the project containing the derived class, the aspect is _deserialized_ from the binary buffer, and its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method can be invoked.

Serialization uses a custom formatter whose semantics closely resemble the legacy <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> of the now obsolete `[Serializable]`. To mark a field or property as non-serializable, use the <xref:Metalama.Framework.Serialization.NonCompileTimeSerializedAttribute> custom attribute. For details, see <xref:aspect-serialization>.

## Eligibility of inherited aspects

The _eligibility_ of an aspect is a set of rules defining which target declarations an aspect can be legitimately applied to. For details, see <xref:eligibility>.

When an aspect is inherited, it has two sets of eligibility rules:

* The _normal_ eligibility rules define on which declarations the aspect can be expanded; typically, this would _not_ include any abstract members;
* The _inheritance_ eligibility rules define which declarations the aspect can be added to _for inheritance_; typically, this would include abstract members.

When an inherited aspect is added to a target that matches the inheritance eligibility rules but not the normal eligibility rules, an _abstract_ aspect instance is added to that target. That is, the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is _not_ invoked for that target, but only for derived targets.

To define the eligibility rules that do not apply to the inheritance scenario, use the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method and the <xref:Metalama.Framework.Eligibility.EligibilityExtensions.ExceptForInheritance*> method.

### Example

The following implementation of <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> specifies that the aspect will be applied abstractly when applied to an abstract method. Its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method will not be invoked for the abstract method but only for methods implementing the abstract method.

```cs
public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
{
    builder.ExceptForInheritance().MustNotBeAbstract();
}
```

