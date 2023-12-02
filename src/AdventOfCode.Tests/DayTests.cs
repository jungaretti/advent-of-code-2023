using AdventOfCode;

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

    [Fact]
    public void Day02Part1Test()
    {
        string[] inputLines = {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };

        var puzzleSolver = new PuzzleSolver();
        var result = puzzleSolver.SolvePuzzle(2, 1, inputLines);

        Assert.Equal("8", result);
    }
}