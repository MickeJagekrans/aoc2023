var lines = File.ReadAllLines("../../3.input");

var acc = 0;

for (int i = 0; i < lines.Length; i++)
{
    var currentLine = lines[i];

    if (!currentLine.Contains('*'))
    {
        continue;
    }

    var linesToCheck = GetLinesToCheck(lines, i);

    for (int starIndex = 0; starIndex < currentLine.Length; starIndex++)
    {
        if (currentLine[starIndex] != '*')
        {
            continue;
        }

        var adjacentNumbers = GetAdjacentNumbers(linesToCheck, starIndex);

        // Only 2 adjacent numbers are allowed, otherwise this is not a gear
        if (adjacentNumbers.Count != 2)
        {
            continue;
        }

        acc += adjacentNumbers[0] * adjacentNumbers[1];
    }
}

Console.WriteLine(acc);

static int GetFullNumber(string line, int index)
{
    var numberStartIndex = index;
    var numberEndIndex = index;

    // Find number start
    while (numberStartIndex > 0 && char.IsDigit(line[numberStartIndex - 1]))
    {
        numberStartIndex--;
    }

    // Find number end 
    while (numberEndIndex < line.Length - 1 && char.IsDigit(line[numberEndIndex + 1]))
    {
        numberEndIndex++;
    }

    return int.Parse(line[numberStartIndex..(numberEndIndex + 1)]);
}

static string[] GetLinesToCheck(string[] lines, int i)
{
    var prevLineIndex = Math.Max(i - 1, 0);
    var nextLineIndex = Math.Min(i + 1, lines.Length - 1);

    return lines[prevLineIndex..(nextLineIndex + 1)];
}

static List<int> GetAdjacentNumbers(string[] linesToCheck, int starIndex)
{
    var adjacentNumbers = new List<int>();

    foreach (var line in linesToCheck)
    {
        // Only 2 adjacent numbers are allowed, otherwise this is not a gear
        if (adjacentNumbers.Count > 2)
        {
            break;
        }

        var startPos = Math.Max(starIndex - 1, 0);
        var endPos = Math.Min(starIndex + 1, line.Length - 1);

        for (int j = startPos; j <= endPos; j++)
        {
            if (!char.IsDigit(line[j]))
            {
                continue;
            }

            var nextIndex = Math.Min(j + 1, endPos);

            // Check if we're at the end of the line or the next char is not a digit
            if (j == endPos || !char.IsDigit(line[nextIndex]))
            {
                // This is the end of the number, get the full number
                var fullNumber = GetFullNumber(line, j);

                adjacentNumbers.Add(fullNumber);
            }
        }
    }

    return adjacentNumbers;
}