using System.Text.RegularExpressions;

namespace AdventOfCode;

partial class Day19 : IPuzzleDay
{
    public int Day => 19;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<Workflow> workflows = ParseWorkflows(inputLines.TakeWhile(line => line != string.Empty));
        IEnumerable<Part> parts = ParseParts(inputLines.SkipWhile(line => line != string.Empty).Skip(1));

        var acceptedParts = parts.Where(part => RunWorkflow(workflows, "in", part) == "A");
        var acceptedPartsSums = acceptedParts.Select(SumPart);
        var answer = acceptedPartsSums.Sum();

        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private int SumPart(Part part)
    {
        return part.X + part.M + part.A + part.S;
    }

    private string RunWorkflow(IEnumerable<Workflow> workflows, string workflowId, Part part)
    {
        Dictionary<string, Workflow> workflowDictionary = workflows.ToDictionary(w => w.Id);

        Workflow currentWorkflow = workflowDictionary[workflowId];
        do
        {
            string workflowResult = RunRules(currentWorkflow, part);
            if (workflowResult == "A" || workflowResult == "R")
            {
                return workflowResult;
            }

            currentWorkflow = workflowDictionary[workflowResult];
        } while (true);
    }

    private string RunRules(Workflow workflow, Part parts)
    {
        string? ruleResult = null;
        foreach (var rule in workflow.Rules)
        {
            ruleResult = rule(parts);
            if (ruleResult != null)
            {
                return ruleResult;
            }
        }

        throw new Exception($"Workflow did not terminate: {workflow.Id}");
    }

    [GeneratedRegex(@"(\w+){(.+)}")]
    private static partial Regex GetWorkflowRegex();

    private IEnumerable<Workflow> ParseWorkflows(IEnumerable<string> inputLines)
    {
        foreach (string line in inputLines)
        {
            var match = GetWorkflowRegex().Match(line);
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

    [GeneratedRegex(@"(\w+)([<\\>])(\d+):(\w+)")]
    private static partial Regex GetComplexRuleRegex();

    [GeneratedRegex(@"(\w+)")]
    private static partial Regex GetSimpleRuleRegex();

    private Func<Part, string?> ParseRule(string rule)
    {
        var complexMatch = GetComplexRuleRegex().Match(rule);
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

        var simpleMatch = GetSimpleRuleRegex().Match(rule);
        if (simpleMatch.Success)
        {
            return _ => simpleMatch.Groups[1].Value;
        }

        throw new Exception($"Failed to parse rule: {rule}");
    }

    [GeneratedRegex(@"{x=(\d+),m=(\d+),a=(\d+),s=(\d+)}")]
    private static partial Regex GetPartRegex();

    private IEnumerable<Part> ParseParts(IEnumerable<string> inputLines)
    {
        foreach (string line in inputLines)
        {
            var match = GetPartRegex().Match(line);
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