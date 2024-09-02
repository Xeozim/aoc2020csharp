using System.Linq;

// Day 1

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