using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class PuzzleSolverTests
{
    private IEnumerable<string> day1InputLines = new string[] {
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000",
    };

    [Fact]
    public void Day01Part1Test()
    {
        var puzzleSolver = new PuzzleSolver();
        var result = puzzleSolver.SolvePuzzle(1, 1, day1InputLines);
        Assert.Equal("24000", result);
    }

    [Fact]
    public void Day01Part2Test()
    {
        var puzzleSolver = new PuzzleSolver();
        var result = puzzleSolver.SolvePuzzle(1, 1, day1InputLines);
        Assert.Equal("45000", result);
    }
}