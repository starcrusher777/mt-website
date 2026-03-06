namespace MT.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string resourceName, object key) 
        : base($"{resourceName} with id '{key}' was not found.") { }
}
