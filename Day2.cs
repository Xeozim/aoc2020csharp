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

Your puzzle answer was 378.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
While it appears you validated the passwords correctly, they don't seem to be what the Official
Toboggan Corporate Authentication System is expecting.

The shopkeeper suddenly realizes that he just accidentally explained the password policy rules from
his old job at the sled rental place down the street! The Official Toboggan Corporate Policy
actually works a little differently.

Each policy actually describes two positions in the password, where 1 means the first character, 2
means the second character, and so on. (Be careful; Toboggan Corporate Policies have no concept of
"index zero"!) Exactly one of these positions must contain the given letter. Other occurrences of
the letter are irrelevant for the purposes of policy enforcement.

Given the same example list from above:

1-3 a: abcde is valid: position 1 contains a and position 3 does not.
1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.
How many passwords are valid according to the new interpretation of the policies?

Your puzzle answer was 280.

*/

class Password
{
    protected interface IPasswordPolicy{
        abstract public bool ValidatePassword(string password);
    }

    protected class PasswordPolicySledRental(char requiredCharacter, int minCount, int maxCount) : IPasswordPolicy
    {
        readonly char requiredCharacter = requiredCharacter;
        readonly int minCount = minCount;
        readonly int maxCount = maxCount;

        public bool ValidatePassword(string password){
            if (password == null){ return false; }
            // Count occurences of required character and check if within bounds
            var occurences = password.Where(x => x == requiredCharacter).Count();
            return occurences >= minCount && occurences <= maxCount;
        }

        // TODO: Try using ReadOnlySpan
    }
    protected class PasswordPolicyTobogganCorp(char requiredCharacter, int pos1, int pos2) : IPasswordPolicy
    {
        readonly char requiredCharacter = requiredCharacter;
        readonly int pos1 = pos1;
        readonly int pos2 = pos2;

        public bool ValidatePassword(string password){
            if (password == null || password.Length < pos2 || password.Length < pos1){ return false; }
            
            var pos1valid = password[pos1 - 1] == requiredCharacter;
            var pos2valid = password[pos2 - 1] == requiredCharacter;

            return pos1valid ^ pos2valid;
        }

        // TODO: Try using ReadOnlySpan
    }

    readonly string password;
    readonly IPasswordPolicy policy;

    // Gotta be a better way...
    public Password(string corruptedPassword, string policyType){
        var mainParts = corruptedPassword.Split(":");
        password = mainParts[mainParts.Length - 1].Trim();
        var policyParts = mainParts[0].Split(" ");
        var requiredCharacter = policyParts[policyParts.Length - 1];
        var countParts = policyParts[0].Split("-");
        var num1 = int.Parse(countParts[0]);
        var num2 = int.Parse(countParts[1]);
        policy = CreatePolicy(requiredCharacter.ToCharArray()[0], num1, num2, policyType);
    }

    static IPasswordPolicy CreatePolicy(char requiredCharacter, int num1, int num2, string policyType){
        return policyType switch
        {
            "SledRental" => new PasswordPolicySledRental(requiredCharacter, num1, num2),
            "TobogganCorp" => new PasswordPolicyTobogganCorp(requiredCharacter, num1, num2),
            _ => throw new ArgumentException("Unknown policy type"),
        };
    }

    public bool IsValid(){
        return policy.ValidatePassword(password);
    }
}

// TODO: make an interface for common day methods
abstract class Day2
{
    protected IEnumerable<string> inputLines;

    // Test data
    // TODO: Look at NUnit or similar test framework
    readonly IEnumerable<string> testInput = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".Split("\n");

    protected abstract int TestOutput { get; }

    // Protected because the base class for Day doesn't implement the solution
    protected Day2()
    {
        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        inputLines = File.ReadAllLines("Day2Input.txt");
    }

    public bool Test()
    {
        // Overwrite the input with the test input
        inputLines = testInput;

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

class Day2PartOne : Day2
{
    protected override int TestOutput { get; } = 2;

    static readonly string POLICY_TYPE = "SledRental";

    public Day2PartOne(): base() {}

    public override int Run()
    {
        Console.WriteLine("Day 2, Part 1");

        // Dumb way, iterate over all the passwords and check if they are valid
        var passwords = inputLines.Select(x => new Password(x, POLICY_TYPE));

        return passwords.Where(x => x.IsValid()).Count();
    }
}

class Day2PartTwo : Day2
{
    protected override int TestOutput { get; } = 1;

    static readonly string POLICY_TYPE = "TobogganCorp";

    public Day2PartTwo(): base() {}

    public override int Run()
    {
        Console.WriteLine("Day 2, Part 2");

        // Dumb way, iterate over all the passwords and check if they are valid
        var passwords = inputLines.Select(x => new Password(x, POLICY_TYPE));

        var valid = passwords.Select(x => x.IsValid());

        return valid.Where(x => x).Count();
    }
}