// Day 5
/* Puzzle Description
--- Day 5: Binary Boarding ---
You board your plane only to discover a new problem: you dropped your boarding pass! You aren't sure
which seat is yours, and all of the flight attendants are busy with the flood of people that
suddenly made it through passport control.

You write a quick program to use your phone's camera to scan all of the nearby boarding passes (your
puzzle input); perhaps you can find your seat through process of elimination.

Instead of zones or groups, this airline uses binary space partitioning to seat people.
A seat might be specified like FBFBBFFRLR, where F means "front", B means "back",
L means "left", and R means "right".

The first 7 characters will either be F or B; these specify exactly one of the 128 rows on the plane
(numbered 0 through 127). Each letter tells you which half of a region the given seat is in.
Start with the whole list of rows; the first letter indicates whether the seat is in the
front (0 through 63) or the back (64 through 127). The next letter indicates which half of that
region the seat is in, and so on until you're left with exactly one row.

For example, consider just the first seven characters of FBFBBFFRLR:

Start by considering the whole range, rows 0 through 127.
F means to take the lower half, keeping rows 0 through 63.
B means to take the upper half, keeping rows 32 through 63.
F means to take the lower half, keeping rows 32 through 47.
B means to take the upper half, keeping rows 40 through 47.
B keeps rows 44 through 47.
F keeps rows 44 through 45.
The final F keeps the lower of the two, row 44.

The last three characters will be either L or R; these specify exactly one of the 8 columns of seats
on the plane (numbered 0 through 7). The same process as above proceeds again, this time with only
three steps. L means to keep the lower half, while R means to keep the upper half.

For example, consider just the last 3 characters of FBFBBFFRLR:

Start by considering the whole range, columns 0 through 7.
R means to take the upper half, keeping columns 4 through 7.
L means to take the lower half, keeping columns 4 through 5.
The final R keeps the upper of the two, column 5.

So, decoding FBFBBFFRLR reveals that it is the seat at row 44, column 5.

Every seat also has a unique seat ID: multiply the row by 8, then add the column.
In this example, the seat has ID 44 * 8 + 5 = 357.

Here are some other boarding passes:

BFFFBBFRRR: row 70, column 7, seat ID 567.
FFFBBBFRRR: row 14, column 7, seat ID 119.
BBFFBBFRLL: row 102, column 4, seat ID 820.

As a sanity check, look through your list of boarding passes. What is the highest seat ID on a
boarding pass?

Your puzzle answer was 926.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
Ding! The "fasten seat belt" signs have turned on. Time to find your seat.

It's a completely full flight, so your seat should be the only missing boarding pass in your list.
However, there's a catch: some of the seats at the very front and back of the plane don't exist on
this aircraft, so they'll be missing from your list as well.

Your seat wasn't at the very front or back, though; the seats with IDs +1 and -1 from yours will be
in your list.

What is the ID of your seat?

Your puzzle answer was 657.

Both parts of this puzzle are complete! They provide two gold stars: **
*/

namespace aoc2020;

// Represents seating information
public class SeatInfo{
    private readonly int _row = 0;
    public int Row { get => _row; }
    private readonly int _column = 0;
    public int Column { get => _column; }

    private const int ROW_SPEC_LENGTH = 7;
    private const int COL_SPEC_LENGTH = 3;

    // NB: integer values:
    // 'F' = 70
    // 'B' = 66
    // 'R' = 82
    // 'L' = 76

    /*
    We want to convert each spec to it's binary equivalent e.g.
    FBFBBFF => 0101100 in binary => 44 in decimal
        RLR => 0000101 in binary =>  5 in decimal
    */
    public SeatInfo(ReadOnlySpan<char> specString){
        var rowSpec = specString[..ROW_SPEC_LENGTH];
        var colSpec = specString[ROW_SPEC_LENGTH..];

        // Console.WriteLine($"rowSpec: {rowSpec}");
        // Console.WriteLine($"colSpec: {colSpec}");

        // Parse the rowSpec into binary, assume anything that isn't B is F
        // var rowBinary = new char[8];
        // var colBinary = new char[3];
        for (int i=0; i<ROW_SPEC_LENGTH; i++) {
            _row += rowSpec[i] == 'B' ? 1 << ROW_SPEC_LENGTH-1-i : 0;
            // rowBinary[i] = rowSpec[i] == 'B' ? '1' : '0';
            if (i<COL_SPEC_LENGTH){
                _column += colSpec[i] == 'R' ? 1 << COL_SPEC_LENGTH-1-i : 0;
                // colBinary[i] = colSpec[i] == 'R' ? '1' : '0';
            }
        }

        // Console.WriteLine($"Row (binary)   : {new string(rowBinary)}");
        // Console.WriteLine($"Row            : {Row}");
        // Console.WriteLine($"Column (binary): {new string(colBinary)}");
        // Console.WriteLine($"Column         : {Column}");
    }

    public int Id {
        get {
            return (Row * 8) + Column;
        }
    }

    public override string ToString()
    {
        return $"Seat in row {Row} and Column {Column} with Id {Id}";
    }
}

// TODO: make an interface for common day methods
public abstract class Day5 : Day
{
    protected override IEnumerable<char[]> TestInput { get; } = [[.. "FBFBBFFRLR"]];
    protected override ulong TestOutput { get; } = 357;

    protected override int DayNumber { get; } = 5;

    protected override string InputFilepath => "Day5Input.txt";

    protected IEnumerable<SeatInfo> seats = [];

    protected void LoadInput()
    {
        seats = inputLines.Select(x => new SeatInfo(x));

        if (IsTestMode){
            foreach (var (line, idx) in inputLines.Select((line, idx) => (line, idx))){
                Console.WriteLine($"Input: {new string(line)}");
                Console.WriteLine($"Seat : {seats.ElementAt(idx)}");
            }
        }
    }
}

public class Day5PartOne : Day5
{
    public Day5PartOne(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 5 Part One");
        LoadInput();

        var seatWithHighestId = seats.OrderBy(x=>x.Id).Last();
        return seatWithHighestId == null ? 0 : (ulong)seatWithHighestId.Id;
    }
}

public class Day5PartTwo : Day5
{
    public Day5PartTwo(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 5 Part Two");
        LoadInput();

        var seatWithLowestId = seats.OrderBy(x=>x.Id).First();

        if (seatWithLowestId == null){
            throw new Exception("Invalid input");
        } else {
            // Sort the seats by ID and find the first gap
            var lastId = seatWithLowestId.Id;
            foreach (var seat in seats.OrderBy(x=>x.Id))
            {
                if (seat.Id - lastId > 1){
                    return (ulong) lastId + 1;
                } else {
                    lastId = seat.Id;
                }
            }
        }

        throw new Exception("Invalid input");
    }

    public override bool Test(){
        throw new NotImplementedException("Day 5 Part Two has no tests");
    }
}