public class MathHelpers
{
    public static long GreatestCommonDivisor(long x, long y)
    {
        while (y != 0)
        {
            long tmp = x % y;
            x = y;
            y = tmp;
        }

        return x;
    }

    public static long LeastCommonMultiple(long val1, long val2)
    {
        return val1 / GreatestCommonDivisor(val1, val2) * val2;
    }

    public static long LeastCommonMultiple(IEnumerable<long> values)
    {
        return values.Aggregate(LeastCommonMultiple);
    }
}