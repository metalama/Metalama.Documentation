---
uid: run-time-testing
level: 300
summary: "The document discusses the approach of applying an aspect to a target code and creating unit tests to verify the resulting code's behavior, using a logging aspect as an example."
keywords: "unit tests, run-time testing, aspect behavior testing, integration testing"
created-date: 2023-02-21
modified-date: 2025-11-30
---

# Testing the aspect's run-time behavior

This approach applies an aspect to target code and uses standard unit tests to verify that the resulting code behaves as expected.

To test a logging aspect, configure it to log to an in-memory `StringWriter`. Then, use a standard unit test to confirm that when a logged method is invoked, it produces the expected result in the `StringWriter`. The following example illustrates this approach.

```cs
class MyTests
{
    StringWriter _logger = new();

    public void TestVoidMethod()
    {
        this.VoidMethod(5);

        Assert.Equal( """
                    Entering VoidMethod(5).
                    Oops
                    VoidMethod(5) succeeded.
                    """,
        _logger.ToString());
    }

    [Log]
    private void VoidMethod(int p)
    {
        _logger.WriteLine("Oops");
    }
}
```

> [!TIP]
> To make your aspects testable, consider using dependency injection. This approach allows you to supply different implementations of your services in test scenarios than in production scenarios. For details, see <xref:dependency-injection>.

> [!WARNING]
> Run-time unit tests shouldn't replace snapshot tests (see <xref:aspect-testing>), but complement them. The problem with run-time unit tests is that the whole project is compiled at once, making it difficult to debug a specific instance of an aspect in isolation from other instances. The most convenient way to debug aspects during development is to create snapshot tests. When a run-time unit test project fails to build because of an aspect, create a snapshot test to isolate, diagnose, and fix the issue. For more information, see <xref:debugging-aspects>.

> [!div class="see-also"]
> <xref:testing>
> <xref:aspect-testing>
> <xref:debugging-aspects>
> <xref:dependency-injection>
