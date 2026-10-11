// Warning LAMA0901 on `IMyInterface`: `The 'IMyInterface' interface can only be implemented by the 'InternalOnlyImplement' project or by another project that has access to its internals.`
namespace Doc.Architecture.InternalOnlyImplement;
internal class TheImplementation : IMyInterface
{
}