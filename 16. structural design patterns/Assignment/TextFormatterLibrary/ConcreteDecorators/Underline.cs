using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary.ConcreteDecorators;

internal class Underline : TextFormatDecorator
{
	public Underline(ITextFormat format) : base(format)
	{
	}

	public override string GetFormat()
	{
		return $"{_format.GetFormat()} with underline";
	}
}
