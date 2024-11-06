---
uid: compile-time-testing
level: 400
summary: "The document provides a guide on testing compile-time code using unit tests, outlining the benefits and a step-by-step process to create unit tests for compile-time code in a .NET 6.0 project using the Metalama.Testing.UnitTesting package."
keywords: "compile-time code testing, unit tests compile-time code, .NET 6.0, Metalama.Testing.UnitTesting, compile-time logic, unit-testing compile-time classes, Xunit test project, MetalamaRemoveCompileTimeOnlyCode, disable Metalama, test methods"
created-date: 2023-03-03
modified-date: 2024-11-06
---

# Testing compile-time helper code

When building complex aspects, it's advisable to shift the intricate compile-time logic, such as code that queries the code model, to compile-time helper classes that are not aspects. Unlike aspects, these compile-time classes can be subjected to unit tests.

## Benefits

Unit-testing compile-time classes offers the following advantages:

* It's generally simpler to achieve comprehensive test coverage with unit tests than with aspect tests (see <xref:aspect-testing>).
* Debugging unit tests is easier than debugging aspect tests.

## Creating unit tests for your compile-time code

### Step 1. Disable pruning of compile-time code

In the project defining the compile-time code, set the `MetalamaRemoveCompileTimeOnlyCode` property to `False`:

```xml
<PropertyGroup>
    <MetalamaRemoveCompileTimeOnlyCode>False</MetalamaRemoveCompileTimeOnlyCode>
</PropertyGroup>
```

Failing to follow this step will result in an exception whenever any compile-time code is called from a unit test.

### Step 2. Create an Xunit test project

Proceed to create an Xunit test project as you usually would.

It's strongly recommended to target .NET 6.0 or later, as temporary files cannot be automatically cleaned up with lower .NET versions.

Disable Metalama for the test project by defining the following property:

```xml
<PropertyGroup>
   <MetalamaEnabled>false</MetalamaEnabled>
</PropertyGroup>
```

### Step 3. Reference the Metalama.Testing.UnitTesting package

```xml
<ItemGroup>
    <PackageReference Include="Metalama.Testing.UnitTesting" Version="CHANGE ME" />
</ItemGroup>
```

### Step 4. Create a test class derived from UnitTestClass

Create a new test class that derives from <xref:Metalama.Testing.UnitTesting.UnitTestClass>.

```cs
public class MyTests : UnitTestClass { }
```

### Step 5. Create test methods

Each test method _must_ call the <xref:Metalama.Testing.UnitTesting.UnitTestClass.CreateTestContext*> and _must_ dispose of the context at the end of the test method.

Your test would typically call the <xref:Metalama.Testing.UnitTesting.TestContext.CreateCompilation*?text=context.CreateCompilation> method to obtain an <xref:Metalama.Framework.Code.ICompilation>.

> [!NOTE]
> Some APIs (such as <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory>) require the execution context to be set and assigned to your compilation. To set the execution context in a test, use the <xref:Metalama.Testing.UnitTesting.TestContext.WithExecutionContext*?text=testContext.WithExecutionContext> method.

```cs
public class MyTests : UnitTestClass
{
    [Fact]
    public void SimpleTest()
    {
        // Create a test context and dispose of it at the end of the test.
        using var testContext = this.CreateTestContext();

        // Create a compilation.
        var code =
            """
            class C
            {
                void M1 () {}

                void M2()
                {
                    var x = 0;
                    x++;
                }
            }
            """;

        var compilation = testContext.CreateCompilation( code );

        // Switch the execution context to this compilation.
        using ( testContext.WithExecutionContext( compilation ) )
        {
            // Query the code model.
            var type = compilation.Types.OfName( "C" ).Single();
            var m1 = type.Methods.OfName( "M1" ).Single();

            // Here you can also call your helper classes.

            // Perform any assertion. Typically, your compile-time code would be called here.
            Assert.Equal( 0, m1.Parameters.Count );

            Assert.True( m1.ReturnType.Is( typeof(void) ) );
        }
    }
}
```

