using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary.ConcreteDecorators;

internal class Color : TextFormatDecorator
{
	private string _color;

	public Color(ITextFormat format, string color) : base(format)
	{
		_color = color;
	}

	public override string GetFormat()
	{
		return $"{_format.GetFormat()} with {_color} color";
	}
}
