namespace AdventOfCode.Tests;

public class PuzzleProviderTests
{
    [Fact]
    public async Task ProviderFindsInputAndSolver()
    {
        var input = new string[] { "test" };

        var mockSolver = new Mock<PuzzleSolver>();
        mockSolver.SetupGet(s => s.Day).Returns(1);
        mockSolver.SetupGet(s => s.Part).Returns(1);
        mockSolver.Setup(s => s.SolveAsync(input)).ReturnsAsync("success");

        var provider = new PuzzleProvider(new PuzzleSolver[] { mockSolver.Object });

        var answer = await provider.SolveAsync(1, 1, input);
        Assert.Equal("success", answer);
    }
}