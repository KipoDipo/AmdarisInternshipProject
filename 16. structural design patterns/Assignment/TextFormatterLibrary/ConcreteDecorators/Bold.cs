using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary.ConcreteDecorators;

internal class Bold : TextFormatDecorator
{
	public Bold(ITextFormat format) : base(format)
	{
	}

	public override string GetFormat()
	{
		return $"{_format.GetFormat()} with bold";
	}
}
