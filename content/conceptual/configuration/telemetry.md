---
uid: telemetry
level: 200
summary: "This article explains what data Metalama collects, how to modify its settings, and how to disable telemetry or reset the device ID. It also explains the conditions for license auditing."
keywords: "telemetry, Metalama, disable telemetry, reset device id, license audit, Metalama command-line tools, environment variable, Visual Studio Tools, performance degradation, exception report"
created-date: 2023-01-23
modified-date: 2025-11-30
---

# Telemetry

This article explains what data Metalama collects and how to modify its settings.

## What is being collected?

By default, Metalama collects and sends usage and quality reports to PostSharp Technologies. These telemetry reports are anonymous and are collected under these circumstances:

- When an unexpected _failure_ or _performance degradation_ occurs, an exception report with an anonymized call stack is sent.
- Periodically, for each project you're building, we gather data such as a one-way hash of the project name, the target framework version, the project size, the number of aspects used, the amount of code saved by Metalama, and performance metrics.

All reports include a randomly generated device ID, which you can [reset at any time using Metalama command-line tools](#resetting-your-device-id).

Telemetry data is collected and processed in accordance with our [Privacy Policy](https://www.postsharp.net/company/legal/privacy-policy).

### License audit

In addition to telemetry, if you're using premium components of Metalama (as opposed to only open-source ones), the software undergoes a _license audit_. This audit is anonymous but mandatory for Metalama Community and trial licenses. It's used to gather statistics on the number of users. If you're using a license key, license audit reports include the ID of your license key. If you disagree with license auditing, [contact our sales team](mailto:hello@postsharp.net), and we'll provide you with a new license key that includes a license audit waiver flag.

## Disabling telemetry

Telemetry is enabled by default. You can disable it using one of these methods.

### Option 1. Defining an environment variable

Disable telemetry by defining the `METALAMA_TELEMETRY_OPT_OUT` environment variable to any non-empty value.

This environment variable has priority over any other setting. It allows you to disable telemetry for all devices in your domain using remote management tools such as Azure Endpoint Manager.

This approach doesn't affect the license audit mechanism.

### Option 2. Using Metalama command-line tools

1. Install Metalama command-line tools as described in <xref:dotnet-tool>.
2. Execute this command:

   ```powershell
   metalama telemetry disable
   ```

### Option 3. Using Visual Studio Tools for Metalama

1. In the top-level menu, choose _Options_.
2. Select the _Metalama + PostSharp_ category, then the _Privacy_ page.
3. Set individual settings.

## Resetting your device ID

Metalama Telemetry uses a randomly generated GUID to uniquely identify your device. You can reset this ID at any time. Once you reset your ID, PostSharp Technologies can no longer correlate past and future reports.

1. Install Metalama command-line tools as described in <xref:dotnet-tool>.
2. Execute this command:

   ```powershell
   metalama telemetry reset-device-id
   ```

> [!div class="see-also"]
> <xref:configuration>
> <xref:dotnet-tool>
