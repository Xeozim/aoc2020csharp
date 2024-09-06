// Base Day class
abstract class Day
{
    protected IEnumerable<char[]> inputLines;

    protected abstract int TestOutput { get; }
    protected abstract IEnumerable<char[]> TestInput { get; }
    protected abstract int DayNumber { get; }
    protected abstract string InputFilepath { get; }

    // Protected because the base class for Day doesn't implement the solution
    protected Day()
    {
        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        inputLines = File.ReadAllLines(InputFilepath).Select(x => x.ToArray());
    }

    public bool Test()
    {
        // Overwrite the input with the test input
        inputLines = TestInput;

        // Check the test output matches the expected value
        var result = Run();

        var pass = result == TestOutput;

        Console.WriteLine($"Result   : {result}");
        Console.WriteLine($"Expected : {TestOutput}");
        Console.WriteLine($"Pass     : {pass}");

        return pass;
    }

    public abstract int Run();
}