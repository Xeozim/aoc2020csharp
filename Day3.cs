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
}

class Day3PartOne : Day3
{
    protected override int TestOutput { get; } = 7;

    public Day3PartOne(): base() {}

    public override int Run()
    {
        Console.WriteLine("Day 3, Part 1");

        var xStep = 3;
        var repeatWidth = inputLines.Last().Length; // Use last because then no /r or /n
        var yStep = 1;
        var (x, y) = (0, 0);
        var treeCount = 0;
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