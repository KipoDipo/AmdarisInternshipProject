using Assignment.TextFormatterLibrary.Contracts;

namespace Assignment.TextFormatterLibrary;

internal abstract class TextFormatDecorator : ITextFormat
{
	protected ITextFormat _format;

	protected TextFormatDecorator(ITextFormat format)
	{
		_format = format;
	}

	public abstract string GetFormat();
}
