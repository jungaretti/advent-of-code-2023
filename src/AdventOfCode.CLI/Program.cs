using System.CommandLine;

namespace AdventOfCode.CLI;

class Program
{
    private static Action<int, int> rootCommandHandler = (day, part) => {
        Console.WriteLine("Advent of Code 2023");
        Console.WriteLine($"Solving day {day} part {part}");

        // var solver = new PuzzleSolver(new PuzzleHandler[]
        // {
        //     // Add all puzzle handlers here
        //     new Day00Part1(File.ReadAllLines("input/day00.txt")),
        //     new Day00Part2(File.ReadAllLines("input/day00.txt")),
        // });
        // string answer = await solver.Solve(day, part);
        var answer = "42";

        Console.WriteLine();
        Console.WriteLine($"Answer: {answer}");
    };

    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Advent of Code 2023");

        var dayArgument = new Argument<int>(
            name: "day",
            description: "The day to run."
        );
        rootCommand.AddArgument(dayArgument);

        var partArgument = new Argument<int>(
            name: "part",
            description: "The part to run."
        );
        rootCommand.AddArgument(partArgument);

        rootCommand.SetHandler(rootCommandHandler, dayArgument, partArgument);

        return await rootCommand.InvokeAsync(args);
    }
}
