using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary.ConcreteDecorators;

internal class Background : TextFormatDecorator
{
	private string _background;
	
	public Background(ITextFormat format, string background) : base(format)
	{
		_background = background;
	}

	public override string GetFormat()
	{
		return $"{_format.GetFormat()} with {_background} background";
	}
}