namespace Isu.Exceptions;
public class SearchIdException : Exception
{
    public SearchIdException(int id)
        : base($"No student with id ({id})")
    { }
}
