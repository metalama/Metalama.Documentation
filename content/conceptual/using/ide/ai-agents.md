---
uid: ai-agents
level: 100
summary: "Install the Metalama plugin for AI coding agents such as Claude Code and OpenAI Codex to get AI-assisted aspect development."
keywords: "Claude Code, OpenAI Codex, Metalama AI, AI coding agent, Claude plugin, Codex plugin, aspect development"
created-date: 2025-12-11
modified-date: 2026-11-13
---

# Configuring AI agents

AI coding agents such as [Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview) and [OpenAI Codex](https://developers.openai.com/codex) generate much better Metalama code when they can look up the real documentation instead of guessing from memory. The Metalama plugin gives them comprehensive knowledge about aspects, templates, fabrics, and the entire Metalama API.

## What the plugin provides

The Metalama plugin gives your AI agent access to:

- **Conceptual documentation**: Complete guides on aspects, templates, fabrics, validation, and configuration.
- **API reference**: Full documentation for all Metalama namespaces and types.
- **Sample code**: Working examples demonstrating common patterns and techniques.
- **Pattern libraries**: Documentation for `Metalama.Patterns.Contracts`, `Metalama.Patterns.Caching`, `Metalama.Patterns.Observability`, and more.

## Installing in Claude Code

1. Add the Metalama marketplace to Claude Code:

   ```powershell
   /plugin marketplace add https://github.com/metalama/Metalama.AI.Skills
   ```

2. Install the Metalama plugin:

   ```powershell
   /plugin install metalama
   ```

To update to a newer version:

```powershell
/plugin update metalama
```

## Installing in OpenAI Codex

1. Add the Metalama marketplace to Codex:

   ```powershell
   codex plugin marketplace add metalama/Metalama.AI.Skills
   ```

2. Install the `metalama` plugin from the plugin directory (**Plugins**) in the Codex app.

To update to a newer version:

```powershell
codex plugin marketplace upgrade
```

## Using the plugin

Once installed, the agent automatically uses the Metalama plugin when you ask questions about:

- Creating or modifying aspects
- Writing T# templates
- Using fabrics to apply aspects in bulk
- Working with the Metalama code model
- Implementing patterns like caching, contracts, or observability

If the agent doesn't seem to use Metalama-specific knowledge, explicitly mention "Metalama" in your prompt.

## Letting the agent see the generated code

An agent cannot use the Metalama Diff of Visual Studio. To check the code that an aspect generates, it must read the transformed code. The fastest way is the `metalama preview` command, which prints the transformed and formatted code of one or a few files without building the whole project:

```powershell
metalama preview src/MyProject/Services/OrderService.cs
```

The Metalama plugin tells the agent to use this command after it changes an aspect or the code that an aspect targets. The command belongs to the Metalama command line tool, so install the tool on the machine where the agent runs:

```powershell
dotnet tool install -g metalama.tool
```

For details, see <xref:previewing-transformed-code>.

## Letting the agent query the aspects

When an agent reads code to which aspects apply, the source code does not show what the aspects do. An agent cannot use the Aspect Explorer of Visual Studio either. The `metalama introspect` command answers these questions from the command line: which aspects apply to a declaration, what created them, which transformations they make and in which order, and which diagnostics they reported. For instance, an agent can check the aspects of a method before it changes the method:

```powershell
metalama introspect show declaration src/MyProject/Services/OrderService.cs:42
```

The command reads the introspection file of the last build, which the build writes only when the `MetalamaWriteIntrospectionFile` MSBuild property is `True`. Set this property in `Directory.Build.props` so that every build writes the file, and the agent can query it at any time without building. The command belongs to the same command line tool as `metalama preview`.

For details, see <xref:introspecting-from-command-line>.

### Example prompts

- "Create a logging aspect that logs method entry and exit"
- "How do I introduce a property to a class using Metalama?"
- "Write a contract that validates a string parameter is not empty"
- "How do I apply an aspect to all public methods in a namespace?"

> [!div class="see-also"]
> <xref:ide-configuration>
> <xref:previewing-transformed-code>
> <xref:introspecting-from-command-line>
> [Claude Code documentation](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)
> [OpenAI Codex plugin documentation](https://developers.openai.com/codex/plugins/build)
