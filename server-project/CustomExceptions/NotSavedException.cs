namespace CustomExceptions;

public class NotSavedException : Exception
{
    public NotSavedException(string? message) : base(message)
    {
    }
}
