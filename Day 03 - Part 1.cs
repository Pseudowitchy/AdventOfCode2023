int inputCount = File.ReadLines("Input.txt").Count();
int partNumberTotal = 0;
string previousLine = "";
string thisLine;
string nextLine;
string foundNumber = "";

for (int i = 0; i < inputCount; i++)
{
    thisLine = File.ReadLines("Input.txt").Skip(i).Take(1).First();
    if (i < inputCount - 1)
    {
        nextLine = File.ReadLines("Input.txt").Skip(i + 1).Take(1).First();
    }
    else { nextLine = ""; }

    int indexStart = 0;

    for (int j = 0; j < thisLine.Length; j++)
    {        
        if (char.IsDigit(thisLine[j]))
        {
            if (foundNumber == "") { indexStart = j - 1; }
            foundNumber += thisLine[j];
        }
        if (foundNumber != "")
        {
            if (j == thisLine.Length - 1 || !char.IsDigit(thisLine[j]))
            {
                string snippet = SnippetMaker(indexStart, Math.Clamp(j, 0, thisLine.Length));
                snippet = snippet.Trim('.');
                if (snippet.Length > 0)
                {
                    partNumberTotal += int.Parse(foundNumber);
                    Console.WriteLine($"New total: {partNumberTotal}");
                }
                foundNumber = "";
            }
        }
    }
    previousLine = thisLine;
}

Console.WriteLine($"Final Total: {partNumberTotal}");

string SnippetMaker(int indexStart, int indexEnd)
{
    string snippet = "";
    for (int k = Math.Clamp(indexStart, 0, 2000); k <= indexEnd; k++)
    {
        if (!char.IsDigit(thisLine[k])) { snippet += thisLine[k]; }
        if (previousLine != "" && !char.IsDigit(previousLine[k])) { snippet += previousLine[k]; }
        if (nextLine != "" && !char.IsDigit(nextLine[k])) { snippet += nextLine[k]; }
    }
    return snippet;
}