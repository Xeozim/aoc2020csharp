// Day 6
/* Puzzle Description
--- Day 6: Custom Customs ---
As your flight approaches the regional airport where you'll switch to a much larger plane, customs
declaration forms are distributed to the passengers.

The form asks a series of 26 yes-or-no questions marked a through z. All you need to do is identify
the questions for which anyone in your group answers "yes". Since your group is just you, this
doesn't take very long.

However, the person sitting next to you seems to be experiencing a language barrier and asks if you
can help. For each of the people in their group, you write down the questions for which they answer
"yes", one per line. For example:

abcx
abcy
abcz

In this group, there are 6 questions to which anyone answered "yes": a, b, c, x, y, and z.
(Duplicate answers to the same question don't count extra; each question counts at most once.)

Another group asks for your help, then another, and eventually you've collected answers from every
group on the plane (your puzzle input). Each group's answers are separated by a blank line, and
within each group, each person's answers are on a single line. For example:

abc

a
b
c

ab
ac

a
a
a
a

b

This list represents answers from five groups:

The first group contains one person who answered "yes" to 3 questions: a, b, and c.
The second group contains three people; combined, they answered "yes" to 3 questions: a, b, and c.
The third group contains two people; combined, they answered "yes" to 3 questions: a, b, and c.
The fourth group contains four people; combined, they answered "yes" to only 1 question, a.
The last group contains one person who answered "yes" to only 1 question, b.
In this example, the sum of these counts is 3 + 3 + 3 + 1 + 1 = 11.

For each group, count the number of questions to which anyone answered "yes". What is the sum of
those counts?

Your puzzle answer was 6703.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
As you finish the last group's customs declaration, you notice that you misread one word in the
instructions:

You don't need to identify the questions to which anyone answered "yes"; you need to identify the
questions to which everyone answered "yes"!

Using the same example as above:

abc

a
b
c

ab
ac

a
a
a
a

b

This list represents answers from five groups:

In the first group, everyone (all 1 person) answered "yes" to 3 questions: a, b, and c.
In the second group, there is no question to which everyone answered "yes".
In the third group, everyone answered yes to only 1 question, a. Since some people did not answer
"yes" to b or c, they don't count.
In the fourth group, everyone answered yes to only 1 question, a.
In the fifth group, everyone (all 1 person) answered "yes" to 1 question, b.
In this example, the sum of these counts is 3 + 0 + 1 + 1 + 1 = 6.

For each group, count the number of questions to which everyone answered "yes".
What is the sum of those counts?

Your puzzle answer was 3430.

Both parts of this puzzle are complete! They provide two gold stars: **
*/

namespace aoc2020;

public abstract class Day6 : Day
{
    protected override IEnumerable<char[]> TestInput { get; } = 
@"abc

a
b
c

ab
ac

a
a
a
a

b".Split("\n").Select(x => x.ToArray());

    protected override int DayNumber { get; } = 6;

    protected override string InputFilepath => "C:/xeozim/src/csharp/aoc2020/aoc2020/inputs/Day6Input.txt";
}

public class Day6PartOne : Day6
{
    protected override ulong TestOutput { get; } = 11;

    public Day6PartOne(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 6 Part One");
        
        // Parse the responses into a list of hashsets
        // Each hashset represents the responses of a group of passengers
        var responseGroups = new List<HashSet<char>>();

        var currentResponses = new HashSet<char>();
        
        var inputArr = inputLines.ToArray();
        var lineCount = inputLines.Count();

        for (int i=0; i<lineCount; i++)
        {
            // Try to read data from the current line
            var line = (ReadOnlySpan<char>) inputArr[i];

            foreach (var c in line.Trim()) { currentResponses.Add(c); }
            
            // When we hit an empty line or the final line, add the latest response set to the list
            if (line.IsWhiteSpace() || i == lineCount-1){
                responseGroups.Add(currentResponses);
                
                currentResponses = [];
            }
        }

        if (IsTestMode){
            foreach(var responseGroup in responseGroups)
            {
                Console.WriteLine($"Response group with {responseGroup.Count} unique responses:");
                foreach (var response in responseGroup){
                    Console.WriteLine($"\t{response}");
                }
            }
        }

        return (ulong) responseGroups.Select(response => response.Count).Sum();
    }
}

public class Day6PartTwo : Day6
{
    protected override ulong TestOutput { get; } = 6;

    public Day6PartTwo(): base() {}

    // Count how many questions everyone in a group answered yes to
    private int GroupResponseCount(List<HashSet<char>> responseGroup){
        HashSet<char> groupResponses = [.. "abcdefghijklmnopqrstuvwxyz"];

        foreach (var passengerResponse in responseGroup)
        {
            groupResponses.IntersectWith(passengerResponse);
        }
        
        return groupResponses.Count;
    }

    public override ulong Run()
    {
        Console.WriteLine("Day 6 Part Two");
        
        // Parse the responses into a list of lists of hashsets
        // Each hashset represents the responses of a single passenger in a group
        // Each mid-level list represents the responses of a group of passengers
        var responseGroups = new List<List<HashSet<char>>>();

        var currentResponses = new List<HashSet<char>>();
        
        var inputArr = inputLines.ToArray();
        var lineCount = inputLines.Count();

        for (int i=0; i<lineCount; i++)
        {
            // Try to read data from the current line
            var line = (ReadOnlySpan<char>) inputArr[i];
            
            // When we hit an empty line, add the latest response set to the list
            if (line.IsWhiteSpace())
            {
                if (currentResponses.Count > 0) { responseGroups.Add(currentResponses); }
                
                currentResponses = [];
            } else
            {
                // Not an empty line - add passenger data
                var passengerResponse = new HashSet<char>();
                foreach (var c in line.Trim()) { passengerResponse.Add(c); }

                currentResponses.Add(passengerResponse);
            }
        }
        // Add the final response
        if (currentResponses.Count > 0) { responseGroups.Add(currentResponses); }

        if (IsTestMode){
            foreach(var responseGroup in responseGroups)
            {
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"Response group with {responseGroup.Count} passengers:");
                foreach (var passengerResponse in responseGroup){
                    Console.WriteLine($"\tPassenger with {passengerResponse.Count} responses:");
                    foreach (var response in passengerResponse){
                        Console.WriteLine($"\t\t{response}");
                    }
                }
                Console.WriteLine($"Combined response count: {GroupResponseCount(responseGroup)}");
            }
        }

        return (ulong) responseGroups.Select(rg => GroupResponseCount(rg)).Sum();
    }
}
