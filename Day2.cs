using System.Linq;

// Day 2
/* Puzzle Description
--- Day 2: Password Philosophy ---
Your flight departs in a few days from the coastal airport; the easiest way down to the coast from 
here is via toboggan.

The shopkeeper at the North Pole Toboggan Rental Shop is having a bad day. "Something's wrong with
our computers; we can't log in!" You ask if you can take a look.

Their password database seems to be a little corrupted: some of the passwords wouldn't have been
allowed by the Official Toboggan Corporate Policy that was in effect when they were chosen.

To try to debug the problem, they have created a list (your puzzle input) of passwords (according to
the corrupted database) and the corporate policy when that password was set.

For example, suppose you have the following list:

1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc

Each line gives the password policy and then the password. The password policy indicates the lowest
and highest number of times a given letter must appear for the password to be valid.
For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.

In the above example, 2 passwords are valid.
The middle password, cdefg, is not; it contains no instances of b, but needs at least 1.
The first and third passwords are valid: they contain one a or nine c, both within the limits of
their respective policies.

How many passwords are valid according to their policies?
*/

// TODO: make an interface for common day methods
class Day2
{
    protected class PasswordPolicy(char requiredCharacter, int minCount, int maxCount)
    {
        readonly char requiredCharacter = requiredCharacter;
        readonly int minCount = minCount;
        readonly int maxCount = maxCount;

        public bool validatePassword(string password){
            if (password == null){ return false; }
            // Count occurences of required character and check if within bounds
            var occurences = password.Where(x => x == requiredCharacter).Count();
            return occurences >= minCount && occurences <= maxCount;
        }

        // TODO: Try using ReadOnlySpan
    }

    protected class Password
    {
        readonly string password;
        readonly PasswordPolicy policy;

        public Password(string corruptedPassword){
            var mainParts = corruptedPassword.Split(":");
            password = mainParts[mainParts.Length - 1].Trim();
            var policyParts = mainParts[0].Split(" ");
            var requiredCharacter = policyParts[policyParts.Length - 1];
            var countParts = policyParts[0].Split("-");
            var minCount = int.Parse(countParts[0]);
            var maxCount = int.Parse(countParts[1]);
            policy = new(requiredCharacter.ToCharArray()[0], minCount, maxCount);
        }

        public bool isValid(){
            return policy.validatePassword(password);
        }
    }

    protected IEnumerable<string> inputLines;

    // Protected because the base class for Day doesn't implement the solution
    protected Day2()
    {
        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        inputLines = File.ReadAllLines("Day2Input.txt");
    }
}

class Day2PartOne : Day2
{
    // Test data
    // TODO: Look at NUnit or similar test framework
    readonly IEnumerable<string> testInput = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".Split("\n");

    readonly int testOutput = 2;

    public Day2PartOne(): base()
    {

    }

    public bool test()
    {
        // Overwrite the input with the test input
        inputLines = testInput;

        // Check the test output matches the expected value
        var result = run();

        var pass = result == testOutput;

        Console.WriteLine($"Result   : {result}");
        Console.WriteLine($"Expected : {testOutput}");
        Console.WriteLine($"Pass     : {pass}");

        return pass;
    }

    public int run()
    {
        Console.WriteLine("Day 2, Part 1");

        // Dumb way, iterate over all the passwords and check if they are valid
        var passwords = inputLines.Select(x => new Password(x));

        return passwords.Where(x => x.isValid()).Count();
    }
}