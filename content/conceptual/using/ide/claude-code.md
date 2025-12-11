---
uid: ide-claude-code
level: 100
summary: "Configure Claude Code with Metalama documentation to get AI-assisted aspect development."
keywords: "Claude Code, Metalama AI, AI coding assistant, Claude skill, aspect development"
created-date: 2025-12-11
modified-date: 2025-12-11
---

# Configuring Claude Code

[Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview) is Anthropic's AI-powered coding assistant that runs in your terminal. You can enhance Claude Code with Metalama-specific knowledge by installing the Metalama skill, which provides comprehensive documentation about aspects, templates, fabrics, and the entire Metalama API.

## Installing the Metalama skill

1. Download the skill zip file from the [Metalama downloads page](https://www.postsharp.net/downloads/metalama). Look for `Metalama.Skill.{version}.zip` in the version folder (e.g., `metalama-2026.0/v2026.0.4-rc/`).

2. Extract the zip contents to your Claude skills directory:
   - **Windows**: `%USERPROFILE%\.claude\skills\metalama`
   - **macOS/Linux**: `~/.claude/skills/metalama`

3. Ensure the `SKILL.md` file is at the root of the `metalama` folder (not nested in a subfolder).

## Verifying the installation

After installation, verify the skill is recognized by Claude Code:

1. Start Claude Code in your terminal.
2. Ask Claude "What skills do you have?" or check if Metalama-related questions trigger the skill.
3. When the skill is active, Claude will have access to Metalama documentation.

## What the skill provides

The Metalama skill gives Claude Code access to:

- **Conceptual documentation**: Complete guides on aspects, templates, fabrics, validation, and configuration.
- **API reference**: Full documentation for all Metalama namespaces and types.
- **Sample code**: Working examples demonstrating common patterns and techniques.
- **Pattern libraries**: Documentation for Metalama.Patterns.Contracts, Caching, Observability, and more.

## Using the skill

Once installed, Claude Code automatically uses the Metalama skill when you ask questions about:

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

## Updating the skill

To update to a newer version, simply run the installation commands again with the new version number. The existing skill directory will be replaced.

## Troubleshooting

### Skill not being detected

Ensure the skill was extracted to the correct location:
- Windows: `%USERPROFILE%\.claude\skills\metalama`
- macOS/Linux: `~/.claude/skills/metalama`

The directory should contain a `SKILL.md` file at the root.

### Claude not using Metalama knowledge

If Claude doesn't seem to use Metalama-specific knowledge:

1. Explicitly mention "Metalama" in your prompt.
2. Verify the skill directory structure is correct.
3. Try restarting Claude Code.

> [!div class="see-also"]
>
> <xref:ide-configuration>
>
> [Claude Code Documentation](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)
