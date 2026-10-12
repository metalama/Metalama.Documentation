---
uid: telemetry
level: 200
summary: "This article explains what data Metalama collects, how to review exception reports before they are sent, and how to disable telemetry per machine or per repository, reset the device ID, and check the current telemetry status."
keywords: "telemetry, Metalama, disable telemetry, reset device id, license audit, metalama.json, exception report, performance report, privacy, environment variable, telemetry status, news feed, rss, never report this error"
created-date: 2023-01-23
modified-date: 2026-07-30
---

# Telemetry

This article explains what data Metalama collects and how to modify its settings.

## What Metalama collects

By default, Metalama collects usage and quality reports and sends them to PostSharp Technologies. These reports are anonymous and fall into two categories:

- **Usage reports.** Periodically, for each project you build, Metalama gathers data such as a one-way hash of the project name, the target framework version, the project size, the number of aspects used, the amount of code saved by Metalama, and performance metrics. Usage reports are sent automatically unless you opt out.
- **Exception and performance reports.** When an unexpected _failure_ or _performance degradation_ occurs, Metalama captures a report that includes an anonymized call stack. By default, these reports are _not_ sent automatically. Instead, they're stored on your machine, and you're notified so you can [review their exact content](#reviewing-exception-and-performance-reports) before deciding whether to send them. Source code is never collected, and sensitive data such as exception messages and secrets are stripped from any report that's uploaded.

All reports include a randomly generated device ID, which you can [reset at any time using Metalama command-line tools](#resetting-your-device-id).

The first time Metalama runs, it informs you that telemetry is enabled and points you to the opt-out settings. On Windows, this notice appears as a toast notification; on other platforms, it's shown as a web page with a disclaimer. Telemetry data stored on your machine is automatically deleted after 30 days.

Telemetry data is collected and processed in accordance with our [Privacy Policy](https://www.postsharp.net/company/legal/privacy-policy).

### License audit

In addition to telemetry, if you're using premium components of Metalama (as opposed to only open-source ones), the software undergoes a _license audit_. This audit is anonymous but mandatory for Metalama Community and trial licenses. It's used to gather statistics on the number of users. If you're using a license key, license audit reports include the ID of your license key.

> [!WARNING]
> The license audit is itself a form of telemetry, and it ignores your telemetry settings: disabling telemetry by any of the methods described in this article doesn't stop it. The only way to avoid the license audit is to use Metalama strictly under its open-source MIT license, with no premium components.
>
> Enterprise customers can request a _license audit waiver_. The waiver flag is embedded in the license key, so once you register a license key that includes it, the license audit no longer runs. To request a waiver, [contact us](mailto:hello@postsharp.net).

## Reviewing exception and performance reports

Exception and performance reports contain the most detailed information, so by default Metalama lets you review each one before it leaves your machine:

1. When Metalama captures a report, it stores the report locally and, on Windows, shows a notification.
2. Opening the notification displays the full local report next to the exact, anonymized content that would be uploaded, so you can see precisely what the anonymization removes.
3. You then choose one of three actions:

   - **Report** sends this report, and only this one.
   - **Report**, with _Automatically report all … in the future_ selected, sends this report and switches that category (exceptions or performance problems) to automatic sending.
   - **Never report this error** discards the report and stops Metalama from asking about that particular error again. Other errors are still reported to you for review.

If you don't act on the notification, nothing is sent and the report stays on your machine. Metalama asks you again the next time the same error occurs, at most once an hour, until you choose one of the three actions above. A report you never decide on is deleted along with the rest of the local telemetry data after 30 days.

> [!NOTE]
> **Never report this error** applies to the error as identified in your current version of Metalama. An error's signature includes the Metalama version number, so after upgrading you may be asked again about a seemingly identical error. This is intentional: the new version may behave differently, and if the problem persists we want to know about it.

This review-first behavior is the default for exception and performance reports. Usage reports have no review step; they're simply enabled or disabled. To stop being asked about exceptions altogether, set that category to _never send_ on the Privacy options page or with the Metalama command-line tools, as described in [Disabling telemetry](#disabling-telemetry).

## Disabling telemetry

Telemetry is enabled by default during interactive development. Metalama automatically disables it when it detects an unattended process, such as a continuous-integration or build server, so these environments don't send telemetry even if you take no action.

To opt out explicitly, use one of the following methods. If you're an individual developer, the Privacy options page (Option 1) or the command-line tool (Option 2) is the simplest. To opt out for an entire repository or across a whole machine or domain, use a `metalama.json` file (Option 3) or the environment variable (Option 4).

### Option 1. Using the Privacy options page

Configure telemetry from the Privacy options page:

- Open the browser-based configuration interface by running `metalama ui` (see <xref:dotnet-tool> to install the `metalama` command-line tool), then go to the _Privacy_ page; or
- In Visual Studio, choose _Options_ from the top-level menu, select the _Metalama + PostSharp_ category, then the _Privacy_ page.

On this page:

- **Usage reports** are controlled by a single on/off setting.
- **Exception reports** and **performance reports** each offer three choices: send automatically, ask before sending (review first), or never send.

### Option 2. Using Metalama command-line tools

1. Install Metalama command-line tools as described in <xref:dotnet-tool>.
2. Execute this command:

   ```powershell
   metalama telemetry disable
   ```

   By default, this disables all telemetry. To disable only a specific category, pass a scenario argument: `usage`, `exception`, or `performance`. For example, to stop sending exception reports while keeping usage reporting enabled:

   ```powershell
   metalama telemetry disable exception
   ```

   To re-enable telemetry, use `metalama telemetry enable`. To return a category to its default state (review-first for exceptions and performance problems), use `metalama telemetry reset`.

### Option 3. Adding a `metalama.json` file to your repository

Disable telemetry for an entire repository by adding a `metalama.json` file at the repository root (the directory that contains the `.git` folder):

```json
{
  "telemetry": {
    "enabled": false
  }
}
```

Because this file is committed to source control, the setting applies automatically to every developer and every CI/CD agent that builds the repository, with no per-machine configuration. Unlike the automatic opt-out for unattended processes — which depends on Metalama detecting the environment — a `metalama.json` opt-out is explicit and always applies: to local and CI builds, to design-time analysis, and to Visual Studio.

`metalama.json` is a general, extensible Metalama repository configuration file (telemetry is currently its only setting). Note these constraints:

- The file must be located at the repository root. A `metalama.json` placed anywhere else (for instance, in a project subfolder) is ignored and produces a warning.
- The setting applies only to the repository whose root contains the file. It has no effect on other repositories built on the same machine.
- When a project is built outside of any git repository, there's no repository context to evaluate, and Metalama performs no telemetry at all.
- A malformed `metalama.json` is ignored and reported as a diagnostic; it never breaks the build.
- When the `METALAMA_TELEMETRY_OPT_OUT` environment variable opts out (set to `true`), it takes priority over this file. Setting it to `false` or `0` has no effect and doesn't override an opt-out declared in `metalama.json`.

### Option 4. Defining an environment variable

Disable telemetry by setting the `METALAMA_TELEMETRY_OPT_OUT` environment variable to `true` (or to any non-empty value other than `false` or `0`).

This is an opt-out switch only: setting the variable to `false` or `0` has no effect — it's treated as if the variable weren't set, and it neither enables telemetry nor overrides any other setting. When the variable does opt out, it has priority over all other settings. Use it to disable telemetry for all devices in your domain through remote management tools such as Azure Endpoint Manager.

This approach doesn't affect the license audit mechanism.

## News notifications

Metalama periodically downloads a news feed from `metalama.net` to notify you about new releases and other news. This is a one-way download: the feed carries no usage data and isn't used as a telemetry channel. Like any web request, however, it does reveal your device's IP address to the server.

Because the news feed isn't usage telemetry, disabling telemetry through the user options — the Privacy options page or `metalama telemetry disable` — doesn't stop it. To stop the news feed:

- Run `metalama rss disable` to turn off the news feed specifically.
- Or opt out through the `METALAMA_TELEMETRY_OPT_OUT` environment variable or a `metalama.json` file. Both of these also stop the news feed.

The news feed is likewise not downloaded in unattended processes, such as CI and build servers, or when building outside a git repository.

## Checking your telemetry settings

To see the current telemetry configuration, run the following command (see <xref:dotnet-tool> to install the `metalama` command-line tool):

```powershell
metalama telemetry status
```

For each category, this command shows both the value you configured and the value actually in effect, and explains any difference (for example, telemetry disabled because the process is unattended or because the repository opted out through `metalama.json`). It also warns you when the current directory isn't inside a git repository, reports any misplaced or malformed `metalama.json` file, and shows when the device ID was last rotated.

## Resetting your device ID

Metalama Telemetry uses a randomly generated GUID to uniquely identify your device. This identifier, together with the anonymization salts, rotates automatically about once a month, so reports from different months can't be correlated. You can also reset the identifier manually at any time. Once you reset your ID, PostSharp Technologies can no longer correlate past and future reports.

1. Install Metalama command-line tools as described in <xref:dotnet-tool>.
2. Execute this command:

   ```powershell
   metalama telemetry reset-device-id
   ```

> [!div class="see-also"]
> <xref:configuration>
> <xref:dotnet-tool>
