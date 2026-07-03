---
uid: ai-agents
level: 100
summary: "Install the Metalama plugin for AI coding agents such as Claude Code and OpenAI Codex to get AI-assisted aspect development."
keywords: "Claude Code, OpenAI Codex, Metalama AI, AI coding agent, Claude plugin, Codex plugin, aspect development"
created-date: 2025-12-11
modified-date: 2026-07-03
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

### Example prompts

- "Create a logging aspect that logs method entry and exit"
- "How do I introduce a property to a class using Metalama?"
- "Write a contract that validates a string parameter is not empty"
- "How do I apply an aspect to all public methods in a namespace?"

> [!div class="see-also"]
> <xref:ide-configuration>
> [Claude Code documentation](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)
> [OpenAI Codex plugin documentation](https://developers.openai.com/codex/plugins/build)
