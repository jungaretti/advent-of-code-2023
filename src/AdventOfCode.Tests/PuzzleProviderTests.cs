using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class PuzzleProviderTests
{
    [Fact]
    public void PuzzleSolverFindsStrategy()
    {
        const int MOCK_DAY = 1;
        const int MOCK_PART = 2;
        const string MOCK_ANSWER = "success";

        var input = new string[] { "test" };

        var mockSolver = new Mock<IPuzzleStrategy>();
        mockSolver.SetupGet(s => s.Day).Returns(MOCK_DAY);
        mockSolver.SetupGet(s => s.Part).Returns(MOCK_PART);
        mockSolver.Setup(s => s.SolvePuzzle(input)).Returns(MOCK_ANSWER);

        var provider = new PuzzleSolver(new IPuzzleStrategy[] { mockSolver.Object });

        var answer = provider.SolvePuzzle(MOCK_DAY, MOCK_PART, input);
        Assert.Equal(MOCK_ANSWER, answer);
    }
}