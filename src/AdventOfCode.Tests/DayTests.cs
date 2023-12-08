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

    [Fact]
    public void Day04Part1Test()
    {
        string[] inputLines = {
            "Card   1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card   2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card  34:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card  42: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 142: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 678: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
        };

        var answer = puzzleSolver.SolvePuzzle(4, 1, inputLines);
        Assert.Equal("13", answer);
    }

    [Fact]
    public void Day04Part2Test()
    {
        string[] inputLines = {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
        };

        var answer = puzzleSolver.SolvePuzzle(4, 2, inputLines);
        Assert.Equal("30", answer);
    }

    [Fact]
    public void Day05Part1Test()
    {
        string[] inputLines = {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4",
        };

        var answer = puzzleSolver.SolvePuzzle(5, 1, inputLines);
        Assert.Equal("35", answer);
    }

    [Fact]
    public void Day05Part2Test()
    {
        string[] inputLines = {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4",
        };

        var answer = puzzleSolver.SolvePuzzle(5, 2, inputLines);
        Assert.Equal("46", answer);
    }

    [Fact]
    public void Day06Part1Test()
    {
        string[] inputLines = {
            "Time:      7  15   30",
            "Distance:  9  40  200",
        };

        var answer = puzzleSolver.SolvePuzzle(6, 1, inputLines);
        Assert.Equal("288", answer);
    }

    [Fact]
    public void Day06Part2Test()
    {
        string[] inputLines = {
            "Time:      7  15   30",
            "Distance:  9  40  200",
        };

        var answer = puzzleSolver.SolvePuzzle(6, 2, inputLines);
        Assert.Equal("71503", answer);
    }

    [Fact]
    public void Day07Part1Test()
    {
        string[] inputLines = {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        };

        var answer = puzzleSolver.SolvePuzzle(7, 1, inputLines);
        Assert.Equal("6440", answer);
    }

    [Fact]
    public void Day07Part2Test()
    {
        string[] inputLines = {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        };

        var answer = puzzleSolver.SolvePuzzle(7, 2, inputLines);
        Assert.Equal("5905", answer);
    }

    [Fact]
    public void Day08Part1Test()
    {
        string[] inputLines = [
            "RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)",
        ];

        var answer = puzzleSolver.SolvePuzzle(8, 1, inputLines);
        Assert.Equal("2", answer);

        inputLines = [
            "LLR",
            "",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)",
        ];

        answer = puzzleSolver.SolvePuzzle(8, 1, inputLines);
        Assert.Equal("6", answer);
    }

    [Fact]
    public void Day08Part2Test()
    {
        string[] inputLines = [
        ];

        var answer = puzzleSolver.SolvePuzzle(8, 2, inputLines);
        Assert.Equal("", answer);
    }
}