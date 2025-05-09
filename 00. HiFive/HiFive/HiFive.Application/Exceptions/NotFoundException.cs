namespace HiFive.Application.Exceptions;
public class NotFoundException : HiFiveException
{
	public NotFoundException(string message)
		: base(message)
	{
	}
}
