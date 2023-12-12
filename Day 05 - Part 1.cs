String file = "Input.txt";
string[] fileContents = File.ReadAllLines(file);
long lowestLocation = -1;

List<string> seedHolder = File.ReadLines(file).Take(1).First().Remove(0, 7).Split(' ').ToList();

foreach (string seed in seedHolder)
{
    Console.WriteLine("NEW SEED");
    long seedValue = long.Parse(seed);
    int mapStart = 0;
    int mapEnd = 0;
    for (int i = 2; i < fileContents.Length;  i++)
    {
        if (fileContents[i].Contains("-to-"))
        {
            mapStart = i + 1;
        }
        else if ((fileContents[i] == "" || i == fileContents.Length-1) && mapStart != 0)
        {
            mapEnd = i - 1;
            seedValue = MapCheck(seedValue, mapStart, mapEnd);
        }
    }
    Console.WriteLine($"Seed {seed} has a final location of: {seedValue}.");
    if (lowestLocation == -1 || seedValue < lowestLocation) { lowestLocation = seedValue; }
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
            Console.WriteLine($"{seed} is being modified.");
            seed = (seed - sourceStart) + destinationStart;
            Console.WriteLine($"{seed} is its new value");
            break;
        }
    }
    Console.WriteLine();
    return seed;

}
