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

        var mockStrategy = new Mock<IPuzzleStrategy>();
        mockStrategy.SetupGet(s => s.Day).Returns(MOCK_DAY);
        mockStrategy.SetupGet(s => s.Part).Returns(MOCK_PART);
        mockStrategy.Setup(s => s.SolvePuzzle(input)).Returns(MOCK_ANSWER);

        var provider = new PuzzleSolver(new IPuzzleStrategy[] { mockStrategy.Object });

        var answer = provider.SolvePuzzle(MOCK_DAY, MOCK_PART, input);
        Assert.Equal(MOCK_ANSWER, answer);
    }

    [Fact]
    public void PuzzleSolverThrowsWithDuplicateStrategies()
    {
        const int MOCK_DAY = 1;
        const int MOCK_PART = 2;
        const string MOCK_ANSWER = "success";

        var input = new string[] { "test" };

        var mockStrategy = new Mock<IPuzzleStrategy>();
        mockStrategy.SetupGet(s => s.Day).Returns(MOCK_DAY);
        mockStrategy.SetupGet(s => s.Part).Returns(MOCK_PART);
        mockStrategy.Setup(s => s.SolvePuzzle(input)).Returns(MOCK_ANSWER);

        var mockDuplicateStrategy = new Mock<IPuzzleStrategy>();
        mockDuplicateStrategy.SetupGet(s => s.Day).Returns(MOCK_DAY);
        mockDuplicateStrategy.SetupGet(s => s.Part).Returns(MOCK_PART);
        mockDuplicateStrategy.Setup(s => s.SolvePuzzle(input)).Returns(MOCK_ANSWER);

        var provider = new PuzzleSolver(new IPuzzleStrategy[] {
            mockStrategy.Object,
            mockDuplicateStrategy.Object,
        });

        Assert.ThrowsAny<InvalidOperationException>(() => provider.SolvePuzzle(MOCK_DAY, MOCK_PART, input));
    }
}