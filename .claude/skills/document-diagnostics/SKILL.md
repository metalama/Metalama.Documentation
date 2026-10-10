---
name: document-diagnostics
description: Documents Metalama error and warning codes (LAMA####) in the Metalama.Documentation repository, and reviews the diagnostic messages in the Metalama sources. Each diagnostic gets its own article under content/errors with two snapshot-tested examples, one reporting the diagnostic and one fixed, plus a review of its message that proposes a correction when the text is imprecise or incorrect. Works in batches: parallel writer subagents draft several diagnostics, then a single serialized build verifies them all. Use when asked to document LAMA error codes, warnings, or diagnostics, to review or correct diagnostic messages, to extend or fix the error reference, or to continue the error-documentation backlog.
---

# Documenting Metalama diagnostics

This skill has two objectives for every `LAMA####` diagnostic:

1. **Document it** in the **Errors and warnings** reference (`content/errors/`).
2. **Validate its message and title** in the Metalama sources, and correct them when they're imprecise or incorrect. Researching a diagnostic for its article reveals exactly when Metalama reports it, which is the knowledge needed to judge whether the message tells the truth. The review also surfaces Metalama bugs, such as a crash right after the diagnostic is reported (LAMA0105).

Every diagnostic gets:

| Artifact | Path |
|---|---|
| Article | `content/errors/lama####.md` (uid `lama####`) |
| Example that reports the diagnostic | `code/Metalama.Documentation.SampleCode.Errors/LAMA####/LAMA####_Error.cs` (+ `LAMA####_Error.*.cs` auxiliaries) |
| Example where the problem is fixed | `code/Metalama.Documentation.SampleCode.Errors/LAMA####/LAMA####_Fixed.cs` (+ `LAMA####_Fixed.*.cs` auxiliaries) |
| Snapshots (generated, then committed) | `LAMA####_Error.t.cs`, `LAMA####_Fixed.t.cs` |
| Message review (committed) | `code/Metalama.Documentation.SampleCode.Errors/MessageReview/LAMA####.json`: verdict `ok` or `correct`, corrected message and title, rationale, Metalama bug if any |

Articles are grouped by **category** (templates, advising, caching, and so on), defined in `scripts/Categories.psd1`. The table of contents is `Errors and warnings > <category> > LAMA####`, and each category has a generated landing page `content/errors/errors-<key>.md` that lists its diagnostics. The inventory computes each diagnostic's category (`DocCategory`) from its definition, and the article stores it in its `diagnostic-category` front matter. To add or split a category, edit `Categories.psd1` and rerun `Update-ErrorIndex.ps1`.

The test project is a Metalama aspect-test (snapshot) project in `Metalama.Documentation.Snippets.TestBased.sln`. Diagnostics that can't be reproduced in a snapshot test get an article without samples, marked `diagnostic-sample: none` (see [references/conventions.md](references/conventions.md#doc-only-articles)).

`content/errors/lama0037.md` and its samples are the reference example. Read them before your first batch.

## Why batches

Building the test project is the expensive part of the loop, and two builds of the same project can't run at the same time (they share `obj/`). Writing a sample, by contrast, is cheap and independent per diagnostic. So:

* **Writers run in parallel and never build.** One `diagnostic-doc-writer` subagent per diagnostic researches it and writes its article and samples.
* **The coordinator (you) builds once per batch.** `Invoke-DiagnosticBatch.ps1` runs all the batch's tests in one `dotnet test`, accepts the snapshots, and verifies every diagnostic. It holds a machine-wide mutex, so a concurrent invocation waits instead of corrupting the build.
* **Failures go back to their writer**, which still has its research in context, and the next build covers only the failed diagnostics.

Never build or test per diagnostic, and never let a writer build.

## Scripts

All scripts are PowerShell 7 and live in `scripts/` (run them from the repository root):

| Script | Purpose |
|---|---|
| `Get-DiagnosticInventory.ps1` | Parses all `LAMA####` definitions from the Metalama sources and reports their status (`todo`, `partial`, `done`, `doc-only`, `excluded`). `-Status todo -First 10` picks a batch; `-Id LAMA0037` prints the full details of an ID. Writes `artifacts/diagnostics/inventory.json`. |
| `Invoke-DiagnosticBatch.ps1 -Id A,B,C` | Builds and tests the batch once, accepts the snapshots, verifies each ID, and writes `artifacts/diagnostics/last-batch.json` and `last-batch.log`. `-NoBuild` only re-verifies. `-AllowExtra LAMA0263` tolerates an unavoidable companion error. |
| `Update-ErrorIndex.ps1` | Regenerates the grouped `content/errors/toc.yml`, the category pages `content/errors/errors-<key>.md`, and the category table in `content/errors/errors.md` from the articles' front matter. Never edit these files by hand. |
| `Categories.psd1` | The categories, in table-of-contents order, and how source categories map to them. |
| `Set-DiagnosticMessage.ps1` | Applies the reviewed corrections to a dedicated Metalama worktree: the `DiagnosticDefinition` texts and the formatted messages in Metalama's `.t.cs` snapshots. Reports what needs a manual edit. `-WhatIf` previews. |
| `Get-MessageProposals.ps1` | Summarizes the writers' message proposals into `artifacts/diagnostics/message-review.md`: current and proposed texts, rationales, required code changes, and the Metalama bugs found. Read-only. |
| `DiagnosticParsing.ps1` | The parser of `DiagnosticDefinition` declarations, shared by the other scripts. |

## Prerequisites (once per session)

1. **The build environment is prepared.** If the repository root has no `global.json`, run `./build.ps1 prepare` (about 30 seconds). Without it, every `dotnet` command fails with `The SDK 'PostSharp.Engineering.Sdk' specified could not be found`.
2. **The Metalama sources match the documentation branch.** The scripts need the `Metalama` and `Metalama.Premium` repositories. By default they look for `../Metalama` next to this repository. Otherwise, set `$env:METALAMA_SOURCE_ROOT` (and `$env:METALAMA_PREMIUM_SOURCE_ROOT` if Premium isn't a sibling) or pass `-MetalamaRoot`. Use the checkout for the same version as the documentation branch (for example `release/2026.1`), because diagnostics are added and renumbered between versions.
3. **A dedicated Metalama worktree receives the message corrections.** Never apply corrections to a checkout someone works in. Ask the user once for the GitHub issue that tracks the message corrections (or whether to create one), then create the worktree from the version branch and point `$env:METALAMA_MESSAGES_WORKTREE` to it:

   ```powershell
   git -C <Metalama> worktree add <Metalama>/.claude/worktrees/diagnostic-messages -b topic/2026.1/<issue>-diagnostic-messages release/2026.1
   ```

   Writers keep reading the sources from the regular checkout (step 2). The inventory reads from there too, so the inventory shows the uncorrected messages until the corrections are merged. That's expected: the proposals are the record of what changed.

## Workflow

### 1. Pick a batch

```powershell
./.claude/skills/document-diagnostics/scripts/Get-DiagnosticInventory.ps1 -Status todo -First 40
```

Pick **8 to 12** IDs. Prefer IDs from the same category (the `DocCategory` column, for example all `templates`, or one pattern library): writers then read the same sources and conceptual articles, and the batch's samples are similar, so mistakes repeat less. Mix in a few likely doc-only IDs (licensing, unexpected exceptions, MSBuild configuration) if convenient; they cost no build time.

Also run the inventory with `-Status partial` and finish those first: they're leftovers of an interrupted batch, or articles written before message review existed (no `message-review` front matter). For the latter, the writer only reviews the message, writes the proposal, and updates the article's front matter and **Message** row.

### 2. Fan out the writers

Spawn one `diagnostic-doc-writer` subagent per ID, **all in a single message** so they run concurrently. Give two or three closely related IDs (for example a warning and its error variant, such as LAMA5020 and LAMA5021) to the same writer. Run them in the foreground of your turn: you need all their reports before building.

Each prompt must contain:

* The ID or IDs, and the output of `Get-DiagnosticInventory.ps1 -Id <ID>` (severity, category, symbol, title, message, defining file).
* The Metalama source root you resolved in the prerequisites.
* The instruction to follow `.claude/agents/diagnostic-doc-writer.md` and `.claude/skills/document-diagnostics/references/conventions.md`, and to not build.

Name each agent after its ID (for example `writer-LAMA0104`) so that you can message it in step 5.

For large runs, write the shared part of the prompt once to `artifacts/diagnostics/writer-brief.md` (paths, rules, and the lessons learned so far) and give each writer only the brief's path and its IDs. Add each new lesson to the brief as soon as a build reveals it. Four IDs per writer and up to a dozen writers per batch worked well. Don't start the next batch's writers while a build is running: a half-written sample breaks the shared build.

### 3. Integrate

When all writers have reported:

1. Check the reports for IDs the writer classified as doc-only, and skim their reason. Push back if the reason is weak; most diagnostics reported by aspects, templates, advice, fabrics, eligibility, validation, and the pattern libraries **are** reproducible.
2. Run `Update-ErrorIndex.ps1`. Fix any missing `short-description`, invalid `diagnostic-category`, or wrong `uid` it reports.
3. Run `Get-MessageProposals.ps1 -Id <batch>` and review every proposed correction in `artifacts/diagnostics/message-review.md`. Reject a correction (ask the writer to set the verdict to `ok`, or to fix the text) when it:
   * rewrites a correct message for taste;
   * narrows the message to the writer's sample while other report sites exist;
   * uses contractions or another documentation-style habit (Metalama messages follow the Metalama repository style, see the conventions);
   * introduces a placeholder that the current message doesn't have, without a `codeChange`.

   Verify each reported Metalama bug against the cited source lines before you report it to the user.

Writers only touch their own `LAMA####/` folder and `lama####.md` article. Only you edit shared files: `toc.yml`, `errors.md`, the `errors-<key>.md` category pages (through the script), `Categories.psd1`, the project file, the solution, and the conceptual articles. That's what makes parallel writers safe.

### 4. Build and verify once

```powershell
./.claude/skills/document-diagnostics/scripts/Invoke-DiagnosticBatch.ps1 -Id LAMA0104,LAMA0105,...
```

Pass every ID of the batch, including doc-only ones (their articles are checked too). On the first run of a new sample, the test itself fails because no snapshot exists yet. That's expected: the script accepts the output and then verifies the accepted snapshot, which is what matters.

Results:

* **Exit code 2, `BUILD FAILED`**: the project doesn't compile as plain C#. The test project is built with Metalama disabled and compiles **all** samples together, so one bad file breaks the whole batch. The printed `error CS####` lines give the file, hence the ID. Typical causes: a duplicate namespace or type, an intentional C# error in a sample, or a missing `using`. Send the error to the owning writer, or fix a trivial one yourself, then rebuild.
* **Per-ID `FAIL`**: the problems list what's wrong, including missing message proposals and an article **Message** row that doesn't match the reviewed message, for example `LAMA0104_Error does not report LAMA0104; it reports: Error LAMA0263`, or `LAMA0104_Fixed still reports: Error CS0103`.
* **`has warnings (check they are intended)`** on a `_Fixed` sample: decide whether the warning belongs in the example. Usually it doesn't (nullable and obsolete-API warnings are the common cases). When it does, for example a user diagnostic that the fixed sample reports on purpose, pass its ID with `-AllowExtra`.
* **An ID converted to doc-only after a build**: the writer deletes its folder. The script removes the stale output from `obj/transformed` before accepting, and fails any doc-only ID whose folder still exists.

### 5. Iterate on failures

For each failed ID, send its writer a message (`SendMessage` to `writer-LAMA####`) containing the problems from the script and the contents of the accepted `LAMA####_Error.t.cs` and `LAMA####_Fixed.t.cs`. The writer fixes its files without building. When all fixes are in, rebuild **only the failed IDs** in one call.

Limit this to three rounds per ID. After the third failed round, either:

* convert the article to doc-only if the diagnostic turns out to be unreproducible in a snapshot test, or
* remove the ID's folder and article, rerun `Update-ErrorIndex.ps1`, and report the ID as not done with the last problem list.

Don't loosen the verification to get a pass. In particular, an `_Error` sample that reports the target ID plus other errors is misleading to readers. Use `-AllowExtra` only when the companion diagnostic is inherent to the situation, and make sure the article mentions it.

### 6. Review the batch

The script verifies the mechanics, not the content. Before reporting, for each ID:

* Read `LAMA####_Error.t.cs`: the diagnostic message must match what the article's **Cause** section says.
* Read `LAMA####_Fixed.t.cs`: the transformed code must show the aspect working, and the fix must be the one the **Resolution** section describes.
* Check that every `<xref:...>` target exists: conceptual uids with `grep -r "^uid: <uid>$" content`, API uids in `artifacts/api/*.yml` (or, if API docs aren't built, in the Metalama sources), and other `lama####` uids in `content/errors`. Remove an xref to a diagnostic that isn't documented yet.

For larger batches, you can run the `docs-style-reviewer` agent on the batch's articles. Don't run `update-html.ps1` or `Build.ps1` per batch; they're slow and the snapshot verification is sufficient.

### 7. Apply the message corrections to Metalama

Apply the corrections you accepted in step 3 to the dedicated Metalama worktree (see the prerequisites), once per batch:

```powershell
./.claude/skills/document-diagnostics/scripts/Set-DiagnosticMessage.ps1 -MetalamaRoot $env:METALAMA_MESSAGES_WORKTREE -WhatIf
./.claude/skills/document-diagnostics/scripts/Set-DiagnosticMessage.ps1 -MetalamaRoot $env:METALAMA_MESSAGES_WORKTREE
```

The script edits the `DiagnosticDefinition` and rewrites the formatted message in the Metalama `.t.cs` snapshots. It refuses to run on a main checkout or on a branch that isn't `topic/...`. Then handle its output:

* `SKIPPED` because of a new placeholder or an interpolated string: apply the change by hand in the worktree, including the `codeChange` at the report sites, or drop the correction. Then set `"applied"` in the proposal. When editing an interpolated message by hand, keep its `nameof(...)` expressions where they render the proposed text, so that the message follows renames. Then rewrite the snapshot lines of that ID with the same old-to-new placeholder mapping that the script uses, and check that lines from another definition of the same ID (duplicate IDs exist) are left alone.
* `MANUAL: snapshot line does not match the old format`: fix that snapshot line by hand. The line was probably produced by another report site with a different text.
* `MANUAL: test code contains the old text`: update the assertion in that test.
* Premium diagnostics are skipped unless you pass `-PremiumRoot` with a dedicated Metalama.Premium worktree.

Never build or test the Metalama worktree per batch. Don't commit in it unless the user asked. At the end of the whole run, tell the user that the worktree needs a Metalama build and test run (`Build.ps1 test` in the worktree, which is long) before a pull request, and list the `MANUAL` items you fixed.

When a Metalama release that includes corrections is used by the documentation, the `_Error` snapshots of the corrected IDs change: rerun `Invoke-DiagnosticBatch.ps1` for the IDs whose article has `message-review: corrected`, and check the accepted snapshots.

### 8. Report

Summarize for the user: the IDs completed with samples, the IDs completed as doc-only (with reasons), the IDs abandoned (with their last problems), the proposed message corrections and the Metalama bugs found (point to `message-review.md`), and the overall inventory status from `Get-DiagnosticInventory.ps1`. Commit only if the user asked, following the repository's commit conventions.

## Pitfalls learned the hard way

* **Test names are file names.** The test framework names each test after its file, without the directory, so `dotnet test --filter "DisplayName~LAMA0104_"` selects one ID. Prefixed file names (`LAMA0104_Error.cs`, not `Error.cs`) keep filters and generated HTML ids unique.
* **A dot in a test file name means auxiliary file.** `LAMA0104.Error.cs` would be treated as an auxiliary of `LAMA0104.cs`. Use an underscore before `Error` and `Fixed`.
* **An `_Error` snapshot with an error contains only the diagnostics**, no transformed code, because Metalama doesn't transform a compilation with errors. A warning snapshot contains both the warning and the transformed code.
* **Info and hidden diagnostics aren't in snapshots** unless the test file has `// @IncludeAllSeverities` (see the conventions).
* **Fresh snapshots are accepted automatically.** Accepting is safe only because the script verifies the result. Never skip step 6 for that reason.
