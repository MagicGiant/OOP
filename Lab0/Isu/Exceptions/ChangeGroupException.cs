using Isu.Entities;

namespace Isu.Exceptions;

public class ChangeGroupException : Exception
{
    public ChangeGroupException(int id)
        : base($"Student with id ({id}) already on changed group")
    { }
}