using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class PuzzleProviderTests
{
    [Fact]
    public void ProviderFindsInputAndSolver()
    {
        var input = new string[] { "test" };

        var mockSolver = new Mock<IPuzzleStrategy>();
        mockSolver.SetupGet(s => s.Day).Returns(1);
        mockSolver.SetupGet(s => s.Part).Returns(1);
        mockSolver.Setup(s => s.SolvePuzzle(input)).Returns("success");

        var provider = new PuzzleSolver(new IPuzzleStrategy[] { mockSolver.Object });

        var answer = provider.SolvePuzzle(1, 1, input);
        Assert.Equal("success", answer);
    }
}