---
uid: diagnosing-memory-leaks
level: 200
summary: "This article explains how to find out which of your compile-time objects keeps a project snapshot in memory, causing the IDE to grow while you edit, using the MetalamaDiagnoseMemoryLeaks MSBuild property."
keywords: "memory leak, design time, Visual Studio memory, project snapshot, fabric, inheritable aspect, MetalamaDiagnoseMemoryLeaks, LAMA0085, LAMA0086, durable reference, IDurableRef, ToDurable, ToSerializableId"
created-date: 2026-08-04
modified-date: 2026-08-04
---

# Diagnosing memory leaks caused by compile-time code

An IDE that uses hundreds of megabytes on a large solution is normal, and its memory rising while you work is normal too. What is not normal is memory that **keeps rising for as long as you keep editing, without ever reaching a plateau**, and that a garbage collection does not bring back down. If you observe that on a solution that uses Metalama, the cause may be in your own compile-time code. This article explains why that happens and how to find the field responsible.

## Why compile-time code can retain memory

When you build from the command line, the compiler handles one snapshot of the project and exits, so nothing that compile-time code keeps in memory matters.

The IDE works differently. The Roslyn analysis process stays alive for as long as the solution is open, and it produces a **new snapshot of the project on essentially every keystroke**. It releases the previous one as soon as every component that received it does. A single retained snapshot keeps alive every syntax tree of the project, the full text of every file, and the symbol tables built from it: tens of megabytes on a medium project.

Metalama does not run your compile-time code on every keystroke. It runs it once and reuses the result:

* A **fabric** runs once per pipeline configuration, and everything it registers is reused for every later version of the project, until you change compile-time code.
* An **inheritable aspect instance**, a **reference validator** and an **annotation** are filed under the path of the document they belong to, and are reused for every later version in which that document did not change.

This is a deliberate design: recompiling your compile-time code between two keystrokes would be far too slow. The consequence is that any object of yours held by one of those results outlives the project snapshot it was created from. If one of its fields holds a declaration, such as an <xref:Metalama.Framework.Code.INamedType>, that whole version of the project can never be released.

This is what produces the curve without a plateau. One retained snapshot costs what is described above; a field that keeps *accumulating* declarations retains one more snapshot for every edit, so the total grows for as long as the session lasts.

The typical shapes are:

* a field of a fabric, or a variable captured by a lambda passed to `Select`, `Where` or `AddAspect`, that accumulates the declarations the query visits;
* a static field of a compile-time class used as a cache;
* a field of an inheritable aspect marked `[NonCompileTimeSerialized]` that holds a declaration.

## Investigating the memory leak

Build the project once from the command line, passing the `MetalamaDiagnoseMemoryLeaks` property:

```powershell
dotnet build MyProject.csproj -p:MetalamaDiagnoseMemoryLeaks=true
```

Metalama then inspects the objects your compile-time code left behind and reports every reference that keeps a project snapshot alive.

This is a one-time investigation, so pass the property on the command line rather than setting it in your project file. The analysis walks the whole object graph of everything your compile-time code registered, which takes time and memory, and it would slow down every build of every developer on the project.

> [!NOTE]
> The diagnostic runs during a **normal command-line build**, not in the IDE. Nothing is leaking during that build, because the compiler exits when it finishes. What the build reports is the *shape* of what your code left behind, and that shape is the same in both cases, so a command-line build tells you what an editing session would retain. Running the analysis inside the IDE instead would add its cost to the very process it is meant to protect.

## Reading the report

Each retention in your own code is reported as warning `LAMA0085`, which names the type that holds the reference, the type of the object being held, and the chain of fields that leads from a long-lived object to it:

```text
warning LAMA0085: The compile-time type 'MyFabric' holds a reference to a 'SourceNamedType',
which pins the Roslyn compilation. (...) The chain of references is:
fabric contributor #0 (AspectQuerySource<IDeclaration>) -> _query -> Owner -> _fabricInstance
-> Driver -> Fabric -> _seen -> _items -> [0].
```

Read the chain from left to right. It starts at an object that Metalama keeps for the lifetime of the project and ends at the object that cannot be released. The part you can act on is the segment inside your own types, here `_seen`, a `List<INamedType>` field of `MyFabric`.

A summary is reported as warning `LAMA0086`:

```text
warning LAMA0086: The analysis of the references retained by compile-time code found
3 retention(s) in code written by the user and 25 retention(s) in Metalama itself. (...)
The full report is in '%TEMP%\Metalama\FabricRetentionReports\MyProject-net8.0.txt'.
```

Findings are split in two:

* Retentions in **code written by you**, which is what `LAMA0085` reports and what you can fix.
* Retentions in **Metalama itself**, which are only counted. Report these to us through [GitHub](https://github.com/metalama/Metalama/issues) if you wish, but they are not something you can act on, and a non-zero count is normal.

The report file named by `LAMA0086` contains both categories, with the full chain for each, formatted one field per line.

## References, durable and otherwise

An <xref:Metalama.Framework.Code.IRef> identifies the same declaration across project snapshots, which is why the API recommends it for passing declarations between aspects. That recommendation concerns a single pipeline run, and it comes with a distinction that matters here.

A reference is either **durable** or not, as reported by <xref:Metalama.Framework.Code.IRef.IsDurable>:

* A **durable** reference, of type <xref:Metalama.Framework.Code.IDurableRef> or <xref:Metalama.Framework.Code.IDurableRef`1>, stores only a string identifier and holds nothing else. It is safe in an object of any lifetime, and it is what Metalama itself stores in its own long-lived objects.
* Any **other** reference, such as one returned by <xref:Metalama.Framework.Code.IDeclaration.ToRef*>, holds the symbol and the project snapshot behind it. It is correct and fast within a run, and it retains a snapshot as soon as you store it in something that outlives the run.

The diagnostic reports the second kind and never the first.

## Fixing a retention

### Do not store declaration objects

Never keep an <xref:Metalama.Framework.Code.IDeclaration>, an <xref:Metalama.Framework.Code.INamedType>, an <xref:Metalama.Framework.Code.IType>, a `SemanticModel` or a Roslyn `ISymbol` in anything that outlives a single pipeline run: a field of a fabric, a field of an inheritable aspect, a static field, or a variable captured by a lambda you register. Each of them is bound to the project snapshot it came from and keeps that snapshot alive.

The same applies to a plain <xref:Metalama.Framework.Code.IRef`1> obtained from <xref:Metalama.Framework.Code.IDeclaration.ToRef*>. A reference resolves in any project snapshot, which makes it the right way to *pass* a declaration between aspects, but it holds the symbol and the snapshot behind it, so it is not the right way to *store* one.

### Store a durable reference instead

Call `ToDurable()` on the reference before storing it, and resolve it later with `GetTarget( compilation )` exactly as you would resolve any other reference. A durable reference holds only a string identifier. Resolving one costs an identifier lookup, which is why references are not durable by default.

**Type the field <xref:Metalama.Framework.Code.IDurableRef`1>, not <xref:Metalama.Framework.Code.IRef`1>.** The conversion can then no longer be forgotten, because the compiler asks for it at every call site, and a reader of your fabric learns the constraint from its signature rather than from a comment.

### Generic types and cross-project references

* A durable reference is backed by a *declaration* identifier, so a constructed generic type such as `Base<int>` resolves back to `Base<T>` and its type arguments are lost silently. Where the value is a type whose shape you do not control, store its full name instead and match on that.
* When the value has to cross a process or a project boundary, store the <xref:Metalama.Framework.Code.SerializableDeclarationId> returned by `ToSerializableId()` and resolve it with `compilation.Factory.GetDeclarationFromId( id )`.

When you only need to recognize a declaration later rather than to use it, storing its full name or its file path is usually enough and is always safe.

## Limitations

* Compile-time code that behaves differently in the IDE, by testing `IExecutionScenario.IsDesignTime`, is not covered: a command-line build cannot reach that branch.
* The analysis reads the static fields of your compile-time assemblies, which runs the static constructors of the types that declare them. This is why it is opt-in.
* A field of an inheritable aspect that is **not** marked `[NonCompileTimeSerialized]` is already checked, more strictly, by the compile-time serializer: it reports an error rather than a warning when the field holds a declaration. This diagnostic covers the fields that the serializer skips.

> [!div class="see-also"]
> <xref:configuration>
> <xref:msbuild-properties>
> <xref:fabrics>
> <xref:creating-logs>
> <xref:process-dump>
