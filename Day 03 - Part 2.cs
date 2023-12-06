int inputCount = File.ReadLines("Input.txt").Count();
int gearRatioSum = 0;
string thisLine;
string foundNumber = "";

List<Gear> gears = new();
List<PartNumber> partNumbers = new();

for (int i = 0; i < inputCount; i++)
{
    thisLine = File.ReadLines("Input.txt").Skip(i).Take(1).First();
    int indexStart = 0;

    for (int j = 0; j < thisLine.Length; j++)
    {        
        if (thisLine[j] == '*') { gears.Add(new(i, j)); }
        if (char.IsDigit(thisLine[j]))
        {
            if (foundNumber == "") { indexStart = j; }
            foundNumber += thisLine[j];
        }
        if (foundNumber != "")
        {
            if (j == thisLine.Length - 1 || !char.IsDigit(thisLine[j]))
            {
                partNumbers.Add(new(int.Parse(foundNumber), i, indexStart, j));
                foundNumber = "";
            }
        }
    }
}

int count = 1;
foreach (Gear gear in gears)
{
    Console.WriteLine($"{gear} #{count}:");
    Console.WriteLine($"  X Position: {gear.Row + 1}");
    Console.WriteLine($"  Y Position: {gear.Column + 1}");
    gearRatioSum += gear.AdjacencyCheck(partNumbers);
    Console.WriteLine($"New Total: {gearRatioSum}");
    count++;
}
Console.WriteLine($"Final Total: {gearRatioSum}");

class Gear
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Gear(int row, int column) { Row = row; Column = column; }

    public int AdjacencyCheck(List<PartNumber> partNumbers)
    {
        int[] _row = new[] { Row, Row - 1, Row + 1 };
        int[] _column = new[] { Column, Column - 1, Column + 1 };
        bool gearRatio = false;
        int partNumberCheck = 1;
        foreach (PartNumber partNumber in partNumbers)
        {
            List<int> indices = new();
            for (int i = 0; i < partNumber.Number.ToString().Length; i++) { indices.Add(partNumber.Column_Start + i); }
            foreach (int n in indices)
            {
                if (_row.Contains(partNumber.Row) && _column.Contains(n))
                {
                    if (partNumberCheck != 1) { gearRatio = true; }
                    partNumberCheck *= partNumber.Number;
                    Console.WriteLine($"Part Number: {partNumber.Number} - Left Index | Right Index: {partNumber.Column_Start} | {partNumber.Column_End}"); break;
                }
            }
        }
        if (gearRatio) { return partNumberCheck; }
        else { return 0; }
    }
}
class PartNumber
{
    public int Number { get; set; }
    public int Row { get; set; }
    public int Column_Start { get; set; }
    public int Column_End { get; set; }

    public PartNumber(int number, int row, int columnStart, int columnEnd)
    {
        Number = number;
        Row = row;
        Column_Start = columnStart;
        Column_End = columnEnd;
    }
}
