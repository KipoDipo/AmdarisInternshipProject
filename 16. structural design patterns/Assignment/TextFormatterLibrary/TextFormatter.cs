using Assignment.TextFormatterLibrary.ConcreteDecorators;
using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary;
internal class TextFormatter
{
	private ITextFormat _format;
	private string _text;

	public TextFormatter(string text, ITextFormat defaultFormat)
	{
		_text = text;
		_format = defaultFormat;
	}

	public void AddBold()
	{
		_format = new Bold(_format);
	}

	public void AddItalic()
	{
		_format = new Italic(_format);
	}

	public void AddUnderline()
	{
		_format = new Underline(_format);
	}

	public void AddColor(string color)
	{
		_format = new Color(_format, color);
	}

	public void AddBackground(string background)
	{
		_format = new Background(_format, background);
	}

	public void ResetFormat(ITextFormat defaultFormat)
	{
		_format = defaultFormat;
	}

	public void Print()
	{
		Console.WriteLine($"\"{_text}\" formatted as: {_format.GetFormat()}");
	}
}
