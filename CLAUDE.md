# Claude Code Instructions

## General

- This documentation is compiled using DocFx.
- Follow the [Microsoft Writing Style Guide](https://learn.microsoft.com/en-us/style-guide/welcome/)

## Microsoft Style Guide key points

- **Bigger ideas, fewer words**: Shorter is always better; cut unnecessary content
- **Write like you speak**: Read aloud, avoid jargon, sound like a friendly conversation
- **Use contractions**: it's, you'll, we're, don't—creates an approachable tone
- **Get to the point fast**: Lead with what's most important; prioritize keywords for scanning
- **Sentence-style capitalization**: Lowercase except first word and proper nouns
- **Oxford comma**: Always use the serial comma ("Android, iOS, and Windows")
- **Single space after punctuation**: No double spaces; no spaces around em dashes
- **Start with verbs**: Remove filler phrases like "you can" and "there is"
- **Skip end punctuation**: On short headings and list items (three words or fewer)
- **Code examples**: Concise, progress from simple to complex, easy to copy and run

## Style guidelines

- Titles should use "-ing" verb form and be problem-oriented (e.g., "Overriding methods", "Reporting diagnostics") except for conceptual topics (e.g., "Fabrics", "Eligibility")
- Use official C# terminology; when in doubt, refer to Microsoft's C# language specification and documentation—not blogs
- Use second person ("you") when addressing the reader
- Write in a professional, technical tone—clear and direct without being informal
- Use bold (`**text**`) for emphasis on key terms and UI elements
- Use backticks for code elements: types, methods, properties, namespaces, keywords
- Prefer active voice
- Be concise but thorough—explain the "why" alongside the "how"
- Use italics (_text_) for introducing new terms or concepts
- Use quotation marks for UI menu items (e.g., "Options")

## Document structure

- Every article starts with YAML front matter (`uid`, `summary`, `keywords`, `created-date`, `modified-date`)
- Set `created-date` when creating a new article; update `modified-date` for significant changes (not typo fixes)
- Conceptual articles also have a `level` field (200, 300, 400) indicating complexity
- Start with an introductory paragraph explaining the purpose and context
- Use a "Benefits" section when explaining why a feature matters
- Use numbered lists for step-by-step instructions
- Use bullet points for non-sequential items
- Use `> [!NOTE]`, `> [!WARNING]`, `> [!IMPORTANT]` for callouts
- End articles with a "See also" section containing cross-references to related topics. This section should start with this code:

    ```text
    > [!div class="see-also"]
    ```

- Use tables for comparing options or listing related articles with descriptions

## Code samples

- Code samples should be complete and runnable when possible. We give priority to external `.cs` files included with directives, so they can be compiled.
- Use `[!metalama-test ...]` to include whole examples based on the aspect framework, possibly composed of many files, with pre- and post-Metalama comparison.
- Use `[!metalama-file ...]` to include code snippets from external files
- You can find the original source code by substituting ~ with the repo root. To find the modified code, look for files named `Foo.t.cs` (`Foo` being the test name) under `obj/Debug/Metalama`.
- Examples should be relevant to the context. You should fetch the code to verify the relevance.


## Formatting conventions

- Fenced code blocks use triple backticks with language identifier (csharp, xml, powershell)
- Inline code in lists: place the code element first, then explain (e.g., "`nameof(value)` expression will be substituted...")

## Cross-references and links

- Use `<xref:fully.qualified.name>` for API cross-references (with optional `?text=DisplayText`)
- Internal API links: `<xref:Metalama.Framework.Namespace.TypeName>`
- With display text: `<xref:Metalama.Framework.Namespace.TypeName?text=DisplayText>` when the type name is important (not only the member name)
- Method references include asterisk: `<xref:Namespace.Type.Method*>`
- Article cross-references: `<xref:article-uid>`
- GitHub links for external code references
- NuGet package links in square brackets: [Package.Name](https://www.nuget.org/packages/Package.Name/)

## Release notes style

- Start with a brief introductory paragraph describing the release focus
- For major releases, include a "Highlights" section with bullet points
- Group changes by category (Platform Update, Breaking changes, Other improvements, etc.)
- Use GitHub issue links: `[#1234](https://github.com/metalama/Metalama/issues/1234)`
- List breaking changes with clear before/after descriptions
- Include "In Progress" section for features not yet stable

## Example sections

- Title format: "Example: [description]" or "### Example: [description]"
- Brief explanation before the example
- Use `[!metalama-test ...]` directive to include testable examples
- Add commentary after example explaining key observations (expected results)

## Directory structure

- Each directory should have an index file named after the directory (e.g., `aspects/aspects.md`, not `aspects/index.md`)
- Index files should include a table describing child pages with columns: Article | Description
- Articles must be added to `toc.yml` in the appropriate location
- When adding a new section, also update the parent index file (e.g., `conceptual.md`)

## Sample code projects

- Long code examples should be in their own compilable project under `code/`
- For aspect tests (with `*.t.cs` expected output files), reuse an existing project in `Metalama.Documentation.Snippets.TestBased.sln`
- Create standalone projects only when special package references are needed (e.g., Workspaces API)
- Standalone sample projects go in `Metalama.Documentation.Snippets.ProjectBased.sln`
- Use `[!metalama-files ~/code/ProjectName]` to include all files from a sample project
- Use `[!metalama-file ~/code/ProjectName/File.cs]` to include a single file

## Available Markdig directives

- `[!metalama-test ...]` - Include aspect test with source/transformed comparison
- `[!metalama-file ...]` - Include a single code file
- `[!metalama-files ...]` - Include multiple files or entire project directory
- Directive implementations are in `eng/src/Markdig/`

## Building documentation

- Use `Build.ps1 build` when sample code has changed (compiles samples, runs tests, generates HTML)
- Use `update-html.ps1` for documentation-only changes (faster, skips sample compilation)
- If errors occur in `source-dependencies/Metalama.Samples`, build that project first with `source-dependencies/Metalama.Samples/Build.ps1 build`
