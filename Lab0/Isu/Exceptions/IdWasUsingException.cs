using System;

namespace Isu.Exceptions;

public class IdWasUsingException : Exception
{
    public IdWasUsingException(int id)
        : base($"this id {id} was using")
    { }
}