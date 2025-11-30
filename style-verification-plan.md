# Style verification plan

This plan outlines the steps to verify and fix all 169 articles under `content/` against the style guidelines in CLAUDE.md.

## Verification checklist per article

### YAML front matter
- [ ] Has `uid` field
- [ ] Has `summary` field (non-empty)
- [ ] Has `keywords` field
- [ ] Has `created-date` field
- [ ] Has `modified-date` field
- [ ] Conceptual articles have `level` field (200, 300, or 400)

### Title and headings
- [ ] Title uses sentence case (not Title Case)
- [ ] Title uses "-ing" verb form where appropriate (problem-oriented)
- [ ] All headings use sentence case

### Structure
- [ ] Starts with introductory paragraph
- [ ] Has "Benefits" section where applicable
- [ ] Ends with "See also" section using `> [!div class="see-also"]` format
- [ ] Index files have table describing child pages

### Code samples
- [ ] Uses external files, not inline Markdown code blocks
- [ ] Uses `[!metalama-test ...]` for complete examples with pre/post comparison
- [ ] Uses `[!metalama-file ...]` for code snippets from external files
- [ ] Examples are relevant to context

### Formatting
- [ ] Uses `-` for unordered lists (not `*`)
- [ ] Uses 4-space indentation for nested lists
- [ ] One blank line between sections (not multiple)
- [ ] Uses Oxford comma in lists

### Cross-references
- [ ] API links use `<xref:...>` format
- [ ] Article cross-references use `<xref:article-uid>`
- [ ] Method references include asterisk

### Code and terminology
- [ ] Uses official C# terminology
- [ ] Code elements in backticks
- [ ] Uses contractions appropriately
- [ ] No filler phrases ("you can", "there is")

## Articles by directory (169 total)

### content/api/ (10 files)
- [ ] advanced-api.md
- [ ] api.md
- [ ] aspect-api.md
- [ ] code-api.md
- [ ] extensions-api.md
- [ ] flashtrace-api.md
- [ ] introspection-api.md
- [ ] migration-api.md
- [ ] patterns-api.md
- [ ] testing-api.md

### content/conceptual/architecture/ (6 files)
- [ ] architecture.md
- [ ] experimental.md
- [ ] extending.md
- [ ] internal-only-implement.md
- [ ] naming-conventions.md
- [ ] usage.md

### content/conceptual/aspects/advising/ (14 files)
- [ ] advising-concepts.md
- [ ] advising.md
- [ ] attributes.md
- [ ] contracts.md
- [ ] implementing-interfaces.md
- [ ] initializers.md
- [ ] introducing-constructor-parameters.md
- [ ] introducing-members.md
- [ ] introducing-types.md
- [ ] overriding-constructors.md
- [ ] overriding-events.md
- [ ] overriding-methods.md
- [ ] overriding-properties.md
- [ ] sharing-state.md

### content/conceptual/aspects/architecture/ (1 file)
- [ ] architecture.md

### content/conceptual/aspects/configuration/ (5 files)
- [ ] before-2023-4.md
- [ ] configuration.md
- [ ] customizing-merge.md
- [ ] exposing-options.md
- [ ] msbuild-properties.md

### content/conceptual/aspects/ide/ (3 files)
- [ ] code-fixes.md
- [ ] ide.md
- [ ] live-template.md

### content/conceptual/aspects/simple-aspects/ (4 files)
- [ ] contracts.md
- [ ] overriding-methods.md
- [ ] overriding-properties.md
- [ ] simple-aspects.md

### content/conceptual/aspects/templates/ (10 files)
- [ ] auxilliary-templates.md
- [ ] dynamic-typing.md
- [ ] generating-expressions.md
- [ ] generating-statements.md
- [ ] invokers.md
- [ ] reflection.md
- [ ] template-compile-time.md
- [ ] template-overview.md
- [ ] template-parameters.md
- [ ] templates.md

### content/conceptual/aspects/testing/ (5 files)
- [ ] aspect-testing.md
- [ ] compile-time-testing.md
- [ ] debugging-aspects.md
- [ ] run-time-testing.md
- [ ] testing.md

### content/conceptual/aspects/ (root - 10 files)
- [ ] aspect-design.md
- [ ] aspect-inheritance.md
- [ ] aspects.md
- [ ] child-aspects.md
- [ ] decoupling-from-attributes.md
- [ ] dependency-injection.md
- [ ] diagnostics.md
- [ ] distributing.md
- [ ] eligibility.md
- [ ] fabrics-advising.md
- [ ] ordering.md
- [ ] type-system.md
- [ ] validating.md

### content/conceptual/configuration/ (8 files)
- [ ] configuration.md
- [ ] creating-logs.md
- [ ] msbuild-properties.md
- [ ] packages.md
- [ ] process-dump.md
- [ ] profiling.md
- [ ] telemetry.md
- [ ] troubleshooting-unattended-build.md

### content/conceptual/divorcing/ (1 file)
- [ ] divorcing.md

### content/conceptual/getting-started/ (1 file)
- [ ] getting-started.md

### content/conceptual/implementation/ (5 files)
- [ ] aspect-composition.md
- [ ] aspect-serialization.md
- [ ] fabrics-execution-order.md
- [ ] implementation.md
- [ ] pipeline.md

### content/conceptual/installing/ (4 files)
- [ ] dotnet-tool.md
- [ ] install-vsx.md
- [ ] installing.md
- [ ] register-license.md

### content/conceptual/introspection/ (1 file)
- [ ] linqpad.md

### content/conceptual/migration/ (9 files)
- [ ] benefits-over-postsharp.md
- [ ] differences-from-postsharp.md
- [ ] feature-status.md
- [ ] migrating-aspects.md
- [ ] migrating-configuration.md
- [ ] migrating-inpc.md
- [ ] migrating-multicasting.md
- [ ] migration.md
- [ ] when-migrate.md

### content/conceptual/release-notes/ (13 files)
- [ ] release-notes-2023.0.md
- [ ] release-notes-2023.1.md
- [ ] release-notes-2023.2.md
- [ ] release-notes-2023.3.md
- [ ] release-notes-2023.4.md
- [ ] release-notes-2024.0.md
- [ ] release-notes-2024.1.md
- [ ] release-notes-2024.2.md
- [ ] release-notes-2025.0.md
- [ ] release-notes-2025.1.md
- [ ] release-notes-2026.0.md
- [ ] release-notes.md

### content/conceptual/sdk/ (4 files)
- [ ] aspect-weavers.md
- [ ] custom-metrics.md
- [ ] roslyn-api.md
- [ ] sdk.md

### content/conceptual/using/ (10 files)
- [ ] adding-aspects-with-fabrics.md
- [ ] adding-aspects.md
- [ ] amending-many-projects.md
- [ ] configuring.md
- [ ] debugging-aspect-oriented-code.md
- [ ] fabrics.md
- [ ] getting-aspects.md
- [ ] live-templates.md
- [ ] understanding-your-code-with-aspects.md
- [ ] using.md

### content/conceptual/ (root - 2 files)
- [ ] conceptual.md
- [ ] requirements.md

### content/overview/ (1 file)
- [ ] overview.md

### content/patterns/caching/ (12 files)
- [ ] caching-keys.md
- [ ] caching.md
- [ ] configuring.md
- [ ] dependencies.md
- [ ] exclude-parameters.md
- [ ] getting-started.md
- [ ] invalidation.md
- [ ] locking.md
- [ ] pubsub.md
- [ ] redis.md
- [ ] troubleshooting.md
- [ ] value-adapters.md

### content/patterns/contracts/ (6 files)
- [ ] adding-contracts.md
- [ ] configuring-contracts.md
- [ ] contract-types.md
- [ ] contracts.md
- [ ] enforcing-non-nullability.md
- [ ] invariants.md

### content/patterns/dependency-injection/ (1 file)
- [ ] dependency-injection.md

### content/patterns/immutability/ (1 file)
- [ ] immutability.md

### content/patterns/memoization/ (1 file)
- [ ] memoization.md

### content/patterns/observability/ (2 files)
- [ ] observability.md
- [ ] standard-cases.md

### content/patterns/wpf/ (3 files)
- [ ] command.md
- [ ] dependency-property.md
- [ ] wpf.md

### content/patterns/ (root - 1 file)
- [ ] patterns.md

### content/reviewing/ (1 file)
- [ ] reviewing.md

### content/videos/ (11 files)
- [ ] architecture-verification.md
- [ ] code-fixes.md
- [ ] custom-architecture-rules.md
- [ ] debugging.md
- [ ] fabrics-and-inheritance.md
- [ ] first-aspect.md
- [ ] more-aspect-types.md
- [ ] reporting-errors-and-warnings.md
- [ ] short-introduction.md
- [ ] testing.md
- [ ] videos.md

### content/ (root - 1 file)
- [ ] index.md

## Common issues to look for

### High priority (likely to affect many files)
1. **List markers**: `*` instead of `-`
2. **Multiple blank lines**: More than one blank line between sections
3. **Missing "See also" section**: Most articles missing proper `> [!div class="see-also"]` format
4. **Title case in headings**: Check for incorrectly capitalized words
5. **Missing Oxford comma**: In series of three or more items
6. **Inline code blocks**: Should use external files with `[!metalama-test]` or `[!metalama-file]`

### Medium priority
1. **Filler phrases**: "You can", "There is/are", "In order to"
2. **Missing contractions**: "do not" instead of "don't", "it is" instead of "it's"
3. **Passive voice**: Look for "is/are/was/were [verb]ed by"
4. **Missing `level` field**: In conceptual articles
5. **Incorrect "See also" format**: Should use `> [!div class="see-also"]` not `## See also`

### Lower priority
1. **API cross-reference format**: Ensure `<xref:...>` is used consistently
2. **Code terminology**: Verify C# terms match official docs
3. **Example section titles**: Should be "Example: [description]"

## Execution approach

### Phase 1: Automated checks
Create scripts to detect:
- Files missing required YAML fields
- Files using `*` for list markers
- Files with multiple consecutive blank lines
- Files missing "See also" section or using wrong format
- Headings with Title Case
- Files with inline Markdown code blocks (should use external files)

### Phase 2: Manual review by section
Review each directory section by section, starting with highest-traffic areas:
1. getting-started/
2. simple-aspects/
3. using/
4. patterns/
5. Release notes
6. Remaining conceptual/
7. api/
8. videos/

### Phase 3: Apply fixes
For each file:
1. Run automated fixes where possible
2. Manual review and fix remaining issues
3. Convert `## See also` sections to `> [!div class="see-also"]` format
4. Verify changes don't break links or formatting
5. Update `modified-date` if changes are significant

## Current progress

### Completed
- [x] Automated checks run - identified issues:
  - 87 files with `*` list markers (498 occurrences)
  - ~98 multiple blank line occurrences
  - 165 files missing "See also" section
  - ~24 headings with Title Case

### In progress
- [ ] Fixing list markers in high-traffic areas
- [ ] Adding proper "See also" sections with `> [!div class="see-also"]` format

### Files already fixed
- content/conceptual/getting-started/getting-started.md
- content/conceptual/aspects/simple-aspects/contracts.md
- content/conceptual/aspects/simple-aspects/overriding-methods.md
- content/conceptual/aspects/simple-aspects/overriding-properties.md
- content/conceptual/using/fabrics.md

## Estimated effort

- **Phase 1 (Automated checks)**: COMPLETE
- **Phase 2 (Manual review)**: ~5-10 minutes per file = 14-28 hours total
- **Phase 3 (Apply fixes)**: Varies; quick fixes can be batched

**Recommendation**: Prioritize high-traffic documentation first and batch similar fixes together.
