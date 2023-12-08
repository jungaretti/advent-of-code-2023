class Helpers
{
    public static long Gcd(long x, long y)
    {
        while (y != 0)
        {
            long tmp = x % y;
            x = y;
            y = tmp;
        }

        return x;
    }

    public static long Lcm(long a, long b)
    {
        return (a / Gcd(a, b)) * b;
    }
}