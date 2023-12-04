namespace AdventOfCode.Tests;

public class DayTests
{
    private PuzzleSolver puzzleSolver;

    public DayTests()
    {
        puzzleSolver = new PuzzleSolver();
    }

    [Fact]
    public void Day01Part1Test()
    {
        string[] inputLines = {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet",
        };

        var answer = puzzleSolver.SolvePuzzle(1, 1, inputLines);
        Assert.Equal("142", answer);
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

        var answer = puzzleSolver.SolvePuzzle(1, 2, inputLines);
        Assert.Equal("281", answer);
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

        var answer = puzzleSolver.SolvePuzzle(2, 1, inputLines);
        Assert.Equal("8", answer);
    }

    [Fact]
    public void Day02Part2Test()
    {
        string[] inputLines = {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };

        var answer = puzzleSolver.SolvePuzzle(2, 2, inputLines);
        Assert.Equal("2286", answer);
    }

    [Fact]
    public void Day03Part1Test()
    {
        string[] inputLines = {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
        };

        var answer = puzzleSolver.SolvePuzzle(3, 1, inputLines);
        Assert.Equal("4361", answer);

        inputLines = [
            "12.......*..",
            "+.........34",
            ".......-12..",
            "..78........",
            "..*....60...",
            "78..........",
            ".......23...",
            "....90*12...",
            "............",
            "2.2......12.",
            ".*.........*",
            "1.1.......56",
        ];

        answer = puzzleSolver.SolvePuzzle(3, 1, inputLines);
        Assert.Equal("413", answer);

        inputLines = [
            "12.......*..",
            "+.........34",
            ".......-12..",
            "..78........",
            "..*....60...",
            "78.........9",
            ".5.....23..$",
            "8...90*12...",
            "............",
            "2.2......12.",
            ".*.........*",
            "1.1..503+.56",
        ];

        answer = puzzleSolver.SolvePuzzle(3, 1, inputLines);
        Assert.Equal("925", answer);
    }

    [Fact]
    public void Day03Part2Test()
    {
        string[] inputLines = {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
        };

        var answer = puzzleSolver.SolvePuzzle(3, 2, inputLines);
        Assert.Equal("467835", answer);

        inputLines = [
            "12.......*..",
            "+.........34",
            ".......-12..",
            "..78........",
            "..*....60...",
            "78..........",
            ".......23...",
            "....90*12...",
            "............",
            "2.2......12.",
            ".*.........*",
            "1.1.......56",
        ];

        answer = puzzleSolver.SolvePuzzle(3, 2, inputLines);
        Assert.Equal("6756", answer);

        inputLines = [
            "12.......*..",
            "+.........34",
            ".......-12..",
            "..78........",
            "..*....60...",
            "78.........9",
            ".5.....23..$",
            "8...90*12...",
            "............",
            "2.2......12.",
            ".*.........*",
            "1.1..503+.56",
        ];

        answer = puzzleSolver.SolvePuzzle(3, 2, inputLines);
        Assert.Equal("6756", answer);
    }
}