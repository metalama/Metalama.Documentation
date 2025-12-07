---
uid: ide-rider
level: 100
summary: "Configure JetBrains Rider for use with Metalama, including aspect testing and diagnostics."
keywords: "Metalama Rider, Rider configuration, aspect testing Rider, xUnit Rider, test discovery Rider"
created-date: 2025-12-04
modified-date: 2025-12-04
---

# Configuring Rider

JetBrains Rider supports Metalama's core functionality out of the box. If you're creating aspects and want to test them, you'll need additional configuration.

## Configuring aspect test discovery

> [!NOTE]
> This section applies only if you're creating your own aspects and want to test them. If you're just using existing aspects, you can skip this.

Metalama's aspect testing framework uses a custom xUnit test discovery mechanism. By default, Rider uses metadata-based discovery and won't detect aspect tests. You'll need to configure Rider to use the test runner for discovery.

To configure test discovery:

1. Open **File** > **Settings** (or **Rider** > **Preferences** on macOS).
2. Navigate to **Build, Execution, Deployment** > **Unit Testing** > **xUnit.net**.
3. For **Test discovery**, select **Test Runner** instead of **Metadata**.

![Rider test runner settings](~/images/rider_test_runner_settings.png)

> [!NOTE]
> Running aspect tests in Rider is supported starting with Metalama 2023.1.

After you change this setting, rebuild your test project. Tests should now appear in the Unit Tests window.

## ReSharper configuration

If you're using ReSharper in Visual Studio, the same test discovery setting applies:

1. Open **Extensions** > **ReSharper** > **Options**.
2. Navigate to **Tools** > **Unit Testing** > **Test Frameworks** > **xUnit.net**.
3. Select **Test Runner** for test discovery.

## Handling orphaned processes

When you run aspect tests with diff tools enabled (see <xref:diff-tool>), Rider may show an "orphaned processes" dialog when tests complete while diff tools are still open.

To prevent this:

1. Open **Settings** > **Build, Execution, Deployment** > **Unit Testing**.
2. Find the orphaned processes settings.
3. Configure to ignore or automatically terminate diff tool processes.

## Limitations

The following Metalama features aren't available in Rider:

- **CodeLens**: Aspect information overlays
- **Diff preview**: Visual code transformation preview
- **Live templates**: Source code modification aspects

These features require the Metalama Tools for Visual Studio extension.

## Troubleshooting

### Tests not appearing

If aspect tests don't appear in the Unit Tests window:

1. Verify the test discovery setting is set to **Test Runner**.
2. Rebuild the test project.
3. Refresh the Unit Tests window.
4. Ensure the project references `Metalama.Testing.AspectTesting`.

### Build errors in test projects

If you see build errors related to Metalama in test projects:

1. Verify that `Metalama.Testing.AspectTesting` is referenced. This disables Metalama processing.
2. Check that `.t.cs` files aren't being compiled. The package should exclude them automatically.

> [!div class="see-also"]
>
> <xref:aspect-testing>
>
> <xref:diff-tool>
>
> <xref:ide-configuration>
