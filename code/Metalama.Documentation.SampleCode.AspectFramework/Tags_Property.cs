﻿// This is public domain Metalama sample code.

using System;

namespace Doc.Tags_Property;

[TagsAspect]
internal class Foo
{
    private int _a, _b;

    public int Sum => this._a + this._b;
}