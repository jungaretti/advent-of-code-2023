using System.CommandLine;

namespace advent_of_code_2023;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var dayArgument = new Argument<int>(
            name: "day",
            description: "The day to run."
        );

        var partArgument = new Argument<int>(
            name: "part",
            description: "The part to run."
        );

        var rootCommand = new RootCommand("Advent of Code 2023");
        rootCommand.AddArgument(dayArgument);
        rootCommand.AddArgument(partArgument);

        rootCommand.SetHandler((day, part) =>
            {
                Console.WriteLine($"Day: {day}");
                Console.WriteLine($"Part: {part}");
            },
            dayArgument,
            partArgument);

        return await rootCommand.InvokeAsync(args);
    }
}
