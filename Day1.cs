using System.Linq;

// Day 1
/* Puzzle Description
--- Day 1: Report Repair ---
After saving Christmas five years in a row, you've decided to take a vacation at a nice resort on a tropical island. Surely, Christmas will go on without you.

The tropical island has its own currency and is entirely cash-only. The gold coins used there have a little picture of a starfish; the locals just call them stars. None of the currency exchanges seem to have heard of them, but somehow, you'll need to find fifty of these coins by the time you arrive so you can pay the deposit on your room.

To save your vacation, you need to get all fifty stars by December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

Before you leave, the Elves in accounting just need you to fix your expense report (your puzzle input); apparently, something isn't quite adding up.

Specifically, they need you to find the two entries that sum to 2020 and then multiply those two numbers together.

For example, suppose your expense report contained the following:

1721
979
366
299
675
1456
In this list, the two entries that sum to 2020 are 1721 and 299. Multiplying them together produces 1721 * 299 = 514579, so the correct answer is 514579.

Of course, your expense report is much larger. Find the two entries that sum to 2020; what do you get if you multiply them together?

Your puzzle answer was 878724.

--- Part Two ---
The Elves in accounting are thankful for your help; one of them even offers you a starfish coin they had left over from a past vacation. They offer you a second one if you can find three numbers in your expense report that meet the same criteria.

Using the above example again, the three entries that sum to 2020 are 979, 366, and 675. Multiplying them together produces the answer, 241861950.

In your expense report, what is the product of the three entries that sum to 2020?

Your puzzle answer was 201251610.
*/

// TODO: make an interface for common day methods
class Day1
{

    IEnumerable<int> inputValues;

    public Day1()
    {
        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        inputValues = File.ReadAllLines("Day1Input.txt").Select(int.Parse);
    }

    public int answerPart1Dave()
    {
        Console.WriteLine("Day 1, Part 1 (Dave's Version)");

        var inputArr = inputValues.ToArray();

        for (int i=0; i<inputArr.Length; i++){
            for (int j=1; j<inputArr.Length; j++){
                var iVal = inputArr[i];
                var jVal = inputArr[j];

                var sum = iVal + jVal;
                if (sum == 2020){
                    return iVal * jVal;
                }
            }
        }

        return -1; // didn't work
    }

    public int runPart2Dave()
    {
        Console.WriteLine("Day 1, Part 2 (Dave's Version)");

        var inputArr = inputValues.ToArray();

        for (int i=0; i<inputArr.Length; i++){
            for (int j=1; j<inputArr.Length; j++){
                for (int k=1; k<inputArr.Length; k++){
                    var iVal = inputArr[i];
                    var jVal = inputArr[j];
                    var kVal = inputArr[k];

                    var sum = iVal + jVal + kVal;
                    if (sum == 2020){
                        return iVal * jVal * kVal;
                    }
                }
            }
        }

        return -1; // didn't work
    }

    /*
    Fancy approach: Sort the list before traversal.

    With a sorted list we can break out of an inner loop whenever the sum of the values is greater than 2020.
    This should be much faster on average
    */ 
    public int runPart2Sorted()
    {
        var inputArr = inputValues.Order().ToArray();

        for (int i=0; i<inputArr.Length; i++){
            var iVal = inputArr[i];
            for (int j=1; j<inputArr.Length; j++){
                var jVal = inputArr[j];
                for (int k=1; k<inputArr.Length; k++){
                    var kVal = inputArr[k];

                    var sum = iVal + jVal + kVal;
                    if (sum == 2020){
                        return iVal * jVal * kVal;
                    } else if (sum > 2020){
                        break;
                    }
                }
                
                if (iVal + jVal > 2020){
                    break;
                }
            }
            
            if (iVal > 2020){
                break;
            }
        }

        return -1; // didn't work
    }

    public int runPart1Rob()
    {
        Console.WriteLine("Day 1, Part 1 (Rob's Version)");

        var calculations = inputValues
            .SelectMany((x,i) => inputValues
                .Where((y,j) => i != j)
                .Select(y => (A : x, B : y, Total : x+y)));


        var twentyTwenty = calculations.First(x => x.Total == 2020);

        return twentyTwenty.A * twentyTwenty.B;
    }

    public int runPart2Rob()
    {
        Console.WriteLine("Day 1, Part 2 (Rob's Version)");

        var calculations = inputValues
            .SelectMany((x,i) => inputValues
                .SelectMany((y,j) => inputValues
                .Where((z,k) => i != j && j != k)
                .Select(z => (A : x, B : y, C : z, Total : x+y+z))));
        
        var twentyTwenty = calculations.First(x => x.Total == 2020);

        return twentyTwenty.A * twentyTwenty.B * twentyTwenty.C;
    }

}