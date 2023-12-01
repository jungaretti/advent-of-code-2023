using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class DayTests
{
    [Fact]
    public void Day01Part1Test()
    {
        string[] inputLines = {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet",
        };

        var puzzleSolver = new PuzzleSolver();
        var result = puzzleSolver.SolvePuzzle(1, 1, inputLines);

        Assert.Equal("142", result);
    }

    [Fact]
    public void Day01Part2Test()
    {
        string[] inputLines = {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen",
        };

        var puzzleSolver = new PuzzleSolver();
        var result = puzzleSolver.SolvePuzzle(1, 2, inputLines);

        Assert.Equal("281", result);
    }
}