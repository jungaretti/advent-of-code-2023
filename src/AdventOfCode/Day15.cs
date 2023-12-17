namespace AdventOfCode;

class Day15 : IPuzzleDay
{
    public int Day => 15;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<string> steps = inputLines.First().Split(',');
        IEnumerable<int> hashes = steps.Select(GetHash);
        int answer = hashes.Sum();

        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        IEnumerable<string> steps = inputLines.First().Split(',');

        Box[] boxes = Enumerable.Range(0, 256).Select(_ => new Box()).ToArray();
        foreach (string item in steps)
        {
            int operationIndex = item.IndexOfAny(['=', '-']);
            char operation = item[operationIndex];
            string label = item.Substring(0, operationIndex);

            int boxIndex = GetHash(label);
            Box box = boxes[boxIndex];

            switch (operation)
            {
                case '-':
                    box.RemoveLens(label);
                    break;
                case '=':
                    int focalLength = int.Parse(item.Substring(operationIndex + 1));
                    box.AddLens(new Lens(label, focalLength));
                    break;
                default:
                    throw new Exception($"Unknown operation: {operation}");
            }
        }

        List<int> focusingPowers = [];
        for (int boxIndex = 0; boxIndex < boxes.Length; boxIndex++)
        {
            Box box = boxes[boxIndex];
            for (int lensIndex = 0; lensIndex < box.Lenses.Count; lensIndex++)
            {
                Lens lens = box.Lenses[lensIndex];
                int boxValue = boxIndex + 1;
                int lensValue = lensIndex + 1;
                int focalLength = lens.FocalLength;
                int focusingPower = boxValue * lensValue * focalLength;
                focusingPowers.Add(focusingPower);
            }
        }

        var answer = focusingPowers.Sum();
        return answer.ToString();
    }

    private class Box
    {
        public List<Lens> Lenses = new List<Lens>();

        public void AddLens(Lens newLens)
        {
            int index = Lenses.FindIndex(lens => lens.Label == newLens.Label);
            if (index >= 0)
            {
                Lenses[index] = newLens;
            }
            else
            {
                Lenses.Add(newLens);
            }
        }

        public void RemoveLens(string label)
        {
            int index = Lenses.FindIndex(lens => lens.Label == label);
            if (index >= 0)
            {
                Lenses.RemoveAt(index);
            }
        }
    }

    private record Lens(string Label, int FocalLength);

    private int GetHash(string input)
    {
        int value = 0;
        foreach (char character in input)
        {
            value += character;
            value *= 17;
            value %= 256;
        }

        return value;
    }
}