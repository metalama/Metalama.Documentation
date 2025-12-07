---
uid: wpf-command
summary: Generate WPF command boilerplate automatically with the [Command] aspect.
keywords: WPF command, ICommand interface, CanExecute method, boilerplate code, Metalama.Patterns.Wpf.CommandAttribute, ICommand.Execute method, INotifyPropertyChanged, CanExecuteChanged event, DelegateCommand class, Metalama
level: 100
created-date: 2024-11-06
modified-date: 2025-11-30
---

# WPF commands

In WPF, a command is an object that implements the <xref:System.Windows.Input.ICommand> interface. You can bind commands to UI controls like buttons to trigger actions and enable or disable these controls based on the <xref:System.Windows.Input.ICommand.CanExecute*> method. The <xref:System.Windows.Input.ICommand.Execute*> method runs the command, while the <xref:System.Windows.Input.ICommand.CanExecuteChanged> event notifies when the command's availability changes.

Implementing WPF commands manually requires substantial boilerplate code, especially to support the <xref:System.Windows.Input.ICommand.CanExecuteChanged> event.

The <xref:Metalama.Patterns.Wpf.CommandAttribute?text=[Command]> aspect automatically generates WPF command boilerplate. When you apply it to a method, the aspect generates a command property. It can also bind to a `CanExecute` property or method and integrates with <xref:System.ComponentModel.INotifyPropertyChanged>.

## Generating a WPF command property from a method

To generate a WPF command property from a method:

1. Add the [Metalama.Patterns.Wpf](https://www.nuget.org/packages/Metalama.Patterns.Wpf) package to your project.
2. Add the <xref:Metalama.Patterns.Wpf.CommandAttribute?text=[Command]> attribute to the method that executes when the command is invoked. This method becomes the implementation of the <xref:System.Windows.Input.ICommand.Execute*?text=ICommand.Execute> interface method. It must have one of the following signatures, where `T` is an arbitrary type:

    ```csharp
    [Command]
    void Execute();

    [Command( Background = true )]
    void Execute(CancellationToken);  // Only for background commands. See below.

    [Command]
    void Execute(T);

    [Command( Background = true )]
    void Execute(T, CancellationToken);  // Only for background commands. See below.

    [Command]
    Task ExecuteAsync();

    [Command]
    Task ExecuteAsync(CancellationToken);

    [Command]
    Task ExecuteAsync(T);

    [Command]
    Task ExecuteAsync(T, CancellationToken);

    ```

3. Make the class `partial` to enable referencing the generated command properties from C# or WPF source code.

### Example: simple commands

The following example implements a window with two commands: `Increment` and `Decrement`. The <xref:Metalama.Patterns.Wpf.CommandAttribute?text=[Command]> aspect generates two properties, `IncrementCommand` and `DecrementCommand`, assigned to an instance of the <xref:Metalama.Patterns.Wpf.DelegateCommand> helper class. This class accepts a delegate to the `Increment` or `Decrement` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/Commands/SimpleCommand.cs]

## Adding a CanExecute method or property

In addition to the <xref:System.Windows.Input.ICommand.Execute*> method, you can supply an implementation of <xref:System.Windows.Input.ICommand.CanExecute*?text=ICommand.CanExecute>. This implementation can be either a `bool` property or, when the `Execute` method has a parameter, a method that accepts the same parameter type and returns `bool`.

Associate a `CanExecute` implementation with the `Execute` member in one of two ways:

- **Implicitly**, by following naming conventions. For a command named `Foo`, the `CanExecute` member can be named `CanFoo`, `CanExecuteFoo`, or `IsFooEnabled`. To customize these naming conventions, see the section below.
- **Explicitly**, by setting the <xref:Metalama.Patterns.Wpf.CommandAttribute.CanExecuteMethod> or <xref:Metalama.Patterns.Wpf.CommandAttribute.CanExecuteProperty> property of the <xref:Metalama.Patterns.Wpf.CommandAttribute>.

When the `CanExecute` member is a property and the declaring type implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface, the <xref:System.Windows.Input.ICommand.CanExecuteChanged?text=ICommand.CanExecuteChanged> event is raised whenever the `CanExecute` property changes. Use the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect to implement <xref:System.ComponentModel.INotifyPropertyChanged>. For details, see <xref:observability>.

### Example: commands with a CanExecute property and implicit association

The following example demonstrates two commands, `Increment` and `Decrement`, coupled to properties that determine whether these commands are available: `CanIncrement` and `CanDecrement`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/Commands/CanExecute.cs]

### Example: commands with a CanExecute property and explicit association

This example is identical to the preceding one, but uses the <xref:Metalama.Patterns.Wpf.CommandAttribute.CanExecuteProperty> property to explicitly associate the `CanExecute` property with the `Execute` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/Commands/CanExecute_Explicit.cs]

### Example: commands with a CanExecute property and [Observable]

The following example demonstrates the code generated when you use the `[Command]` and `[Observable]` aspects together. Notice the compact source code compared to the substantial generated code.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/Commands/CanExecute_Observable.cs]

## Async commands

When the `Execute` method returns a `Task`, the `[Command]` aspect implements an asynchronous command, meaning the <xref:System.Windows.Input.ICommand.Execute*?text=ICommand.Execute> method returns immediately (after the first non-synchronous `await`). The aspect generates a property of type <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand>, which implements <xref:System.ComponentModel.INotifyPropertyChanged> and exposes the following members:

- The <xref:Metalama.Patterns.Wpf.BaseAsyncDelegateCommand.ExecutionTask> property returns the task representing the last execution of the command.
- The <xref:Metalama.Patterns.Wpf.BaseAsyncDelegateCommand.Cancel*> method allows canceling the current task.
- The <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand.CanExecute>, <xref:Metalama.Patterns.Wpf.BaseAsyncDelegateCommand.CanCancel>, <xref:Metalama.Patterns.Wpf.BaseAsyncDelegateCommand.IsCancellationRequested>, and <xref:Metalama.Patterns.Wpf.BaseAsyncDelegateCommand.IsRunning> properties expose the state of the command.

By default, the <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand.CanExecute> property returns `false` if the previous call of the <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand.Execute*> method is still running. To allow concurrent execution, set the <xref:Metalama.Patterns.Wpf.CommandAttribute.SupportsConcurrentExecution?text=CommandAttribute.SupportsConcurrentExecution> property to `true`.

To track and cancel concurrent executions of the command, subscribe to the <xref:Metalama.Patterns.Wpf.BaseAsyncDelegateCommand.Executed> event and use the <xref:Metalama.Patterns.Wpf.DelegateCommandExecution> object.

## Background commands

By default, the command implementation method executes in the foreground thread. Dispatch execution to a background thread by setting the <xref:Metalama.Patterns.Wpf.CommandAttribute.Background?text=CommandAttribute.Background> property to `true`. This works for implementation methods returning both `void` and `Task`.

In both cases, the `[Command]` aspect generates a property of type <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand>.

## Customizing naming conventions

The preceding examples rely on the default naming convention, which is based on the following assumptions:

- The command name is obtained by trimming the `Execute` method name (the one with the `[Command]` aspect) from:
  - Prefixes: `_`, `m_`, and `Execute`
  - Suffixes: `_`, `Command`, and `Async`
- Given a command name `Foo` determined by the previous step:
  - The command property is named `FooCommand`.
  - The `CanExecute` command or method can be named `CanFoo`, `CanExecuteFoo`, or `IsFooEnabled`.

Modify this naming convention by calling the <xref:Metalama.Patterns.Wpf.Configuration.CommandExtensions.ConfigureCommand*> fabric extension method, then <xref:Metalama.Patterns.Wpf.Configuration.CommandOptionsBuilder.AddNamingConvention*?text=builder.AddNamingConvention>, and supplying an instance of the <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention> class.

If specified, the <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention.CommandNamePattern?text=CommandNamingConvention.CommandNamePattern> is a regular expression that matches the command name from the name of the main method. If unspecified, the default matching algorithm is used. The <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention.CanExecutePatterns> property is a list of patterns used to select the `CanExecute` property or method, and the <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention.CommandPropertyName> property is a pattern that generates the name of the generated command property. In <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention.CanExecutePatterns> and <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention.CommandPropertyName>, the `{CommandName}` substring is replaced by the command name returned by <xref:Metalama.Patterns.Wpf.Configuration.CommandNamingConvention.CommandNamePattern>.

Naming conventions are evaluated by priority order. The default priority is the order in which you added the convention. Override it by supplying a value to the `priority` parameter.

The default naming convention is evaluated last and can't be modified.

### Example: Czech naming conventions

The following example illustrates a naming convention for the Czech language. There are two conventions. The first matches the `Vykonat` prefix in the main method—for instance, it matches a method named `VykonatBlb` and returns `Blb` as the command name. The second naming convention matches everything and removes the conventional prefixes `_` and `Execute` as described previously. The default naming convention isn't used in this example.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/Commands/CanExecute_Czech.cs]

> [!div class="see-also"]
> <xref:wpf>
> <xref:wpf-dependency-property>
> <xref:observability>
