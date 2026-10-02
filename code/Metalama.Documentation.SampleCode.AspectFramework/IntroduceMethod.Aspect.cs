// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.IntroduceMethod;

internal class ToStringAttribute : TypeAspect
{
    [Introduce]
    private readonly int _id = IdGenerator.GetId();

    [Introduce( WhenExists = OverrideStrategy.Override )]
    public override string ToString() => $"{this.GetType().Name} Id={this._id}";
}