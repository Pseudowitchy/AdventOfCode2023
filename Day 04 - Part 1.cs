string fileToRead = "Input.txt";
List<Card> cards = new();
int scoreTotal = 0;

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

        if (card.PrizeSet.Contains(i))
        {
            if (score == 0) { score = 1; }
            else score *= 2;
        }
    }
    Console.WriteLine($"Card Score: {score}");
    scoreTotal += score;
}
Console.WriteLine($"Total Score: {scoreTotal}");


class Card
{
    public int CardNumber { get; set; }
    public List<int> PrizeSet { get; set; }
    public List<int> CardSet { get; set; }

    public Card(string cardData)
    {
        string _cardData = cardData.Remove(0,cardData.IndexOf(':') - 1);
        CardNumber = int.Parse(_cardData[.._cardData.IndexOf(':')]);

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