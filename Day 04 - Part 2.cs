string fileToRead = "Input.txt";
List<Card> cards = new();
int scratchcardTotal = 0;

for (int i = 0; i < File.ReadLines(fileToRead).Count(); i++)
{
    cards.Add(new(File.ReadLines(fileToRead).Skip(i).Take(1).First()));
}

foreach (Card card in cards)
{
    int score = 0;
    Console.Write($"Card number: {card.CardNumber:000}  |  Prize set: ");
    foreach (int i in card.PrizeSet)
    {
        Console.Write($"{i:00} ");
    }
    Console.Write(" |  Card Set: ");
    foreach (int i in card.CardSet)
    {
        Console.Write($"{i:00} ");
        if (card.PrizeSet.Contains(i)) { score++; }
    }
    Console.Write($" |  Card Score: {score:00} ");
    Console.WriteLine($" |  Number of copies of this card: {card.Copies}");

    for (int i = 1; i <= score; i++) { cards[cards.IndexOf(card) + i].Copies += card.Copies; }

    scratchcardTotal += card.Copies;
}
Console.WriteLine($"Total number of scratchcards: {scratchcardTotal}");

class Card
{
    public int CardNumber { get; set; }
    public int Copies { get; set; }
    public List<int> PrizeSet { get; set; }
    public List<int> CardSet { get; set; }

    public Card(string cardData)
    {
        string _cardData = cardData.Remove(0,cardData.IndexOf(' '));
        CardNumber = int.Parse(_cardData[.._cardData.IndexOf(':')]);
        Copies = 1;

        PrizeSet = new(ListBuilder(_cardData[_cardData.IndexOf(' ').._cardData.IndexOf('|')]));

        CardSet = new(ListBuilder(_cardData.Remove(0, _cardData.IndexOf('|'))));
    }

    static List<int> ListBuilder(string input)
    {
        string number = "";
        List<int> output = new();
        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsDigit(input[i])) { number += input[i]; }
            if ((input[i] == ' ' || i == input.Length - 1) && number != "") { output.Add(int.Parse(number)); }
            if (input[i] == ' ') { number = ""; }
        }
        return output;
    }
}