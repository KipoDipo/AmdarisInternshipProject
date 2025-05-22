namespace HiFive.Application.Exceptions;
public class BadOperationException : HiFiveException
{
	public BadOperationException(string message)
		: base(message)
	{
	}
}
