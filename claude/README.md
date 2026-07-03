<p align="center">
<img width="450" src="https://raw.githubusercontent.com/metalama/.github/HEAD/images/metalama.svg" alt="Metalama logo" />
</p>

# Metalama Plugin for AI Coding Agents

This repository contains the Metalama plugin for [Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview) and [OpenAI Codex](https://developers.openai.com/codex/plugins/build), providing documentation for aspect-oriented programming with Metalama.

## Installation

### Claude Code

1. Add this marketplace to Claude Code:

   ```
   /plugin marketplace add https://github.com/metalama/Metalama.AI.Skills
   ```

2. Install the Metalama plugin:

   ```
   /plugin install metalama
   ```

### OpenAI Codex

1. Add this marketplace to Codex:

   ```
   codex plugin marketplace add metalama/Metalama.AI.Skills
   ```

2. Install the Metalama plugin from the Codex plugin directory (**Plugins** in the Codex app).

## What's included

The plugin provides the coding agent with access to:

- **Conceptual documentation**: Guides on aspects, templates, fabrics, validation, and configuration.
- **API reference**: Full documentation for all Metalama namespaces and types.
- **Sample code**: Working examples demonstrating common patterns and techniques.
- **Pattern libraries**: Documentation for Metalama.Patterns.Contracts, Caching, Observability, and more.

## Usage

Once installed, the agent automatically uses Metalama knowledge when you ask questions about:

- Creating or modifying aspects
- Writing T# templates
- Using fabrics to apply aspects in bulk
- Working with the Metalama code model
- Implementing patterns like caching, contracts, or observability

### Example prompts

- "Create a logging aspect that logs method entry and exit"
- "How do I introduce a property to a class using Metalama?"
- "Write a contract that validates a string parameter is not empty"
- "How do I apply an aspect to all public methods in a namespace?"


## Resources

- [Metalama Documentation](https://doc.metalama.net)
- [Metalama GitHub](https://github.com/metalama/Metalama)
- [Claude Code Documentation](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)
- [OpenAI Codex Plugin Documentation](https://developers.openai.com/codex/plugins/build)

---

> **Note:** This repository is entirely generated from the [Metalama.Documentation](https://github.com/metalama/Metalama.Documentation) repository. Do not edit files here directly.
