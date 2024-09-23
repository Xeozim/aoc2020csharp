namespace aoc2020.test;

public class TestDay1
{
    const string TEST_INPUT = 
@"1721
979
366
299
675
1456";

    // Runs before every test
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestDay1Part1()
    {
        Day1 day1 = new()
        {
            InputValues = TEST_INPUT.Split("\n").Select(int.Parse)
        };

        var answer = day1.answerPart1Dave();
        
        Assert.That(answer, Is.EqualTo(514579));
    }
}