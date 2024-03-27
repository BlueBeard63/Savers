using System;

namespace Savers.Shared.Exceptions;

public class NoPrimaryKeyFound : Exception
{
    public NoPrimaryKeyFound(string message) : base(message)
    {
    }
}