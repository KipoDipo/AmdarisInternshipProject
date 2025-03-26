namespace Assignment;
using static Solution;

public class Program
{
    public static void Foo()
    {
        var value = 999;
        try
        {
            value = GetAtAndDivideBy(0, 0);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Console.WriteLine($"Out of range exception: {exception.Message} ");
            throw;
        }
        catch (DivideByZeroException exception)
        {
            Console.WriteLine($"Divide by zero exception: {exception.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine($"Value: {value}");
        }
    }

    static void Main(string[] args)
    {
        try
        {
            Foo();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Go fix the Foo() method...");
#if(DEBUG)
            Console.WriteLine($"Full exception:\n{exception}");
#endif
        }
    }
}
