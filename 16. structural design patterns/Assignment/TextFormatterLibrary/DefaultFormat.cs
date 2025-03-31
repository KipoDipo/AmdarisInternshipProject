using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary;

internal class DefaultFormat : ITextFormat
{
	public string GetFormat()
	{
		return "A text";
	}
}
