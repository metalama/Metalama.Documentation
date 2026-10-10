# Conventions for diagnostic articles and samples

These conventions apply to everything under `content/errors/` and `code/Metalama.Documentation.SampleCode.Errors/`. The reference example is LAMA0037: `content/errors/lama0037.md` and `code/Metalama.Documentation.SampleCode.Errors/LAMA0037/`.

## Researching a diagnostic

Before writing anything, establish **when exactly** Metalama reports the diagnostic. Guessing from the message alone produces samples that report something else.

1. **Definition.** `Get-DiagnosticInventory.ps1 -Id LAMA####` gives the defining file and the symbol name (for example `ScopeMismatch` in `TemplatingDiagnosticDescriptors.cs`).
2. **Report sites.** Search the Metalama sources for the symbol, for example `grep -rn "ScopeMismatch" <MetalamaRoot>/Metalama.Framework/src --include=*.cs`, excluding `tests` and `obj`. Read the code around each `.CreateRoslynDiagnostic(`, `.ReportDiagnostic(`, or `Report(` call to learn the exact condition. Several report sites often mean several causes; document the common ones.
3. **Existing reproductions.** Metalama's own aspect tests usually reproduce the diagnostic. Search the snapshots:
   * `grep -rlE "// (Error|Warning) LAMA####" <MetalamaRoot>/Metalama.Framework/src/tests --include=*.t.cs`
   * the same under `<MetalamaRoot>/Metalama.Patterns/src/tests`, `<MetalamaRoot>/Metalama.Extensions/src/tests`, and `<PremiumRoot>/src` (Premium tests sit next to the sources).
   * Skip hits under `obj/`.

   Use these tests to learn the trigger, but don't copy them: they're written to test edge cases, often report several diagnostics, and use internal names. Write a realistic, minimal scenario instead.
4. **Concepts.** Find the conceptual article that explains the feature involved (use the package-to-concept table in `CLAUDE.md`, or grep `content/`). You'll link to it, and it tells you the vocabulary to use.

## Reviewing the message

The second objective of this work, besides the article, is to make the diagnostic's own text correct. While researching, you learn exactly when Metalama reports the diagnostic. Use that knowledge to review the **message** and the **title** of the `DiagnosticDefinition`, as found in the inventory.

### Criteria

Propose a correction only when the text has a real defect:

* **Incorrect**: the text states something false about the condition. For example, it says "must not be static" when the report site checks for `virtual`, or it names the wrong declaration kind.
* **Imprecise**: the reader can't tell what's wrong or where. For example, the message doesn't name the offending declaration although it's available, or it's so generic that it fits several distinct problems.
* **Misleading or missing advice**: the suggested fix doesn't work, or the message gives no way forward when an obvious one exists.
* **Language defects**: typos, grammar errors, duplicated words ("constructor constructor"), wrong capitalization ("it Cannot"), outdated terminology ("build-time" instead of "compile-time").
* **Placeholder defects**: a placeholder is quoted inconsistently, shows a confusing value, or doesn't match what the code passes.

Don't propose a rewrite for taste alone. A correct, clear message stays as it is (verdict `ok`).

### Style of Metalama messages

Messages and titles follow the Metalama repository's rules, which **differ from the documentation style**:

* Documentation-grade: professional, rather formal, strictly grammatical, complete sentences.
* **No contractions**: "cannot", "does not", "is not". (Articles use contractions; messages never do.)
* No figurative language, no slang, no rhetorical emphasis, no uncommon abbreviations. Plain vocabulary for non-native readers.
* Name declarations and expressions in single quotes, for example `'{0}'`, as the existing messages do.
* A message is one or more complete sentences ending with a period. A title is a short sentence ending with a period that makes sense without the placeholders.
* Use `compile-time` and `run-time` (with hyphens) for the scopes.

### Constraints on a correction

* **Keep the placeholders.** The new text may reorder or drop placeholders, but it can only use placeholders that the current message already has: the values come from the report sites, which you don't change. If a good message needs a new value, describe it in `codeChange` and keep the message text within the existing placeholders.
* Keep the meaning that applies to **all** report sites of the diagnostic, not only the one in your sample.
* Never edit the Metalama sources yourself. Write a proposal instead (next section).

### Proposal file

For every ID, write `code/Metalama.Documentation.SampleCode.Errors/MessageReview/LAMA####.json`, even when the verdict is `ok`:

```json
{
  "id": "LAMA0018",
  "symbol": "CannotProvideInstanceForLocalFunction",
  "verdict": "correct",
  "message": "'{0}' is a local function, so it cannot be invoked with an instance.",
  "title": null,
  "rationale": "The current message capitalizes 'Cannot' in the middle of a sentence, and 'non-null instance' is imprecise because no instance can be provided at all.",
  "codeChange": null,
  "engineBug": null
}
```

* `verdict`: `ok` or `correct`.
* `message`, `title`: the new text, or `null` to keep the current one. Write the text as it appears at run time (no C# escaping).
* `rationale`: one or two sentences citing the defect, and the report site when the defect is about accuracy.
* `codeChange`: `null`, or a description of a change needed at the report sites (for example a missing argument).
* `engineBug`: `null`, or a description of a bug found in Metalama while researching (for example a crash after reporting the diagnostic), with the file and line.

### Effect on the article

* Add `message-review: ok` or `message-review: corrected` to the front matter.
* The **Message** row of the article shows the corrected message when the verdict is `correct`.
* In the **Message** row and everywhere else in the article, write each placeholder as a meaningful PascalCase name in angle brackets instead of `{0}`, `{1}`: for example `'<Method>' cannot be overridden because '<DeclaringType>' is sealed.` Take the name from the named tuple element of the descriptor's `DiagnosticDefinition<(...)>` type argument when it's meaningful, otherwise from what the report sites pass. Use the same name for the same placeholder throughout the article. The proposal file and the Metalama sources keep `{0}`. The snapshots keep showing the message of the Metalama package used by the documentation until a Metalama release includes the correction; that's expected.

## Reproducible or doc-only

A diagnostic is **reproducible** if a single test file (plus auxiliaries, and optionally a dependency project) can make Metalama report it. This covers nearly everything reported by templates, aspects, advice, fabrics, eligibility, the code model, serialization, validation and architecture rules, code fixes, dependency injection, aspect weavers, and the pattern libraries, including WPF.

The test project targets `net10.0-windows` with WPF, allows unsafe code, and references `Metalama.Framework.Sdk` and `Metalama.Patterns.Wpf`, so diagnostics about WPF commands and dependency properties, unsafe code, and aspect weavers are reproducible. Before declaring a diagnostic doc-only, search Metalama's own aspect tests (the `.t.cs` files under `tests`, excluding `obj`) for the ID: if one reports it, reproduce its setup, including its test options.

A diagnostic is **doc-only** if it can't be reproduced deterministically in a snapshot test, typically because it depends on:

* licensing (`LAMA08xx`), the user profile, or the machine;
* MSBuild properties, project configuration, or package versions (for example several Metalama versions in one solution);
* the IDE or design-time pipeline only;
* an unexpected exception or crash in Metalama (`LAMA0001`, `LAMA0049`), or an internal inconsistency that's only reachable through a Metalama bug;
* a check that an earlier check always preempts, for example an advice whose eligibility rule rejects the same case first;
* a Roslyn analyzer (snapshot tests don't run analyzers), or code that the C# compiler rejects.

Whether another diagnostic is reported together with it isn't a reason for doc-only: show both, and pass `-AllowExtra` to the verification script. A Metalama bug that breaks the sample is a temporary reason: say so in the article and name the issue.

When in doubt, try a sample. If three build rounds fail to reproduce it, the coordinator converts the article to doc-only.

## Samples

### Files and names

For diagnostic `LAMA####`, in `code/Metalama.Documentation.SampleCode.Errors/LAMA####/`:

| File | Content |
|---|---|
| `LAMA####_Error.cs` | The target code, for example the class to which the aspect is applied. This is the test's principal file. |
| `LAMA####_Error.Aspect.cs` | The aspect, if the sample needs one. Other auxiliaries use other suffixes: `.Fabric.cs`, `.Options.cs`, and so on. |
| `LAMA####_Error.Dependency.cs` | Only if the scenario needs a referenced project, for example for inheritance across projects. |
| `LAMA####_Fixed.cs` and `LAMA####_Fixed.*.cs` | The same scenario with the problem fixed. |

Never write `*.t.cs` files: the build generates them. Never put a dot before `Error` or `Fixed` in a file name.

### Code rules

* Start every file with the line `// This is public domain Metalama sample code.` followed by an empty line.
* Use the file-scoped namespace `Doc.LAMA####.Error` or `Doc.LAMA####.Fixed`, identical in a test's principal and auxiliary files. The test project compiles all samples together as plain C#, so namespaces must be unique per test.
* **The samples must compile as plain C#.** The project build runs with Metalama disabled; only the tests run Metalama. A `LAMA` error is fine, because the C# compiler doesn't see it, but an intentional `CS` error breaks the build of the whole batch. To demonstrate a diagnostic that Metalama reports on code that C# rejects, choose another scenario or make the diagnostic doc-only.
* Keep the `_Error` and `_Fixed` samples as close as possible: the reader should spot the fix by comparing them. Same class names, same members, same aspect, except for the fix.
* Keep the samples minimal and realistic: a `Calculator`, an `OrderService`, a `Log` or `Cache` aspect. Avoid `Foo`, `Bar`, and `TargetCode`.
* Add one short comment on the offending line of the `_Error` sample (for example `// The [Log] aspect requires an instance method.`) and one on the fixed line of the `_Fixed` sample. Comments appear in the rendered documentation.
* Use the formatting style of the repository: spaces inside parentheses of calls and declarations (`Console.WriteLine( x )`, `Add( int a, int b )`), `this.` for instance members.
* Classes that receive introduced members are `partial`.
* The `_Error` sample must report the target diagnostic and **no other error**. Warnings from other diagnostics should also be avoided.
* The `_Fixed` sample must report **no error and not the target diagnostic**. It should show the aspect doing its work in the snapshot, so that the reader sees the fix is complete.
* For a warning, both snapshots contain transformed code, and the `_Error` snapshot additionally starts with the `// Warning LAMA####` line. Name the tab `Warning` instead of `Error` in the article.

### Test options

Test options go in a block at the top of the principal file, after the header comment:

```
#if TEST_OPTIONS
// @IncludeAllSeverities
#endif
```

Options you may need:

| Option | When |
|---|---|
| `@IncludeAllSeverities` | The diagnostic has severity `Info` or `Hidden`. |
| `@RemoveOutputCode` | The `_Fixed` snapshot would be long and irrelevant. Use sparingly: showing the working output is usually valuable. |
| `@LanguageVersion(N)` / `@LanguageFeature(name)` | The diagnostic depends on a C# version or feature. |
| `@AllowCompileTimeDynamicCode`, `@RoslynIsCompileTimeOnly(false)`, `@DefinedConstant(NAME)`, `@OutputAssemblyType(...)` | The diagnostic depends on the corresponding project option. |

The test framework can't set arbitrary MSBuild properties: `IProject.TryGetProperty` returns `false` in snapshot tests.

Options that don't work in this project:

* `@RequireOrderedAspects` reports every unordered pair of aspect classes, including those of the referenced pattern libraries, so it produces dozens of unrelated errors.
* `@EnableLogging` makes the test runner fail ("An element with the same key but a different value already exists. Key: 'Metalama.Backstage.Diagnostics.ILoggerFactory'").
* The default order of aspects, and therefore anything that depends on it (such as LAMA0042), differs from Metalama's own tests, because the referenced libraries contribute aspect classes.

Because the project uses WPF, `Accessibility.dll` would hide `Metalama.Framework.Code.Accessibility`. The csproj removes that reference, so samples can write `Accessibility.Public` as usual.

Don't use `@TestScenario(CodeFix)`: the documentation test project has no code-fix runner ("Cannot find the code fix runner"). Show the result of the code fix written by hand in the `_Fixed` sample instead. Don't use `@TestScenario(DesignTime)` either: it produces no HTML, so the article can't render the sample. A diagnostic reported only at design time is doc-only.

The options are defined in `Metalama.Testing.AspectTesting/TestOptions.cs` in the Metalama sources.

## Articles

### Front matter

```yaml
---
uid: lama####
level: 200
summary: "One or two sentences: when Metalama reports LAMA#### and, briefly, the typical cause."
keywords: "LAMA####, 4 to 8 key phrases a user would search for"
created-date: YYYY-MM-DD
modified-date: YYYY-MM-DD
diagnostic-id: LAMA####
diagnostic-severity: Error
diagnostic-category: templates
message-review: ok
short-description: "One sentence for the index table, describing the problem, not the message."
---
```

* `level`: 200 by default; 300 for template and SDK diagnostics that need advanced concepts.
* `diagnostic-severity`: the severity from the inventory (`Error`, `Warning`, `Info`, `Hidden`).
* `diagnostic-category`: the `DocCategory` value from the inventory, one of the keys in `.claude/skills/document-diagnostics/scripts/Categories.psd1` (`general`, `templates`, `advising`, `serialization`, `design-time`, `licensing`, `architecture`, `dependency-injection`, `caching`, `contracts`, `immutability`, `observability`, `wpf`). It decides where the article appears in the table of contents.
* `short-description`: shown in the table of the category page. Write it as a plain statement of the problem, ending with a period, for example `A field of a type marked with [Immutable] isn't read-only.`
* Doc-only articles add `diagnostic-sample: none`.
* Use today's date for both dates.

### Body

```markdown
# LAMA####: <title stating the problem in plain words>

| | |
|---|---|
| **Severity** | Error |
| **Reported by** | <package name, for example Metalama.Framework or Metalama.Patterns.Caching> |
| **Message** | `<the message format from the inventory, with each {0} placeholder replaced by a meaningful name in angle brackets>` (use ``double backticks`` when the message contains a backtick) |

## Cause

<What the rule is and why it exists, then what triggers the diagnostic. One to three paragraphs.
Refer to the placeholders by the same names, for example `<Method>`, never as `{0}`. Explain them if they aren't obvious. If the diagnostic has several distinct causes, use a bulleted list.>

<One sentence introducing the example.>

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Errors/LAMA####/LAMA####_Error.cs name="Error"]

## Resolution

<The fixes, most common first. A bulleted list when there are several options. Then one sentence introducing the example.>

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Errors/LAMA####/LAMA####_Fixed.cs name="Fixed"]

## See also

* <xref:conceptual-article-uid>
```

Rules:

* The H1 is `LAMA####: ` followed by a short title, in sentence case, stating the problem (`The field of an immutable type must be read-only`), not a copy of the message.
* Don't write inline C# code blocks. Code belongs in the samples. MSBuild or JSON snippets are allowed in doc-only articles.
* Follow the Microsoft Writing Style Guide: second person, active voice, present tense, contractions (`can't`, `doesn't`). Use `run-time` (adjective, at program execution) and `compile-time`; `runtime` only means the CLR.
* Refer to API members with `<xref:Full.Uid>` the first time when you're sure of the UID, otherwise in backticks. For overload groups, append `*`. For inherited members, use the declaring type's UID.
* Link only to `lama####` articles that already exist. The coordinator removes others.
* Don't promise behaviors you haven't verified in the sources or the snapshot.

### Doc-only articles

Same front matter plus `diagnostic-sample: none`, and the same sections without the `[!metalama-test]` lines. Under **Cause**, add one sentence explaining why the article has no example, for example `This diagnostic depends on the license configuration of the machine, so it can't be shown in an example.` Under **Resolution**, give concrete steps (MSBuild properties, commands, where to look for the crash report).
