namespace CustomExceptions;

public class NsbNotPublishedException : Exception
{
    public NsbNotPublishedException(string? message) : base(message)
    {
    }
}
