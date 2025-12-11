# Metalama Coding Challenges

This file contains 50 coding challenges for testing knowledge of Metalama, a .NET compile-time metaprogramming framework.

---

## Basic Questions (15)

### Q1: Simple Logging Aspect
**Category**: OverrideMethodAspect
**Difficulty**: Basic

Create an aspect called `LogAttribute` that logs "Entering {MethodName}" before a method executes and "Exiting {MethodName}" after it completes, using `Console.WriteLine`. The aspect should work on any method.

---

### Q2: Null Parameter Check
**Category**: Contracts
**Difficulty**: Basic

Create a contract aspect called `NotNullAttribute` that can be applied to parameters. When the parameter value is null, it should throw an `ArgumentNullException` with the parameter name.

---

### Q3: Property Change Notification
**Category**: OverrideFieldOrPropertyAspect
**Difficulty**: Basic

Create an aspect called `NotifyAttribute` that overrides a property setter to call a method named `OnPropertyChanged(string propertyName)` after the value is set. Assume this method exists on the target class.

---

### Q4: Method Timing
**Category**: OverrideMethodAspect
**Difficulty**: Basic

Create an aspect called `TimedAttribute` that measures how long a method takes to execute and writes the elapsed time in milliseconds to the console after the method completes.

---

### Q5: String Trimming Contract
**Category**: Contracts
**Difficulty**: Basic

Create a contract aspect called `TrimAttribute` that automatically trims whitespace from string parameters or properties when they are set.

---

### Q6: Retry on Exception
**Category**: OverrideMethodAspect
**Difficulty**: Basic

Create an aspect called `RetryAttribute` that retries a method up to 3 times if it throws an exception. If all retries fail, rethrow the last exception.

---

### Q7: Introduce a Counter Field
**Category**: IntroduceAttribute
**Difficulty**: Basic

Create a type-level aspect called `CountedAttribute` that introduces a private static integer field `_instanceCount` and a public static read-only property `InstanceCount` that returns its value.

---

### Q8: Project Fabric for Logging
**Category**: Fabrics
**Difficulty**: Basic

Create a `ProjectFabric` that automatically applies a `[Log]` aspect to all public methods in the project.

---

### Q9: Range Validation Contract
**Category**: Contracts
**Difficulty**: Basic

Create a contract aspect called `RangeAttribute` that takes `Min` and `Max` integer parameters in its constructor. It should throw an `ArgumentOutOfRangeException` if the value is outside the specified range.

---

### Q10: Simple Cache Aspect
**Category**: OverrideMethodAspect
**Difficulty**: Basic

Create an aspect called `CacheAttribute` that caches the return value of a parameterless method in a private field introduced by the aspect. On subsequent calls, return the cached value instead of executing the method again.

---

### Q11: Method Authorization
**Category**: OverrideMethodAspect
**Difficulty**: Basic

Create an aspect called `AuthorizeAttribute` that takes a `Role` string parameter. Before executing the method, check if `Thread.CurrentPrincipal.IsInRole(Role)` returns true. If not, throw an `UnauthorizedAccessException`.

---

### Q12: Property Getter Logging
**Category**: OverrideFieldOrPropertyAspect
**Difficulty**: Basic

Create an aspect called `LogAccessAttribute` that logs to the console whenever a property getter is accessed, showing the property name and the value being returned.

---

### Q13: ToString Introduction
**Category**: IntroduceAttribute
**Difficulty**: Basic

Create a type-level aspect called `AutoToStringAttribute` that introduces a `ToString()` method returning the type name.

---

### Q14: Eligibility for Non-Static Methods
**Category**: Eligibility
**Difficulty**: Basic

Create an aspect called `InstanceOnlyAttribute` that can only be applied to non-static methods. Define proper eligibility rules so the IDE shows an error if applied to a static method.

---

### Q15: Suppress Unused Field Warning
**Category**: Diagnostics
**Difficulty**: Basic

Create an aspect that introduces a field and suppresses the CS0169 warning ("Field is never used") for that field.

---

## Intermediate Questions (20)

### Q16: Async Method Override
**Category**: OverrideMethodAspect, Async
**Difficulty**: Intermediate

Create an aspect called `AsyncLogAttribute` that works correctly on both synchronous and async methods. For async methods, it should log "Starting" before execution and "Completed" after the task completes (not when the method returns the Task).

---

### Q17: Observable Pattern Implementation
**Category**: ImplementInterface
**Difficulty**: Intermediate

Create an aspect called `ObservableAttribute` that implements `INotifyPropertyChanged` on a class. It should introduce the `PropertyChanged` event and an `OnPropertyChanged(string propertyName)` method.

---

### Q18: Introduce Method with Dynamic Signature
**Category**: IntroduceMethod, BuildAspect
**Difficulty**: Intermediate

Create a type-level aspect called `GenerateUpdateAttribute` that introduces an `Update` method. The method should have one parameter for each writable property in the target type, and its implementation should set each property to the corresponding parameter value.

---

### Q19: Child Aspect Pattern
**Category**: Child Aspects
**Difficulty**: Intermediate

Create two aspects: `AuditedClassAttribute` (type-level) that automatically adds `AuditedMethodAttribute` to all public methods in the class. The `AuditedMethodAttribute` should log method entry and exit.

---

### Q20: Aspect Configuration Options
**Category**: Aspect Configuration
**Difficulty**: Intermediate

Create a logging aspect with a configurable log level. Create an options class that allows setting the default log level at the project level using a fabric, which individual aspects can override.

---

### Q21: Deep Clone Implementation
**Category**: ImplementInterface
**Difficulty**: Intermediate

Create an aspect called `DeepCloneableAttribute` that implements `ICloneable`. The `Clone` method should create a new instance and copy all field values. For fields that also implement `ICloneable`, call `Clone()` on them.

---

### Q22: Namespace Fabric Validation
**Category**: Fabrics, Validation
**Difficulty**: Intermediate

Create a `NamespaceFabric` that validates all types in its namespace end with "Service" if they implement any interface ending with "Service".

---

### Q23: Disposable Pattern
**Category**: ImplementInterface, IntroduceMethod
**Difficulty**: Intermediate

Create an aspect called `AutoDisposeAttribute` that implements `IDisposable`. The `Dispose` method should call `Dispose()` on all fields that implement `IDisposable`.

---

### Q24: Exception Wrapping
**Category**: OverrideMethodAspect
**Difficulty**: Intermediate

Create an aspect called `WrapExceptionAttribute` that takes a target exception type as a generic parameter. It should catch all exceptions and wrap them in the specified exception type, preserving the original as the inner exception.

---

### Q25: Type Fabric for Introducing Members
**Category**: TypeFabric
**Difficulty**: Intermediate

Create a `TypeFabric` inside a class that introduces 5 methods named `Method1` through `Method5`, each writing its name to the console.

---

### Q26: Aspect Ordering
**Category**: Aspect Ordering
**Difficulty**: Intermediate

You have two aspects: `CacheAttribute` and `LogAttribute`. Configure the aspect ordering so that at runtime, logging happens first (outermost), then caching. Write the correct `[assembly: AspectOrder(...)]` attribute.

---

### Q27: Conditional Aspect Application
**Category**: Eligibility, Fabrics
**Difficulty**: Intermediate

Create a fabric that applies a `[Trace]` aspect only to methods that have at least one parameter and return a non-void type.

---

### Q28: Contract with Custom Exception
**Category**: Contracts
**Difficulty**: Intermediate

Create a contract called `NotEmptyAttribute` for strings that throws a custom `ValidationException` (that you define) with a message including the parameter name when the string is null or empty.

---

### Q29: Introduce Property with Lazy Initialization
**Category**: IntroduceAttribute
**Difficulty**: Intermediate

Create an aspect that introduces a property with lazy initialization. The property should use `Lazy<T>` internally and only compute the value on first access.

---

### Q30: Validation After All Aspects
**Category**: Validation
**Difficulty**: Intermediate

Create an aspect that validates (after all aspects are applied) that the target class contains a field named `_logger` of type `ILogger`. Report an error if this field doesn't exist.

---

### Q31: Using InterpolatedStringBuilder
**Category**: Builders, Templates
**Difficulty**: Intermediate

Create an aspect that introduces a `ToDebugString()` method that returns an interpolated string containing the name and value of all public properties, formatted as "PropName=Value, PropName2=Value2".

---

### Q32: Reference Validation
**Category**: Validation
**Difficulty**: Intermediate

Create an aspect called `TestOnlyAttribute` that can be applied to methods. It should report a warning when the method is called from any namespace that doesn't contain ".Tests.".

---

### Q33: Caching with Key Generation
**Category**: Caching
**Difficulty**: Intermediate

Create a caching aspect that generates a cache key from all method parameters. Use `string.Join` to combine parameter values into a key string.

---

### Q34: Initializer Injection
**Category**: Initializers
**Difficulty**: Intermediate

Create an aspect that adds initialization code to run before any constructor. The code should set a `_createdAt` field (that you introduce) to `DateTime.UtcNow`.

---

### Q35: Transitive Project Fabric
**Category**: Fabrics
**Difficulty**: Intermediate

Create a `TransitiveProjectFabric` that automatically applies a `[Serializable]` attribute to all classes in any project that references the current project.

---

## Advanced Questions (15)

### Q36: Generic Type Constraints in Templates
**Category**: Templates, Type System
**Difficulty**: Advanced

Create an aspect that introduces a generic method `T CloneValue<T>(T value) where T : ICloneable`. The method should call `Clone()` and cast the result back to `T`. Handle the compile-time type constraints correctly.

---

### Q37: Aspect State Sharing
**Category**: AspectState, Tags
**Difficulty**: Advanced

Create an aspect that counts how many times it has been applied in the same project and makes this count available to other aspects through `IAspectState`. Each instance should know its own "index" in the application order.

---

### Q38: Builder Pattern Implementation
**Category**: IntroduceTypes
**Difficulty**: Advanced

Create an aspect that generates a complete Builder pattern for a class. It should introduce a nested `Builder` class with: (1) settable properties for each property of the outer class, (2) a `Build()` method that creates and returns an instance of the outer class.

---

### Q39: Expression Builder for Dynamic Validation
**Category**: ExpressionBuilder
**Difficulty**: Advanced

Create a contract aspect that accepts a list of forbidden values at compile time. Use `ExpressionBuilder` to generate a runtime check that compares the parameter against all forbidden values and throws if there's a match.

---

### Q40: Override Constructor
**Category**: Overriding Constructors
**Difficulty**: Advanced

Create an aspect that overrides constructors to add parameter validation. For each parameter, if its type is a reference type, add a null check that throws `ArgumentNullException`.

---

### Q41: Introduce Constructor Parameter with Pull Strategy
**Category**: IntroduceParameter, DI
**Difficulty**: Advanced

Create an aspect that introduces an `ILogger` parameter to the primary constructor. Use a pull strategy so that derived classes automatically receive the parameter and pass it to the base constructor.

---

### Q42: Memoization with Cache Invalidation
**Category**: Caching, Introduce
**Difficulty**: Advanced

Create a memoization aspect for properties that: (1) caches the computed value, (2) introduces a method `InvalidatePropertyCache(string propertyName)` that clears the cache for a specific property, (3) handles dependencies between properties.

---

### Q43: Dynamic Interface Implementation
**Category**: ImplementInterface, Dynamic
**Difficulty**: Advanced

Create an aspect called `DelegateToFieldAttribute` that takes an interface type as a parameter. It should implement that interface by delegating all method calls to a field of that interface type (which should be introduced if not present).

---

### Q44: Aspect Inheritance with State
**Category**: Aspect Inheritance, Serialization
**Difficulty**: Advanced

Create an inheritable aspect that, when applied to a base class, automatically applies to all derived classes. Each derived class should see cumulative state from parent aspects through the predecessor chain.

---

### Q45: Compile-Time Code Generation with ArrayBuilder
**Category**: ArrayBuilder, Templates
**Difficulty**: Advanced

Create an aspect that introduces a static `GetMethodNames()` method returning a `string[]` of all method names in the class. Use `ArrayBuilder` to construct the array expression at compile time.

---

### Q46: Auxiliary Templates with Return Values
**Category**: Auxiliary Templates
**Difficulty**: Advanced

Create a caching aspect with auxiliary templates for customization. Include overridable template methods for `CreateCacheKey()`, `OnCacheHit()`, and `OnCacheMiss()`. The base aspect should use these templates, and derived aspects should be able to override them.

---

### Q47: Decorator Pattern Generation
**Category**: IntroduceTypes, ImplementInterface
**Difficulty**: Advanced

Create an aspect that generates a decorator class for an interface. Apply it to an interface, and it should introduce a class that: (1) implements the interface, (2) has a constructor accepting an instance of the interface, (3) delegates all calls to the wrapped instance while allowing interception points.

---

### Q48: Compile-Time vs Run-Time Type Handling
**Category**: Type System, meta.RunTime
**Difficulty**: Advanced

Create an aspect that, given a compile-time `IType`, generates run-time code that: (1) uses the `Type` object to call `Activator.CreateInstance`, (2) handles generic types correctly, (3) works for types not known at the aspect's compile time.

---

### Q49: Multi-Layer Aspect with Different Templates per Layer
**Category**: Layers, Templates
**Difficulty**: Advanced

Create an aspect with multiple layers: one layer for before-execution logic, one for after-execution logic, and one for exception handling. Each layer should be independently orderable with other aspects.

---

### Q50: Custom Eligibility with Declaration Analysis
**Category**: Eligibility, Code Model
**Difficulty**: Advanced

Create an aspect that is only eligible for methods where: (1) the return type implements `IDisposable`, (2) all parameters are value types or strings, (3) the declaring type has a parameterless constructor. Implement custom eligibility using `MustSatisfy` with informative error messages for each condition.

---

## Notes for Evaluators

- Solutions should compile without errors
- Solutions should follow Metalama best practices
- Pay attention to:
  - Proper use of `meta.Proceed()` vs `meta.Target.Method.Invoke()`
  - Correct compile-time vs run-time code separation
  - Immutability of aspect classes (no target-specific state in fields)
  - Proper eligibility definitions
  - Correct use of `dynamic` typing in templates
- Trap areas to watch for:
  - Using `nameof` for introduced members (resolves at aspect compile-time, not target compile-time)
  - Forgetting `partial` keyword on classes receiving introduced members
  - Storing mutable state in aspect fields
  - Incorrect aspect ordering (build-time vs run-time order are opposite)
  - Extension methods not working on `dynamic` expressions
  - Compile-time code must be .NET Standard 2.0 compatible
