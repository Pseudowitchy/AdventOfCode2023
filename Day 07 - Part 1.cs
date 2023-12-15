string file = "Input.txt";
string[] fileContents = File.ReadAllLines(file);
List<Hand> hands = new();
int handCount = 1;
int totalScore = 0;

foreach (string line in fileContents) { hands.Add(new(line)); }

var handsSorted = hands.OrderBy(c => c.HandStrength).ToList();

handsSorted = hands.OrderBy(c => c.HandStrength)
    .ThenByDescending(c => c.CardRanks[0])
    .ThenByDescending(c => c.CardRanks[1])
    .ThenByDescending(c => c.CardRanks[2])
    .ThenByDescending(c => c.CardRanks[3])
    .ThenByDescending(c => c.CardRanks[4])
    .ToList();
handsSorted.Reverse();

foreach (Hand hand in handsSorted) {
    Console.Write("Hand: ");
    foreach (char c in hand.Cards) { Console.Write(c); }
    Console.WriteLine($" Bid: {hand.Bid:D3}  |  {hand.HandStrength} Hand Rank: {hand.Rank}");

    totalScore += hand.Bid * handCount;
    handCount++;
}

Console.WriteLine($"The Total Score is: {totalScore}");

class Hand
{
    public char[] Cards { get; set; } = new char[5];
    public int[] CardRanks { get; set; } = new int[5];
    public int Bid { get; set; }
    public HandStrength HandStrength { get; set; }
    public int Rank { get; set; } = 1;

    public Hand(string input)
    {
        Bid = int.Parse(input.Remove(0, 5));
        for (int i = 0; i < 5; i++) { Cards[i] += input[i]; CardRanks[i] = CardChecker(input[i]); }
        HandStrength = StrengthChecker(input.Remove(input.IndexOf(' ')));
    }        

    private static int CardChecker(char c)
    {
        if (c == 'A') return 14;
        else if (c == 'K') return 13;
        else if (c == 'Q') return 12;
        else if (c == 'J') return 11;
        else if (c == 'T') return 10;
        else return int.Parse(char.ToString(c));
    }
    
    private static HandStrength StrengthChecker(string input)
    {
        int count = 0;
        int previousGroup = -1;
        int uniqueCards = input.Distinct().Count();

        if (uniqueCards == 5) return HandStrength.HighCard;
        else if (uniqueCards == 4) return HandStrength.OnePair;
        else if (uniqueCards == 1) return HandStrength.FiveOfAKind;
        else
        {
            foreach (char card in input.Distinct())
            {
                count = input.Where(c => c == card).Count();
                if (count == 4) return HandStrength.FourOfAKind;
                else if (count == 2 && previousGroup == 2) return HandStrength.TwoPair;
                else if (count == 2 && previousGroup == 3) return HandStrength.FullHouse;
                else if (count == 3 && previousGroup == 2) return HandStrength.FullHouse;
                else if (count == 1 && previousGroup == 3) return HandStrength.ThreeOfAKind;
                if (count > 1) previousGroup = count;
            }
            if (count == 3) return HandStrength.ThreeOfAKind;
        Console.WriteLine($"ERROR: Input: {input} Unique Cards: {uniqueCards} Count: {count} previousGroup: {previousGroup}");
        return HandStrength.HighCard;
        }
    }
}

enum HandStrength { FiveOfAKind, FourOfAKind, FullHouse, ThreeOfAKind, TwoPair, OnePair, HighCard }