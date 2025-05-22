namespace HiFive.Application.Exceptions;
public class UserInputException : HiFiveException
{
	public UserInputException(string message)
		: base(message)
	{
	}
}
