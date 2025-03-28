---
uid: telemetry
level: 200
summary: "The document outlines the data Metalama collects, how to modify its settings, and how to disable telemetry or reset the device id. It also explains the conditions for license auditing."
keywords: "telemetry, Metalama, disable telemetry, reset device id, license audit, Metalama Command Line Tools, environment variable, Visual Studio Tools, performance degradation, exception report"
created-date: 2023-01-23
modified-date: 2024-08-04
---

# Telemetry

This article explains the data that Metalama collects and how you can modify its settings.

## What is being collected?

By default, Metalama collects and sends usage and quality reports to PostSharp Technologies. These telemetry reports are anonymous and are collected under the following circumstances:

- In the event of an unexpected _failure_ or _performance degradation_. In such cases, an exception report, inclusive of an anonymized call stack, is reported.
- Periodically, for each project you are building, we gather data such as a one-way hash of the project name, the target framework version, the project size, the number of aspects used, the amount of code saved by Metalama, and performance metrics.

All reports include a randomly generated device id, which you can [reset at any time using Metalama Command Line Tools](#resetting-your-device-id).

Telemetry data is collected and processed in accordance with our [Privacy Policy](https://www.postsharp.net/company/legal/privacy-policy).

### License audit

In addition to telemetry, if you are using premium components of Metalama (as opposed to only open-source ones), the software undergoes a _license audit_. This audit is anonymous but mandatory for Metalama Community and trial licenses. It is used to gather statistics on the number of users. If you are using a license key, license audit reports include the id of your license key. If you disagree with license auditing, please [contact our sales team](mailto:hello@postsharp.net), and we will provide you with a new license key that includes a license audit waiver flag.

## Disabling telemetry

Telemetry is enabled by default. You can disable telemetry using one of the two methods outlined below.

### Option 1. Defining an environment variable

You can disable telemetry by defining the `METALAMA_TELEMETRY_OPT_OUT` environment variable to any non-empty value.

This environment variable has priority over any other setting. It allows you to disable telemetry for all devices in your domain using remote management tools such as Azure Endpoint Manager.

This approach does not affect the license audit mechanism.

### Option 2. Using Metalama Command Line Tools

1. Install Metalama Command Line Tools as outlined in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama telemetry disable
   ```

### Option 3. Using Visual Studio Tools for Metalama

1. In the top-level menu, choose _Options_.
2. Select the _Metalama + PostSharp_ category, then the _Privacy_ page.
3. Set individual settings.

## Resetting your device id

Metalama Telemetry uses a randomly generated GUID to uniquely identify your device. You can reset this ID at any time. Once you reset your ID, PostSharp Technologies will no longer be able to correlate past and future reports.

1. Install Metalama Command Line Tools as outlined in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama telemetry reset-device-id
   ```




