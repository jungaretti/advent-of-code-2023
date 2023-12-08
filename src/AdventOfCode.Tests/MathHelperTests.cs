namespace AdventOfCode.Tests;

public class MathHelperTests
{
    [Fact]
    public void LeastCommonMultipleTest()
    {
        long[] values = [522, 5522, 334];
        Assert.Equal(240687414, MathHelpers.LeastCommonMultiple(values));
    }
}