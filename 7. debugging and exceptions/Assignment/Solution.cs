namespace Assignment;

class Solution
{
    private static int[] arr;

    static Solution()
    {
        arr = [10, 20, 30, 40, 50];
    }

    public static int GetAt(int index)
    {
        if (index < 0 || index >= arr.Length)
            throw new ArgumentOutOfRangeException($"Index {index} was out of range (0 to {arr.Length - 1})");

        return arr[index];
    }

    public static int GetAtAndDivideBy(int index, int divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Cannot divide by zero");

        return GetAt(index) / divisor;
    }
}
