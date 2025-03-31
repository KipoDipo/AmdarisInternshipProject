using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary.ConcreteDecorators;

internal class Italic : TextFormatDecorator
{
	public Italic(ITextFormat format) : base(format)
	{
	}

	public override string GetFormat()
	{
		return $"{_format.GetFormat()} with italic";
	}
}
