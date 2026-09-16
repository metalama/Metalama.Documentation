---
uid: Metalama.Framework.Metrics
summary: *content
created-date: 2023-01-26
modified-date: 2023-07-11
---

This namespace enables you to consume and implement metrics.

To consume a metric, begin with a declaration, then call the extension method <xref:Metalama.Framework.Metrics.MetricsExtensions.Metrics*?text=MetricsExtensions.Metric>. Afterward, call <xref:Metalama.Framework.Metrics.Metrics`1.Get*>.

## Class Diagram

```mermaid
classDiagram

    class IMeasurable {

    }

    class IDeclaration {

    }

    class IMetric~TMeasurable~ {

    }

    class YourCustomMetric {

    }

    class YourCustomMetricProvider {
        <<sdk>>

    }

    class IMetricProvider~TMetric~ {
        GetMetric(IMeasurable)
    }

    class Metrics~TMeasurable~ {
        Get()
    }

    class MetricsExtensions {
        <<extension class>>
        Metrics()$
    }

    IMeasurable <-- MetricsExtensions : extension methods
    Metrics <-- MetricsExtensions : exposes
    IMetric~TMeasurable~ <-- Metrics : exposes

    IMeasurable <|-- IDeclaration : implements

    IMetric~TMeasurable~ <|-- YourCustomMetric : implements
    IMetricProvider~TMetric~ <|-- YourCustomMetricProvider : implements
    YourCustomMetric <-- YourCustomMetricProvider : computes

    IMetric~TMeasurable~ <-- IMetricProvider~TMetric~ : computes

    IMeasurable <-- IMetric~TMeasurable~ : applies to


```

## Namespace members


