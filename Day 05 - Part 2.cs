String file = "Input.txt";
string[] fileContents = File.ReadAllLines(file);
long lowestLocation = -1;
long seedValue;

List<string> seedHolder = File.ReadLines(file).Take(1).First().Remove(0, 7).Split(' ').ToList();

for (int i = 0; i < seedHolder.Count; i += 2) 
{
    long seedStart = long.Parse(seedHolder[i]);
    long seedRange = long.Parse(seedHolder[i+1]);
    int mapStart = 0;
    int mapEnd;
    Console.WriteLine($"\r\nNEW SEED PAIRING: SEED START: {seedStart} SEED RANGE: {seedRange}\r\n");
    for (int j = 0; j <= seedRange - 1; j++)
    {
        seedValue = seedStart + j;
        for (int k = 2; k < fileContents.Length; k++)
        {
            if (fileContents[k].Contains("-to-")) { mapStart = k + 1; }
            else if ((fileContents[k] == "" || k == fileContents.Length - 1) && mapStart != 0)
            {
                mapEnd = k - 1;
                seedValue = MapCheck(seedValue, mapStart, mapEnd);
            }
        }
        if (lowestLocation == -1 || seedValue < lowestLocation) { lowestLocation = seedValue; }
    }
}
Console.WriteLine($"Lowest location: {lowestLocation}");

long MapCheck(long seed, int mapStart, int mapEnd)
{
    for (int i = mapStart; i <= mapEnd; i++)
    {
        string[] strings = fileContents[i].Split(" ");
        long destinationStart = long.Parse(strings[0]);
        long sourceStart = long.Parse(strings[1]);
        long rangeLength = long.Parse(strings[2]);

        if (seed >= sourceStart && seed <= sourceStart + rangeLength)
        {
            seed = (seed - sourceStart) + destinationStart;
            break;
        }
    }
    return seed;
}
