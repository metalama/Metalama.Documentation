<p align="center">
<img width="450" src="https://raw.githubusercontent.com/metalama/Metalama/HEAD/images/metalama.svg" alt="Metalama logo" />
</p>

# Metalama Plugin for Claude Code

This repository contains the Metalama plugin for [Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview), providing comprehensive documentation for aspect-oriented programming with Metalama.

## Installation

1. Add this marketplace to Claude Code:

   ```
   /plugin marketplace add https://github.com/metalama/Metalama.AI.Skills
   ```

2. Install the Metalama plugin:

   ```
   /plugin install metalama
   ```

## What's included

The plugin provides Claude Code with access to:

- **Conceptual documentation**: Complete guides on aspects, templates, fabrics, validation, and configuration.
- **API reference**: Full documentation for all Metalama namespaces and types.
- **Sample code**: Working examples demonstrating common patterns and techniques.
- **Pattern libraries**: Documentation for Metalama.Patterns.Contracts, Caching, Observability, and more.

## Usage

Once installed, Claude Code automatically uses Metalama knowledge when you ask questions about:

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

## Updating

To update to the latest version:

```
/plugin update metalama
```

## Resources

- [Metalama Documentation](https://doc.metalama.net)
- [Metalama GitHub](https://github.com/metalama/Metalama)
- [Claude Code Documentation](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)

---

> **Note:** This repository is entirely generated from the [Metalama.Documentation](https://github.com/metalama/Metalama.Documentation) repository. Do not edit files here directly.
