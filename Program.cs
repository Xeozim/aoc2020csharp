// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();

// var answer = -1;
// Day1 day1 = new();
// answer = day1.runPart1Dave();
// answer = day1.runPart2Dave();
// answer = day1.runPart2Sorted();
// answer = day1.runPart1Rob();
// answer = day1.runPart2Rob();

// Day2PartOne day2PartOne = new();
// day2PartOne.Test();
// answer = day2PartOne.Run();

// Day2PartTwo day2PartTwo = new();
// day2PartTwo.Test();
// answer = day2PartTwo.Run();

// Day3PartOne day3PartOne = new();
// day3PartOne.Test();
// answer = day3PartOne.Run();

Day3PartTwo day3PartTwo = new();
day3PartTwo.Test();
var answer = day3PartTwo.Run();

Console.WriteLine($"Answer: {answer}");

stopwatch.Stop();
Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");