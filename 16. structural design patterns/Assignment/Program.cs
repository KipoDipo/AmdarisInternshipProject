using Assignment.TextFormatterLibrary;

namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
			TextFormatter formatter = new TextFormatter("Hello World", new DefaultFormat());

			formatter.AddColor("Red");
			formatter.AddBackground("Yellow");
			formatter.AddBold();
			formatter.AddItalic();
			//formatter.AddUnderline();

			formatter.Print();
		}
    }
}
