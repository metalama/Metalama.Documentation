---
uid: advising-concepts
summary: "The document explains the concept of transforming code using advice in aspect-oriented programming. It discusses two methods of adding advice: declaratively and imperatively. It also covers the use of templates."
keywords: "aspect-oriented programming, transforming code, advice, declarative advising, imperative advising, IntroduceAttribute, IAspect.BuildAspect, AdviserExtensions, template methods, compile-time"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Transforming code: concepts

Aspects can transform the target code by providing _advice_. Advice refers to a primitive transformation of code. It is safely composable, meaning that several aspects, even without knowledge of each other, can add advice to the same declaration.

> [!NOTE]
> In English, the word _advice_ is uncountable, i.e., grammatically plural. The grammatically correct singular form of _advice_ is _piece of advice_, but using these words in a software engineering text seems unusual. In aspect-oriented programming, _advice_ is a countable concept. Despite the challenges associated with using uncountable nouns as countable, we sometimes use _an advice_ for the singular form and _advices_ for the plural form, which may be occasionally surprising to some native English speakers. We use other neutral turns of phrases whenever possible unless it would make the phrase much more cumbersome or less understandable.

There are two methods to add advice: _declaratively_ and _imperatively_.

## Declarative advising

The only _declarative advice_ is the _member introduction_ advice, denoted by the <xref:Metalama.Framework.Aspects.IntroduceAttribute> custom attribute. For each member of the aspect class annotated with `[Introduce]`, the aspect framework will attempt to introduce the member in the target class. For details, refer to <xref:introducing-members>.

## Imperative advising

_Imperative advice_ is added by implementing the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, thanks to the advising extension methods exposed by the `builder` parameter implementing the <xref:Metalama.Framework.Advising.IAdviser`1> interface.

The following methods are available:

* <xref:Metalama.Framework.Advising.AdviserExtensions.Override*> allows you to replace the implementation of a method, field, property, event, or constructor.
* <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceMethod*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceProperty*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceField*>, and <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceEvent*> enable your aspect to introduce new members into the target type. Refer to <xref:introducing-members#introducing-members-programmatically> for details.
* <xref:Metalama.Framework.Advising.AdviserExtensions.ImplementInterface*> allows the target type to implement an interface. Refer to <xref:implementing-interfaces> for details.
* <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceAttribute*> and <xref:Metalama.Framework.Advising.AdviserExtensions.RemoveAttributes*> allow adding and removing custom attributes. See <xref:adding-attributes> for details.
* <xref:Metalama.Framework.Advising.AdviserExtensions.AddContract*> allows you to add a pre-condition or post-condition to a field, property, or parameter. See <xref:contracts> for details.
* <xref:Metalama.Framework.Advising.AdviserExtensions.AddInitializer*> allows you to add an initialization statement in the constructor or static constructor. See <xref:initializers> for details.
* <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceParameter*> allows you to append a parameter to a constructor and pull them from constructors of derived classes. See <xref:introducing-constructor-parameters> for details.

Please refer to the <xref:Metalama.Framework.Advising.AdviserExtensions> class for a complete list of methods.

To advise a member of the current declaration (for instance, to override a method in the current type), you can get an adviser for the member by calling the <xref:Metalama.Framework.Advising.IAdviser.With*?text=IAdviser.With> method.

> [!NOTE]
> You can only advise the target of the current aspect instance or any declaration _contained_ in this target. For instance, the `BuildAspect` method of a type-level aspect can advise all methods of the current type, including all parameters.

## Template methods

With most types of advice, you must provide a _template_ of the member you want to add to the target type.

Templates are written in standard C# code but combine two kinds of code: _compile-time_ and _run-time_. When some target code is advised, the compile-time part of the corresponding template is _executed_. The output of this execution is the run-time code, which is then injected into the source code to form the _transformed code_.

For details, refer to <xref:templates>.


