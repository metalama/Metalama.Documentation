---
uid: caching-troubleshooting
summary: "The document provides instructions on how to troubleshoot the caching feature in Metalama using verbose logging, both with and without dependency injection."
---
# Troubleshooting Metalama Caching

If you need to troubleshoot the caching feature, you can enable verbose logging.

## With dependency injection

If your application utilizes dependency injection, add the .NET logging service as usual using the <xref:Microsoft.Extensions.DependencyInjection.LoggingServiceCollectionExtensions.AddLogging*> extension method. The the <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*> extension method automatically uses logging when available.

 Therefore, to troubleshoot caching, just set the minimum caching level to `Debug` using the <xref:Microsoft.Extensions.Logging.LoggingBuilderExtensions.SetMinimumLevel*> method.

The following code snippet illustrates this:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Logging/Logging.Program.cs marker="AddLogging"]

### Example: Logging of caching with dependency injection

The following is an update to the _getting started_ example, with logging enabled. You can observe detailed logging in the program output.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Logging/Logging.cs]

## Without dependency injection

The <xref:Metalama.Patterns.Caching.CachingService> object and other components of Metalama Caching acquire the <xref:Flashtrace.IFlashtraceLoggerFactory> interface through the <xref:System.IServiceProvider>. To enable logging, you must provide an implementation of <xref:System.IServiceProvider> that supports the <xref:Flashtrace.IFlashtraceLoggerFactory> interface. Then, pass this <xref:System.IServiceProvider> to the <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method. You can use the <xref:Flashtrace.Loggers.TraceSourceLoggerFactory> class, which relies on the <xref:System.Diagnostics.TraceSource> system class.

If you plan to implement the <xref:Flashtrace.IFlashtraceLoggerFactory> yourself, you may find the <xref:Flashtrace.Loggers.SimpleFlashtraceLogger> useful.

