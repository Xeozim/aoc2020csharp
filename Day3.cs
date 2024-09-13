// Day 1
/* Puzzle Description
--- Day 1: Report Repair ---
With the toboggan login problems resolved, you set off toward the airport. While travel by toboggan
might be easy, it's certainly not safe: there's very minimal steering and the area is covered in
trees. You'll need to see which angles will take you near the fewest trees.

Due to the local geology, trees in this area only grow on exact integer coordinates in a grid. You
make a map (your puzzle input) of the open squares (.) and trees (#) you can see. For example:

..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#

These aren't the only trees, though; due to something you read about once involving arboreal
genetics and biome stability, the same pattern repeats to the right many times:

..##.........##.........##.........##.........##.........##.......  --->
#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....  --->
.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........#.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...##....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#  --->

You start on the open square (.) in the top-left corner and need to reach the bottom (below the
bottom-most row on your map).

The toboggan can only follow a few specific slopes (you opted for a cheaper model that prefers
rational numbers); start by counting all the trees you would encounter for the slope right 3, down 1:

From your starting position at the top-left, check the position that is right 3 and down 1.
Then, check the position that is right 3 and down 1 from there, and so on until you go past the
bottom of the map.

The locations you'd check in the above example are marked here with O where there was an open square
and X where there was a tree:

..##.........##.........##.........##.........##.........##.......  --->
#..O#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....X..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#O#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..X...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.X#.......#.##.......#.##.......#.##.......#.##.....  --->
.#.#.#....#.#.#.#.O..#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........X.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.X#...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...#X....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...X.#.#..#...#.#.#..#...#.#.#..#...#.#  --->

In this example, traversing the map using this slope would cause you to encounter 7 trees.

Starting at the top-left corner of your map and following a slope of right 3 and down 1, how many
trees would you encounter?

Your puzzle answer was 268.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
Time to check the rest of the slopes - you need to minimize the probability of a sudden arboreal
stop, after all.

Determine the number of trees you would encounter if, for each of the following slopes, you start at
the top-left corner and traverse the map all the way to the bottom:

Right 1, down 1.
Right 3, down 1. (This is the slope you already checked.)
Right 5, down 1.
Right 7, down 1.
Right 1, down 2.

In the above example, these slopes would find 2, 7, 3, 4, and 2 tree(s) respectively;
multiplied together, these produce the answer 336.

What do you get if you multiply together the number of trees encountered on each of the listed slopes?

Your puzzle answer was 3093068400.

*/

// TODO: make an interface for common day methods
abstract class Day3 : Day
{
    // Test data
    // TODO: Look at NUnit or similar test framework
    protected override IEnumerable<char[]> TestInput { get; } = 
@"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#".Split("\n").Select(x => x.ToArray());

    protected override int DayNumber { get; } = 3;

    protected override string InputFilepath => "Day3Input.txt";

    protected ulong GetTreesOnSlope(int xStep, int yStep)
    {
        var repeatWidth = inputLines.Last().Length; // Use last because then no /r or /n
        var (x, y) = (0, 0);
        ulong treeCount = 0;
        var yMax = inputLines.Count() - 1;
        // Because we're doing indexed access not iteration better to convert to an array
        var inputArr = inputLines.ToArray(); 
        
        while (y <= yMax){
            // Check for a tree at the current posistion
            if (inputArr[y][x] == '#'){ treeCount++; }

            // Move the y and x position and wrap x around if necessary
            x = (x + xStep) % repeatWidth;
            y += yStep;
        }

        return treeCount;
    }
}

class Day3PartOne : Day3
{
    protected override ulong TestOutput { get; } = 7;

    public Day3PartOne(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 3, Part 1");

        return GetTreesOnSlope(3,1);
    }
}


class Day3PartTwo : Day3
{
    protected override ulong TestOutput { get; } = 336;

    public Day3PartTwo(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 3, Part 2");

        var slopes = new List<(int, int)>
        {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2),
        };

        // Multipying answers together so start at 1
        ulong answer = 1;
        for (int i = 0; i < slopes.Count; i++)
        {
            var (xStep, yStep) = slopes[i];
            answer *= GetTreesOnSlope(xStep, yStep);
            if (IsTestMode){
                Console.WriteLine($"Slope {i}, answer so far: {answer}");
            }
        }

        return answer;
    }
}