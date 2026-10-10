---
uid: errors
level: 200
summary: "Reference of the errors and warnings reported by Metalama, its extensions, and the Metalama.Patterns libraries, with an example of the problem and of its resolution for each diagnostic."
keywords: "Metalama errors, Metalama warnings, LAMA diagnostics, error codes, diagnostic reference, build errors"
created-date: 2026-10-10
modified-date: 2026-10-10
---

# Errors and warnings

Metalama reports its errors and warnings with an identifier starting with `LAMA`. Each article in this section explains the cause of a diagnostic and shows how to resolve it, usually with an example that reports the diagnostic and a corrected version of the same code.

Diagnostics reported by the Metalama.Patterns libraries and by Metalama extensions also start with `LAMA`. For diagnostics that your own aspects report, see <xref:diagnostics>.

The articles are grouped by the feature or library that reports the diagnostic:

<!-- BEGIN GENERATED TABLE: run .claude/skills/document-diagnostics/scripts/Update-ErrorIndex.ps1, do not edit -->

| Article | Description |
|---------|-------------|
| <xref:errors-general> | Diagnostics about applying aspects, aspect classes and their attributes, eligibility, aspect ordering, fabrics, and the compilation pipeline. |
| <xref:errors-templates> | Diagnostics about T# templates: compile-time and run-time scopes, unsupported C# constructs, and template parameters. |
| <xref:errors-advising> | Diagnostics about advice: overriding and introducing members, implementing interfaces, adding initializers, and conflicts between aspects. |
| <xref:errors-serialization> | Diagnostics about converting compile-time values into run-time code, and about the serialization of aspects and fabrics across projects. |
| <xref:errors-design-time> | Diagnostics reported by the IDE integration of Metalama. |
| <xref:errors-licensing> | Diagnostics about the Metalama license and the features it includes. |
| <xref:errors-architecture> | Diagnostics reported by Metalama.Extensions.Architecture and Metalama.Extensions.Validation. |
| <xref:errors-dependency-injection> | Diagnostics reported by Metalama.Extensions.DependencyInjection. |
| <xref:errors-caching> | Diagnostics reported by Metalama.Patterns.Caching. |
| <xref:errors-contracts> | Diagnostics reported by Metalama.Patterns.Contracts. |
| <xref:errors-immutability> | Diagnostics reported by Metalama.Patterns.Immutability. |
| <xref:errors-observability> | Diagnostics reported by Metalama.Patterns.Observability. |
| <xref:errors-wpf> | Diagnostics reported by Metalama.Patterns.Wpf. |

<!-- END GENERATED TABLE -->
