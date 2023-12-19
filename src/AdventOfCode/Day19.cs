
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day19 : IPuzzleDay
{
    public int Day => 19;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<Workflow> workflows = ParseWorkflows(inputLines.TakeWhile(line => line != string.Empty));
        IEnumerable<Part> parts = ParseParts(inputLines.SkipWhile(line => line != string.Empty).Skip(1));

        return "0";
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private IEnumerable<Workflow> ParseWorkflows(IEnumerable<string> inputLines)
    {
        var workflowRegex = new Regex(@"(\w+){(.+)}");
        foreach (string line in inputLines)
        {
            var match = workflowRegex.Match(line);
            if (match.Success == false)
            {
                throw new Exception($"Failed to parse workflow: {line}");
            }

            yield return new Workflow(
                Id: match.Groups[1].Value,
                Rules: match.Groups[2].Value.Split(",").Select(ParseRule).ToList()
            );
        }
    }

    private Func<Part, string?> ParseRule(string rule)
    {
        var complexRuleRegex = new Regex(@"(\w+)([<\\>])(\d+):(\w+)");
        var complexMatch = complexRuleRegex.Match(rule);
        if (complexMatch.Success)
        {
            string propertyName = complexMatch.Groups[1].Value.ToUpper();
            string operatorSymbol = complexMatch.Groups[2].Value;
            int threshold = int.Parse(complexMatch.Groups[3].Value);
            string result = complexMatch.Groups[4].Value;

            return (Part part) =>
            {
                int propertyValue = (int)typeof(Part).GetProperty(propertyName)!.GetValue(part)!;
                return operatorSymbol switch
                {
                    "<" => propertyValue < threshold ? result : null,
                    ">" => propertyValue > threshold ? result : null,
                    _ => throw new Exception($"Failed to parse operator: {operatorSymbol}")
                };
            };
        }

        var simpleRuleRegex = new Regex(@"(\w+)");
        var simpleMatch = simpleRuleRegex.Match(rule);
        if (simpleMatch.Success)
        {
            return _ => simpleMatch.Groups[1].Value;
        }

        throw new Exception($"Failed to parse rule: {rule}");
    }

    private IEnumerable<Part> ParseParts(IEnumerable<string> inputLines)
    {
        var partRegex = new Regex(@"{x=(\d+),m=(\d+),a=(\d+),s=(\d+)}");
        foreach (string line in inputLines)
        {
            var match = partRegex.Match(line);
            if (match.Success == false)
            {
                throw new Exception($"Failed to parse part: {line}");
            }

            yield return new Part(
                X: int.Parse(match.Groups[1].Value),
                M: int.Parse(match.Groups[2].Value),
                A: int.Parse(match.Groups[3].Value),
                S: int.Parse(match.Groups[4].Value)
            );
        }
    }

    private record Workflow(string Id, List<Func<Part, string?>> Rules);

    private record Part(int X, int M, int A, int S);
}