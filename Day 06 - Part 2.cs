string file = "Input.txt";
List<Race> races = new();
List<long> cleanedLine = new();
long totalValue = 1;

string[] fileContents = File.ReadAllLines(file);

foreach (string line in fileContents)
{
    string numberFound = "";
    for (int i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i])) { numberFound += line[i]; }
        if ((i == line.Length - 1)) { cleanedLine.Add(long.Parse(numberFound)); numberFound = ""; }
    }
}

for (int i = 0; i < cleanedLine.Count / 2; i++) { races.Add(new(cleanedLine[i], cleanedLine[(cleanedLine.Count / 2) + i])); }

foreach (Race race in races)
{
    Console.WriteLine($"Race Length: {race.Time} Miliseconds. Race Record: {race.DistanceRecord} Milimeters");
    int timesBeatRecord = 0;
    for (int i = 0; i <= race.Time; i++) //i = time to hold the button
    {
        long boatSpeed = i;
        long totalDistance = boatSpeed * (race.Time - i);
        if (totalDistance > race.DistanceRecord) { timesBeatRecord++; }
    }
    Console.WriteLine($"  The race record can be beaten with {timesBeatRecord} different options.\r\n");
    totalValue *= timesBeatRecord;
}

Console.WriteLine($"The total number of ways you could beat the records is: {totalValue}");

class Race
{
    public long Time { get; set; }
    public long DistanceRecord { get; set; }

    public Race(long time, long distanceRecord) { Time = time; DistanceRecord = distanceRecord; }
}