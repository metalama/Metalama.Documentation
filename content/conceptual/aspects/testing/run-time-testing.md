---
uid: run-time-testing
level: 300
summary: "The document discusses the approach of applying an aspect to a target code and creating unit tests to verify the resulting code's behavior, using a logging aspect as an example."
keywords: "unit tests"
created-date: 2023-02-21
modified-date: 2024-08-29
---

# Testing the aspect's run-time behavior

The objective of this approach is to apply an aspect to a specific target code and then create standard unit tests to verify that the resulting code behaves as expected.

For example, to test a logging aspect, you could configure your logging aspect to log to an in-memory `StringWriter`. Then, use a standard unit test to confirm that a logged method, when invoked from the test method, yields the expected result in the `StringWriter`. This concept is illustrated in the code snippet below.

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
> To make your aspects testable, you might benefit from using dependency injection in your aspects. This approach allows you to supply different implementations of your services in test scenarios than in production scenarios. For details, see @dependency-injection.

> [!WARNING]
> Run-time unit tests should not replace, but complement, aspect tests (see <xref:aspect-testing>). The problem with run-time unit tests is that the whole project is compiled at once, so it is difficult to debug a specific instance of an aspect in isolation from the other instances. The most convenient way to debug aspects during development is to create aspect tests. When a run-time unit test project fails to build because of an aspect, we suggest creating an aspect test to isolate, diagnose, and fix the issue. For more information, see <xref:debugging-aspects>.
