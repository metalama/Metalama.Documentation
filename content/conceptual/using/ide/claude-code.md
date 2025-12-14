---
uid: ide-claude-code
level: 100
summary: "Configure Claude Code with Metalama documentation to get AI-assisted aspect development."
keywords: "Claude Code, Metalama AI, AI coding assistant, Claude plugin, aspect development"
created-date: 2025-12-11
modified-date: 2025-12-14
---

# Configuring Claude Code

[Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview) is Anthropic's AI-powered coding assistant that runs in your terminal. You can enhance Claude Code with Metalama-specific knowledge by installing the Metalama plugin, which provides comprehensive documentation about aspects, templates, fabrics, and the entire Metalama API.

## Installing the Metalama plugin

1. Add the Metalama marketplace to Claude Code:

   ```
   /plugin marketplace add https://github.com/metalama/Metalama.AI.Skills
   ```

2. Install the Metalama plugin:

   ```
   /plugin install metalama
   ```

## Verifying the installation

After installation, verify the plugin is recognized by Claude Code:

1. Start Claude Code in your terminal.
2. Ask Claude "What skills do you have?" or check if Metalama-related questions trigger the skill.
3. When the plugin is active, Claude will have access to Metalama documentation.

## What the plugin provides

The Metalama plugin gives Claude Code access to:

- **Conceptual documentation**: Complete guides on aspects, templates, fabrics, validation, and configuration.
- **API reference**: Full documentation for all Metalama namespaces and types.
- **Sample code**: Working examples demonstrating common patterns and techniques.
- **Pattern libraries**: Documentation for Metalama.Patterns.Contracts, Caching, Observability, and more.

## Using the plugin

Once installed, Claude Code automatically uses the Metalama plugin when you ask questions about:

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

## Updating the plugin

To update to a newer version:

```
/plugin update metalama
```

## Troubleshooting

### Plugin not being detected

If the plugin is not detected, verify the marketplace was added correctly:

```
/plugin marketplace list
```

Then reinstall the plugin:

```
/plugin install metalama
```

### Claude not using Metalama knowledge

If Claude doesn't seem to use Metalama-specific knowledge:

1. Explicitly mention "Metalama" in your prompt.
2. Verify the plugin is installed with `/plugin list`.
3. Try restarting Claude Code.

> [!div class="see-also"]
>
> <xref:ide-configuration>
>
> [Claude Code Documentation](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)
