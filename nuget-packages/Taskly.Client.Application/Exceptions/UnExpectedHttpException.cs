namespace Taskly.Client.Application.Exceptions
{
    public class UnExpectedHttpException(string message) : Exception(message)
    {
    }
}
