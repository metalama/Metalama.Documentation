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
- Include examples with the `[!metalama-test ...]` directive
- Use `[!metalama-file ...]` to include code snippets from external files
- Use `> [!NOTE]`, `> [!WARNING]`, `> [!IMPORTANT]` for callouts
- End articles with a "See also" section containing cross-references to related topics
- Use tables for comparing options or listing related articles with descriptions

## Formatting conventions

- Use `-` for unordered lists (not `*`)
- Use 4-space indentation for nested list items
- One blank line between sections, not multiple
- Code samples should be complete and runnable when possible
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
- Add commentary after example explaining key observations

## Directory structure

- Each directory should have an index file named after the directory (e.g., `aspects/aspects.md`, not `aspects/index.md`)
- Index files should include a table describing child pages with columns: Article | Description
- Articles must be added to `toc.yml` in the appropriate location
