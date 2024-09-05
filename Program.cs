// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();

// Day1 day1 = new();
// var answer = day1.runPart1Dave();
// var answer = day1.runPart2Dave();
// var answer = day1.runPart2Sorted();
// var answer = day1.runPart1Rob();
// var answer = day1.runPart2Rob();

Day2PartOne day2PartOne = new();
// day2PartOne.test();
var answer = day2PartOne.run();

Console.WriteLine($"Answer: {answer}");

stopwatch.Stop();
Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");