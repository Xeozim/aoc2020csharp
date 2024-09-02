using System.Linq;

// Day 1

// TODO: make an interface for common day methods
class Day1
{
    public Day1()
    {
    }

    public int runPart1Dave()
    {
        Console.WriteLine("Day 1, Part 1 (Dave's Version)");

        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        var problemInput = File.ReadAllLines("Day1Input.txt").Select(int.Parse).ToArray();

        for (int i=0; i<problemInput.Length; i++){
            for (int j=1; j<problemInput.Length; j++){
                var iVal = problemInput[i];
                var jVal = problemInput[j];

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

        // ReadAllLines returns an array of strings
        // Each entry in the string array is a single line
        // Convert everything to an integer up front
        var problemInput = File.ReadAllLines("Day1Input.txt").Select(int.Parse).ToArray();

        for (int i=0; i<problemInput.Length; i++){
            for (int j=1; j<problemInput.Length; j++){
                for (int k=1; k<problemInput.Length; k++){
                    var iVal = problemInput[i];
                    var jVal = problemInput[j];
                    var kVal = problemInput[k];

                    var sum = iVal + jVal + kVal;
                    if (sum == 2020){
                        return iVal * jVal * kVal;
                    }
                }
            }
        }

        return -1; // didn't work
    }

    public int runPart1Rob()
    {
        Console.WriteLine("Day 1, Part 1 (Rob's Version)");

        var values = File.ReadAllLines("Day1Input.txt").Select(x => int.Parse(x));

        var calculations = values
            .SelectMany((x,i) => values
                .Where((y,j) => i != j)
                .Select(y => (A : x, B : y, Total : x+y)));


        var twentyTwenty = calculations.First(x => x.Total == 2020);

        return twentyTwenty.A * twentyTwenty.B;
    }

    public int runPart2Rob()
    {
        Console.WriteLine("Day 1, Part 2 (Rob's Version)");

        var values = File.ReadAllLines("Day1Input.txt").Select(x => int.Parse(x));

        var calculations = values
            .SelectMany((x,i) => values
                .SelectMany((y,j) => values
                .Where((z,k) => i != j && j != k)
                .Select(z => (A : x, B : y, C : z, Total : x+y+z))));
        
        var twentyTwenty = calculations.First(x => x.Total == 2020);

        return twentyTwenty.A * twentyTwenty.B * twentyTwenty.C;
    }

}