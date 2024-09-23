namespace aoc2020;

// Base Day class
public abstract class Day
{
    protected IEnumerable<char[]> inputLines = [];

    protected abstract ulong TestOutput { get; }
    protected abstract IEnumerable<char[]> TestInput { get; }
    protected abstract int DayNumber { get; }
    protected abstract string InputFilepath { get; }

    protected bool IsTestMode = false;

    // Protected because the base class for Day doesn't implement the solution
    protected Day() { ReadInput(); }

    protected void ReadInput()
    {
        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        inputLines = File.ReadAllLines(InputFilepath).Select(x => x.ToArray());
    }

    public virtual bool Test()
    {
        // Test mode setup
        IsTestMode = true;
        // Overwrite the input with the test input
        var realInput = inputLines;
        inputLines = TestInput;

        // Check the test output matches the expected value
        var result = Run();

        var pass = result == TestOutput;

        Console.WriteLine($"Result   : {result}");
        Console.WriteLine($"Expected : {TestOutput}");
        Console.WriteLine($"Pass     : {pass}");

        // Rervert test mode setup
        IsTestMode = false;
        inputLines = realInput;

        return pass;
    }

    public abstract ulong Run();
}