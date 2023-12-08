class Helpers
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

    public static long LeastCommonMultiple(long a, long b)
    {
        return a / GreatestCommonDivisor(a, b) * b;
    }
}